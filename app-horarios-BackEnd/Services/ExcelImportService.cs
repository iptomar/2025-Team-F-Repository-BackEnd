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

                case "grau":
                case "graus":
                    await ImportGraus(table);
                    break;
                

                case "categorias":
                case "categoria":
                case "categoriasdocentes":
                    await ImportCategoriasDocentes(table);
                    break;


                case "unidade_departamental":
                case "unidades":
                case "ud":
                case "unidadesdepartamentais":
                    await ImportUnidadesDepartamentais(table);
                    break;
                
                case "docentes":
                case "professores":
                    await ImportProfessores(table);
                    break;

                case "tipologias":
                case "tiposaula":
                    await ImportTipoAula(table);
                    break;
                
                case "cursos":
                    await ImportCursos(table);
                    break;

                case "secretariado":
                    await ImportSecretariado(table);
                    break;

                case "dir. curso":
                case "dir_curso":
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


    private async Task ImportGraus(DataTable table)
    {
        var grausExistentes = await _context.Graus
            .AsNoTracking()
            .Select(g => g.Id)
            .ToListAsync();

        var idSet = new HashSet<int>(grausExistentes);

        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];

            if (!int.TryParse(row["CD_GRAU"]?.ToString(), out int id))
                continue;

            if (idSet.Contains(id))
                continue;

            string nome = row["DS_GRAU"]?.ToString()?.Trim();
            string duracao = row["duração"]?.ToString()?.Trim();

            if (string.IsNullOrWhiteSpace(nome))
                continue;

            var grau = new Grau
            {
                Id = id,
                Nome = nome,
                Duracao = duracao ?? ""
            };

            _context.Graus.Add(grau);
            idSet.Add(id);
        }

        await _context.SaveChangesAsync();
    }

    private async Task ImportUnidadesDepartamentais(DataTable table)
    {
        var idsExistentes = await _context.UnidadesDepartamentais
            .AsNoTracking()
            .Select(ud => ud.Id)
            .ToListAsync();

        var idSet = new HashSet<int>(idsExistentes);

        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];

            if (!int.TryParse(row["id_ud"]?.ToString(), out int id))
                continue;

            if (idSet.Contains(id))
                continue;

            string nome = row["ud"]?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(nome))
                continue;

            var unidade = new UnidadeDepartamental
            {
                Id = id,
                Nome = nome
            };

            _context.UnidadesDepartamentais.Add(unidade);
            idSet.Add(id);
        }

        await _context.SaveChangesAsync();
    }

    private async Task ImportCursos(DataTable table)
    {
        var cursosExistentes = await _context.Cursos
            .AsNoTracking()
            .ToDictionaryAsync(c => (c.Id, c.EscolaId));


        var grausExistentes = await _context.Graus
            .AsNoTracking()
            .ToDictionaryAsync(g => g.Id);

        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];

            // Validações mínimas
            if (!int.TryParse(row["CD_CURSO"]?.ToString(), out int cursoId) ||
                !int.TryParse(row["CD_INSTITUIC"]?.ToString(), out int escolaId))
                continue;

            string nome = row["NM_CURSO"]?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(nome))
                continue;

            // Grau
            int.TryParse(row["CD_GRAU"]?.ToString(), out int grauId);
            if (!grausExistentes.ContainsKey(grauId))
            {
                var novoGrau = new Grau { Id = grauId, Nome = $"Grau {grauId}" };
                _context.Graus.Add(novoGrau);
                grausExistentes[grauId] = novoGrau;
            }


            var chave = (cursoId, escolaId);
            if (cursosExistentes.ContainsKey(chave))
                continue;

            var curso = new Curso
            {
                Id = cursoId,
                EscolaId = escolaId,
                Nome = nome,
                GrauId = grauId
            };

            _context.Cursos.Add(curso);
            cursosExistentes[chave] = curso;
        }

        await _context.SaveChangesAsync();
    }


    private async Task ImportProfessores(DataTable table)
    {
        var idsProfessoresExistentes = await _context.Professores
            .AsNoTracking()
            .Select(p => p.Id)
            .ToListAsync();

        var idSet = new HashSet<int>(idsProfessoresExistentes);

        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];

            if (!int.TryParse(row["id"]?.ToString(), out int id))
                continue;

            if (idSet.Contains(id))
                continue;

            string nome = row["nome"]?.ToString()?.Trim();
            string email = row["email"]?.ToString()?.Trim();

            if (!int.TryParse(row["id_ud"]?.ToString(), out int unidadeId))
                continue;

            if (!int.TryParse(row["id_categoria"]?.ToString(), out int categoriaId))
                continue;

            var professor = new Professor
            {
                Id = id,
                Nome = nome,
                Email = email,
                UnidadeDepartamentalId = unidadeId,
                CategoriaId = categoriaId
            };

            _context.Professores.Add(professor);
            idSet.Add(id);
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

        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];

            if (!int.TryParse(row["id_utilizador"]?.ToString(), out int idUtilizador))
                continue;

            if (idsUtilizadoresExistentes.Contains(idUtilizador))
                continue;

            string nome = row["nome"]?.ToString()?.Trim();
            string email = row["email"]?.ToString()?.Trim();

            if (!int.TryParse(row["id_escola"]?.ToString(), out int escolaId))
                continue;

            if (!int.TryParse(row["cod_curso"]?.ToString(), out int cursoId))
                continue;

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

        await _context.SaveChangesAsync();
    }


    private async Task ImportCategoriasDocentes(DataTable table)
    {
        // Lê todos os IDs existentes no banco para evitar duplicações
        var idsExistentes = await _context.CategoriasDocentes
            .AsNoTracking()
            .Select(c => c.Id)
            .ToListAsync();

        var idSet = new HashSet<int>(idsExistentes);

        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];

            if (!int.TryParse(row["id_categoria"]?.ToString(), out int id))
                continue;

            if (idSet.Contains(id))
                continue;

            string nome = row["categoria"]?.ToString()?.Trim();

            if (string.IsNullOrWhiteSpace(nome))
                continue;

            var categoria = new CategoriaDocente
            {
                Id = id,
                Nome = nome
            };

            _context.CategoriasDocentes.Add(categoria);
            idSet.Add(id);
        }

        await _context.SaveChangesAsync();
    }


    private async Task ImportDiretorCurso(DataTable table)
    {
        var diretoresExistentes = await _context.DiretoresCurso
            .AsNoTracking()
            .Select(d => new { d.EscolaId, d.CursoId, d.IdUtilizador })
            .ToListAsync();

        var diretoresSet = new HashSet<(int escolaId, int cursoId, int utilizadorId)>(
            diretoresExistentes.Select(d => (d.EscolaId, d.CursoId, d.IdUtilizador))
        );

        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];

            if (!int.TryParse(row["CD_INSTITUIC"]?.ToString(), out int escolaId) ||
                !int.TryParse(row["CD_CURSO"]?.ToString(), out int cursoId) ||
                !int.TryParse(row["id_utilizador"]?.ToString(), out int utilizadorId))
                continue;

            var chave = (escolaId, cursoId, utilizadorId);
            if (diretoresSet.Contains(chave))
                continue;

            var diretor = new DiretorCurso
            {
                EscolaId = escolaId,
                CursoId = cursoId,
                IdUtilizador = utilizadorId
            };

            _context.DiretoresCurso.Add(diretor);
            diretoresSet.Add(chave);
        }

        await _context.SaveChangesAsync();
    }


    private async Task ImportComissaoCurso(DataTable table)
    {
        var comissoesExistentes = await _context.ComissoesCurso
            .AsNoTracking()
            .Select(c => new { c.EscolaId, c.CursoId, c.IdUtilizador })
            .ToListAsync();

        var comissaoSet = new HashSet<(int escolaId, int cursoId, int utilizadorId)>(
            comissoesExistentes.Select(c => (c.EscolaId, c.CursoId, c.IdUtilizador))
        );

        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];

            if (!int.TryParse(row["id_escola"]?.ToString(), out int escolaId) ||
                !int.TryParse(row["cod_curso"]?.ToString(), out int cursoId) ||
                !int.TryParse(row["id_utilizador"]?.ToString(), out int utilizadorId))
                continue;

            var chave = (escolaId, cursoId, utilizadorId);
            if (comissaoSet.Contains(chave))
                continue;

            var comissao = new ComissaoCurso
            {
                EscolaId = escolaId,
                CursoId = cursoId,
                IdUtilizador = utilizadorId
            };

            _context.ComissoesCurso.Add(comissao);
            comissaoSet.Add(chave);
        }

        await _context.SaveChangesAsync();
    }


    private async Task ImportTipoAula(DataTable table)
    {
        var idsExistentes = await _context.TiposAula
            .AsNoTracking()
            .Select(t => t.Id)
            .ToListAsync();

        var idSet = new HashSet<int>(idsExistentes);

        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];

            if (!int.TryParse(row["id"]?.ToString(), out int id))
                continue;

            if (idSet.Contains(id))
                continue;

            string tipo = row["tipo"]?.ToString()?.Trim();
            string acronimo = row["acronimo"]?.ToString()?.Trim();

            if (string.IsNullOrWhiteSpace(tipo) || string.IsNullOrWhiteSpace(acronimo))
                continue;

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


    private async Task ImportDisciplinas(DataTable table)
    {
        var disciplinasExistentes = await _context.Disciplinas
            .AsNoTracking()
            .ToDictionaryAsync(d => d.DisciplinaId); // CD_DISCIP

        var cursosExistentes = await _context.Cursos
            .AsNoTracking()
            .ToDictionaryAsync(c => (c.Id, c.EscolaId));

        var associacoesExistentes = await _context.DisciplinaCursoProfessor
            .AsNoTracking()
            .Select(x => new { x.CursoId, x.EscolaId, x.DisciplinaId })
            .ToListAsync();

        var associacoesSet = new HashSet<(int cursoId, int escolaId, int disciplinaId)>(
            associacoesExistentes.Select(x => (x.CursoId, x.EscolaId, x.DisciplinaId))
        );


        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];

            if (!int.TryParse(row["CD_DISCIP"]?.ToString(), out int disciplinaId) ||
                !int.TryParse(row["CD_CURSO"]?.ToString(), out int cursoId) ||
                !int.TryParse(row["CD_INSTITUIC"]?.ToString(), out int escolaId))
                continue;

            string nome = row["DS_DISCIP"]?.ToString()?.Trim();

            // Verifica se já temos essa disciplina (salva uma vez só)
            if (!disciplinasExistentes.ContainsKey(disciplinaId))
            {
                var disciplina = new Disciplina
                {
                    DisciplinaId = disciplinaId,
                    Nome = nome,
                    Plano = row["NM_PLANO"]?.ToString(),
                    PlanoId = int.TryParse(row["CD_PLANO"]?.ToString(), out int planoId) ? planoId : null,
                    Ramo = row["NM_RAMO"]?.ToString(),
                    RamoId = int.TryParse(row["CD_RAMO"]?.ToString(), out int ramoId) ? ramoId : null,
                    Ano = int.TryParse(row["ANO"]?.ToString(), out int ano) ? ano : null,
                    Semestre = row["SEMESTRE"]?.ToString(),
                    Tipo = row["TIPO"]?.ToString(),

                    HorasTeorica = int.TryParse(row["HR_TEORICA"]?.ToString(), out int ht) ? ht : null,
                    HorasPratica = int.TryParse(row["HR_PRATICA"]?.ToString(), out int hp) ? hp : null,
                    HorasTp = int.TryParse(row["HR_TEO_PRA"]?.ToString(), out int htp) ? htp : null,
                    HorasSeminario = int.TryParse(row["HR_SEMINAR"]?.ToString(), out int hs) ? hs : null,
                    HorasLaboratorio = int.TryParse(row["HR_LABORAT"]?.ToString(), out int hl) ? hl : null,
                    HorasCampo = int.TryParse(row["HR_CAMPO"]?.ToString(), out int hc) ? hc : null,
                    HorasOrientacao = int.TryParse(row["HR_ORIENTACAO"]?.ToString(), out int ho) ? ho : null,
                    HorasEstagio = int.TryParse(row["HR_ESTAGIO"]?.ToString(), out int he) ? he : null,
                    HorasOutras = int.TryParse(row["HR_OUTRA"]?.ToString(), out int ho2) ? ho2 : null
                };

                _context.Disciplinas.Add(disciplina);
                disciplinasExistentes[disciplinaId] = disciplina;
            }

            // Verifica se já existe associação Curso x Escola x Disciplina
            var chave = (cursoId, escolaId, disciplinaId);
            if (!associacoesSet.Contains(chave))
            {
                var associacao = new DisciplinaCursoProfessor
                {
                    CursoId = cursoId,
                    EscolaId = escolaId,
                    DisciplinaId = disciplinaId,
                    ProfessorId = null // inicialmente sem professor
                };

                _context.DisciplinaCursoProfessor.Add(associacao);
                associacoesSet.Add(chave);
            }
        }

        await _context.SaveChangesAsync();
    }
}