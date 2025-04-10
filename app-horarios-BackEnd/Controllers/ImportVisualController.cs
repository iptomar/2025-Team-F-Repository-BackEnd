using app_horarios_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace app_horarios_BackEnd.Controllers
{
    public class ImportVisualController : Controller
    {
        
        private readonly ExcelImportService _importService;

        public ImportVisualController(ExcelImportService importService)
        {
            _importService = importService;
        }
        
        // GET: ImportVisualController
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Ficheiro inválido.";
                return View();
            }

            using var stream = file.OpenReadStream();
            await _importService.ImportAllSheetsAsync(stream);

            ViewBag.Message = "Importação concluída com sucesso!";
            return View();
        }

    }
}
