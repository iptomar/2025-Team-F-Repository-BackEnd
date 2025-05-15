using System.Data;
using System.Text;
using ExcelDataReader;
using app_horarios_BackEnd.Data;
using App_horarios_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace app_horarios_BackEnd.Services;

    public class ExcelImportService
    {
        private readonly HorarioDbContext _context;

        public ExcelImportService(HorarioDbContext context)
        {
            _context = context;
        }

        public async Task ImportAllSheetsAsync(Stream excelStream)
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var reader = ExcelReaderFactory.CreateReader(excelStream);

            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true // 👈 Importante! Usa a primeira linha como cabeçalho
                }
            };

            var dataSet = reader.AsDataSet(conf);

            foreach (DataTable table in dataSet.Tables)
            {
                string sheetName = table.TableName.Trim().ToLower();

                switch (sheetName)
                {
                    case "salas":
                        await ImportSalas(table);
                        break;

                    case "docentes":
                        await ImportProfessores(table);
                        break;

                    case "cursos":
                        await ImportCursos(table);
                        break;

                    case "tipologias":
                    case "tiposaula":
                        await ImportTipoAula(table);
                        break;

                    case "secretariado":
                        await ImportSecretariado(table);
                        break;

                    case "dir. curso":
                    case "dir curso":
                    case "diretorcurso":
                        await ImportDiretorCurso(table);
                        break;

                    case "ccc":
                        await ImportComissaoCurso(table);
                        break;

                    case "ucs":
                        await ImportDisciplinas(table);
                        break;

                    default:
                        Console.WriteLine($"[IGNORADO] Folha não reconhecida: {table.TableName}");
                        break;
                }
            }

            await _context.SaveChangesAsync();
        }


        private async Task ImportCursos(DataTable table)
        {
            var ramosEmMemoria = new HashSet<int>();

            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                try
                {
                    int escolaId = int.Parse(row[2]?.ToString() ?? "0");
                    int ramoId = int.Parse(row["CD_RAMO"]?.ToString() ?? "0");
                    string nomeRamo = row["NM_RAMO"]?.ToString();

                    // Verifica e insere Ramo se necessário (com cache)
                    if (!ramosEmMemoria.Contains(ramoId))
                    {
                        var ramo = await _context.Ramos
                            .AsNoTracking()
                            .FirstOrDefaultAsync(r => r.Id == ramoId);

                        if (ramo == null)
                        {
                            _context.Ramos.Add(new Ramo { Id = ramoId, Nome = nomeRamo });
                        }

                        ramosEmMemoria.Add(ramoId);
                    }

                    // Cria objeto Curso
                    var curso = new Curso
                    {
                        Id = int.Parse(row["CD_CURSO"]?.ToString() ?? "0"),
                        Nome = row["NM_CURSO"]?.ToString(),
                        EscolaId = escolaId,
                        RamoId = ramoId,
                        GrauId = int.Parse(row["CD_GRAU"]?.ToString() ?? "0")

                    };

                    // Verifica duplicação
                    if (!await _context.Cursos.AnyAsync(c => c.Id == curso.Id))
                    {
                        _context.Cursos.Add(curso);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro na linha {i + 1}: {ex.Message}");
                    continue;
                }
            }

            await _context.SaveChangesAsync();
        }

        
        private async Task ImportProfessores(DataTable table)
        {
            var unidadesEmMemoria = new HashSet<int>();
            var categoriasEmMemoria = new HashSet<int>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                if (row[0]?.ToString().Trim().ToLower() == "id")
                    continue;

                if (row.ItemArray.All(field => field == null || string.IsNullOrWhiteSpace(field.ToString())))
                    continue;

                try
                {
                    int id = Convert.ToInt32(row[0]);
                    string nome = row[1]?.ToString();
                    string email = row[2]?.ToString();
                    int idUd = Convert.ToInt32(row[3]);
                    string nomeUd = row[4]?.ToString();
                    int idCategoria = Convert.ToInt32(row[5]);
                    string nomeCategoria = row[6]?.ToString();

                    // Unidade Departamental
                    if (!unidadesEmMemoria.Contains(idUd))
                    {
                        var udExistente = await _context.UnidadesDepartamentais.AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Id == idUd);

                        if (udExistente == null)
                        {
                            _context.UnidadesDepartamentais.Add(new UnidadeDepartamental { Id = idUd, Nome = nomeUd });
                        }

                        unidadesEmMemoria.Add(idUd); // Marcar como "já tratado"
                    }

                    // Categoria Docente
                    if (!categoriasEmMemoria.Contains(idCategoria))
                    {
                        var catExistente = await _context.CategoriasDocentes.AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == idCategoria);

                        if (catExistente == null)
                        {
                            _context.CategoriasDocentes.Add(new CategoriaDocente { Id = idCategoria, Nome = nomeCategoria });
                        }

                        categoriasEmMemoria.Add(idCategoria);
                    }

                    // Professor
                    var profExistente = await _context.Professores.FindAsync(id);

                    if (profExistente == null)
                    {
                        _context.Professores.Add(new Professor
                        {
                            Id = id,
                            Nome = nome,
                            Email = email,
                            UnidadeDepartamentalId = idUd,
                            CategoriaId = idCategoria
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro na linha {i + 1}: {ex.Message}");
                    continue;
                }
            }

            await _context.SaveChangesAsync();
        }

        
        private async Task ImportSalas(DataTable table)
        {
            var salasEmMemoria = new HashSet<string>(); // chave composta: "nome|escolaId"

            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                string nome = row[0]?.ToString()?.Trim();
                bool parsedCapacidade = int.TryParse(row[1]?.ToString(), out int capacidade);
                string tipo = row[2]?.ToString()?.Trim();
                bool parsedEscola = int.TryParse(row[3]?.ToString(), out int escolaId);

                if (string.IsNullOrEmpty(nome) || !parsedEscola)
                    continue;

                string chaveSala = $"{nome.ToLower()}|{escolaId}";

                if (!salasEmMemoria.Contains(chaveSala))
                {
                    var salaExistente = await _context.Salas
                        .AsNoTracking()
                        .FirstOrDefaultAsync(s => s.Nome == nome && s.EscolaId == escolaId);

                    if (salaExistente == null)
                    {
                        var sala = new Sala
                        {
                            Nome = nome,
                            Capacidade = parsedCapacidade ? capacidade : 0,
                            Tipo = tipo,
                            EscolaId = escolaId
                        };

                        _context.Salas.Add(sala);
                    }

                    salasEmMemoria.Add(chaveSala);
                }
            }

            await _context.SaveChangesAsync();
        }
        
        private async Task ImportSecretariado(DataTable table)
        {
            var idsUtilizadoresExistentes = new HashSet<int>(
                await _context.Secretariados
                    .AsNoTracking()
                    .Select(s => s.IdUtilizador)
                    .ToListAsync()
            );

            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                bool parsedEscola = int.TryParse(row[0]?.ToString(), out int escolaId);
                bool parsedCurso = int.TryParse(row[1]?.ToString(), out int cursoId);
                bool parsedIdUtilizador = int.TryParse(row[2]?.ToString(), out int idUtilizador);

                string nome = row[3]?.ToString()?.Trim();
                string email = row[4]?.ToString()?.Trim();

                if (!parsedEscola || !parsedCurso || !parsedIdUtilizador || string.IsNullOrEmpty(nome))
                    continue;

                if (!idsUtilizadoresExistentes.Contains(idUtilizador))
                {
                    var secretariado = new Secretariado
                    {
                        IdUtilizador = idUtilizador,
                        Nome = nome,
                        Email = email,
                        EscolaId = escolaId,
                        CursoId = cursoId
                    };

                    _context.Secretariados.Add(secretariado);
                    idsUtilizadoresExistentes.Add(idUtilizador);
                }
            }

            await _context.SaveChangesAsync();
        }
        
        private async Task ImportDisciplinas(DataTable table)
        {
            // Verifica se a coluna existe
            if (!table.Columns.Contains("CD_INSTITUIC"))
                throw new Exception("A coluna 'CD_INSTITUIC' não foi encontrada no ficheiro de importação.");

            if (!table.Columns.Contains("CD_GRAU") || !table.Columns.Contains("DS_GRAU"))
                throw new Exception("Colunas de grau (CD_GRAU ou DS_GRAU) não encontradas no ficheiro de importação.");


            var grausExistentes = await _context.Graus.AsNoTracking().ToDictionaryAsync(g => g.Id);
            var escolasExistentes = await _context.Escolas.AsNoTracking().ToDictionaryAsync(e => e.Id);
            var ramosExistentes = await _context.Ramos.AsNoTracking().ToDictionaryAsync(r => r.Id);
            var cursosExistentes = await _context.Cursos.AsNoTracking().ToDictionaryAsync(c => c.Id);
            var disciplinasExistentes = await _context.Disciplinas.AsNoTracking().ToDictionaryAsync(d => d.Id);
            var associacoesExistentes = await _context.DisciplinaCursoProfessor
                .AsNoTracking()
                .Select(x => new { x.CursoId, x.DisciplinaId })
                .ToListAsync();
            var associacoesSet = new HashSet<(int cursoId, int disciplinaId)>(associacoesExistentes.Select(x => (x.CursoId, x.DisciplinaId)));

            foreach (DataRow row in table.Rows)
            {
                int instituicaoId = Convert.ToInt32(row["CD_INSTITUIC"]);
                
                

                
                // Ramo
                int ramoId = Convert.ToInt32(row["CD_RAMO"]);
                string nomeRamo = row["NM_RAMO"]?.ToString();
                Ramo ramo;
                if (!ramosExistentes.TryGetValue(ramoId, out ramo))
                {
                    ramo = new Ramo { Id = ramoId, Nome = nomeRamo };
                    _context.Ramos.Add(ramo);
                    ramosExistentes[ramoId] = ramo;
                    await _context.SaveChangesAsync(); // salva para poder usar em FK
                }


                int grauId = Convert.ToInt32(row["CD_GRAU"]);
                string nomeGrau = row["DS_GRAU"].ToString()?.Trim();

                // Garante que o Grau existe
                var grau = await _context.Graus.FirstOrDefaultAsync(g => g.Id == grauId);
                if (grau == null)
                {
                    grau = new Grau
                    {
                        Id = grauId,
                        Nome = nomeGrau
                    };
                    _context.Graus.Add(grau);
                    await _context.SaveChangesAsync(); // salva para poder usar em FK
                }

                int cursoId = Convert.ToInt32(row["CD_CURSO"]);
                string nomeCurso = row["NM_CURSO"].ToString();

                // Curso
                if (!cursosExistentes.ContainsKey(cursoId))
                {
                    var curso = new Curso
                    {
                        Id = cursoId,
                        Nome = nomeCurso,
                        EscolaId = instituicaoId,
                        RamoId = ramoId,
                        GrauId = grauId // <-- Agora usando GrauId
                    };
                    _context.Cursos.Add(curso);
                    cursosExistentes[cursoId] = curso;
                }


                // Disciplina
                int disciplinaId = Convert.ToInt32(row["CD_DISCIP"]);
                string nomeDisciplina = row["DS_DISCIP"].ToString();

                if (!disciplinasExistentes.ContainsKey(disciplinaId))
                {
                    var disciplina = new Disciplina
                    {
                        Id = disciplinaId,
                        Nome = nomeDisciplina,
                        Plano = row["NM_PLANO"].ToString(),
                        Ano = Convert.ToInt32(row["ANO"]),
                        Semestre = Convert.ToInt32(row["SEMESTRE"]),
                        Tipo = row["TIPO"].ToString(),
                        HorasTeorica = Convert.ToInt32(row["HR_TEORICA"]),
                        HorasPratica = Convert.ToInt32(row["HR_PRATICA"]),
                        HorasTp = Convert.ToInt32(row["HR_TEO_PRA"]),
                        HorasSeminario = Convert.ToInt32(row["HR_SEMINAR"]),
                        HorasLaboratorio = Convert.ToInt32(row["HR_LABORAT"]),
                        HorasCampo = Convert.ToInt32(row["HR_CAMPO"]),
                        HorasOrientacao = Convert.ToInt32(row["HR_ORIENTACAO"]),
                        HorasEstagio = Convert.ToInt32(row["HR_ESTAGIO"]),
                        HorasOutras = Convert.ToInt32(row["HR_OUTRA"])
                    };
                    _context.Disciplinas.Add(disciplina);
                    disciplinasExistentes[disciplinaId] = disciplina;
                }

                // Associação Disciplina-Curso
                if (!associacoesSet.Contains((cursoId, disciplinaId)))
                {
                    _context.DisciplinaCursoProfessor.Add(new DisciplinaCursoProfessor
                    {
                        CursoId = cursoId,
                        DisciplinaId = disciplinaId,
                        ProfessorId = null
                    });

                    associacoesSet.Add((cursoId, disciplinaId));
                }
            }

            await _context.SaveChangesAsync();
        }

        
        private async Task ImportDiretorCurso(DataTable table)
        {
            // Carrega todos os diretores existentes (cursoId + utilizadorId)
            var diretoresExistentes = await _context.DiretoresCurso
                .AsNoTracking()
                .Select(d => new { d.CursoId, d.IdUtilizador })
                .ToListAsync();

            var diretoresSet = new HashSet<(int cursoId, int idUtilizador)>(
                diretoresExistentes.Select(d => (d.CursoId, d.IdUtilizador))
            );

            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                bool parsedEscola = int.TryParse(row[0]?.ToString(), out int escolaId);
                bool parsedCurso = int.TryParse(row[1]?.ToString(), out int cursoId);
                bool parsedUtilizador = int.TryParse(row[2]?.ToString(), out int idUtilizador);

                if (!parsedEscola || !parsedCurso || !parsedUtilizador)
                    continue;

                if (diretoresSet.Contains((cursoId, idUtilizador)))
                    continue;

                var diretor = new DiretorCurso
                {
                    EscolaId = escolaId,
                    CursoId = cursoId,
                    IdUtilizador = idUtilizador
                };

                _context.DiretoresCurso.Add(diretor);
                diretoresSet.Add((cursoId, idUtilizador));
            }

            await _context.SaveChangesAsync();
        }


        
        private async Task ImportComissaoCurso(DataTable table)
        {
            // Carrega combinações existentes de (EscolaId, CursoId, IdUtilizador)
            var comissoesExistentes = await _context.ComissoesCurso
                .AsNoTracking()
                .Select(c => new { c.EscolaId, c.CursoId, c.IdUtilizador })
                .ToListAsync();

            var comissoesSet = new HashSet<(int escolaId, int cursoId, int idUtilizador)>(
                comissoesExistentes.Select(c => (c.EscolaId, c.CursoId, c.IdUtilizador))
            );

            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                int escolaId = Convert.ToInt32(row["id_escola"]);
                int cursoId = Convert.ToInt32(row["cod_curso"]);
                int idUtilizador = Convert.ToInt32(row["id_utilizador"]);

                if (comissoesSet.Contains((escolaId, cursoId, idUtilizador)))
                    continue;

                var novaComissao = new ComissaoCurso
                {
                    EscolaId = escolaId,
                    CursoId = cursoId,
                    IdUtilizador = idUtilizador
                };

                _context.ComissoesCurso.Add(novaComissao);
                comissoesSet.Add((escolaId, cursoId, idUtilizador));
            }

            await _context.SaveChangesAsync();
        }
        
        private async Task ImportTipoAula(DataTable table)
        {
            // Verifica se a coluna 'id' existe
            if (!table.Columns.Contains("id") || !table.Columns.Contains("tipo") || !table.Columns.Contains("acronimo"))
                throw new Exception("A folha 'Tipologias' deve conter as colunas 'id', 'tipo' e 'acronimo'.");

            var idsExistentes = await _context.TiposAula
                .AsNoTracking()
                .Select(t => t.Id)
                .ToListAsync();

            var idSet = new HashSet<int>(idsExistentes);

            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                if (!int.TryParse(row["id"]?.ToString(), out int id))
                    continue;

                if (idSet.Contains(id))
                    continue;

                string tipo = row["tipo"]?.ToString()?.Trim();
                string acronimo = row["acronimo"]?.ToString()?.Trim();

                var tipoAula = new TipoAula
                {
                    Id = id,
                    Tipo = tipo,
                    Acronimo = acronimo
                };

                _context.TiposAula.Add(tipoAula);
                idSet.Add(id);
            }

            await _context.SaveChangesAsync();
        }


    }



