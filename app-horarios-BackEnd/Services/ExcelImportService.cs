using System.Data;
using System.Text;
using ExcelDataReader;
using app_horarios_BackEnd.Data;
using App_horarios_BackEnd.Models;

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
            var dataSet = reader.AsDataSet();

            foreach (DataTable table in dataSet.Tables)
            {
                string sheetName = table.TableName.Trim().ToLower();

                switch (sheetName)
                {
                    case "cursos":
                        await ImportCursos(table);
                        break;

                    case "professores":
                        await ImportProfessores(table);
                        break;

                    case "salas":
                        await ImportSalas(table);
                        break;

                    default:
                        // Ignorar folhas desconhecidas ou logar
                        break;
                }
            }

            await _context.SaveChangesAsync();
        }

        private Task ImportCursos(DataTable table)
        {
            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                var curso = new Curso
                {
                    Nome = row[0]?.ToString(),
                    Tipo = row[1]?.ToString(),
                    EscolaId = int.Parse(row[2]?.ToString() ?? "0")
                };

                _context.Cursos.Add(curso);
            }

            return Task.CompletedTask;
        }

        private Task ImportProfessores(DataTable table)
        {
            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                var prof = new Professor
                {
                    Nome = row[0]?.ToString(),
                    Email = row[1]?.ToString(),
                    UnidadeDepartamentalId = int.Parse(row[2]?.ToString() ?? "0"),
                    CategoriaId = int.Parse(row[3]?.ToString() ?? "0")
                };

                _context.Professores.Add(prof);
            }

            return Task.CompletedTask;
        }

        private Task ImportSalas(DataTable table)
        {
            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                var sala = new Sala
                {
                    Nome = row[0]?.ToString(),
                    Capacidade = int.Parse(row[1]?.ToString() ?? "0"),
                    Tipo = row[2]?.ToString(),
                    EscolaId = int.Parse(row[3]?.ToString() ?? "0")
                };

                _context.Salas.Add(sala);
            }

            return Task.CompletedTask;
        }
    
    }


