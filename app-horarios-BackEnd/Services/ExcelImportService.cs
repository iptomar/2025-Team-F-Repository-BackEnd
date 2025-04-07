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

        public async Task ImportCursosFromExcelAsync(Stream excelStream)
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var reader = ExcelReaderFactory.CreateReader(excelStream);
            var result = reader.AsDataSet();
            var table = result.Tables[0]; // Primeira folha do Excel

            for (int i = 1; i < table.Rows.Count; i++) // Começa em 1 para pular o cabeçalho
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

            await _context.SaveChangesAsync();
        }
    }


