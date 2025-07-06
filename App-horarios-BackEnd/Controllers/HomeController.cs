using System.Diagnostics;
using app_horarios_BackEnd.Data;
using Microsoft.AspNetCore.Mvc;
using App_horarios_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace App_horarios_BackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HorarioDbContext _context;

        public HomeController(ILogger<HomeController> logger, HorarioDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                int horarioId = 1;

                var blocos = await _context.BlocosHorarios
                    .Include(b => b.BlocoAula)
                        .ThenInclude(a => a.Disciplina)
                    .Include(b => b.BlocoAula.BlocoAulaProfessores)
                        .ThenInclude(p => p.Professor)
                    .Where(b => b.HorarioId == horarioId)
                    .ToListAsync();

                var viewModel = blocos.Select(b => new BlocoHorarioView
                {
                    DiaSemana = b.DiaSemana,
                    HoraInicio = b.HoraInicio,
                    HoraFim = b.HoraFim,
                    BlocoAula = b.BlocoAula,
                    Id = b.Id // Adicione Id para controlar no hashset
                }).ToList();

                ViewBag.HorarioId = horarioId;
                ViewBag.HorarioAnterior = null;
                ViewBag.HorarioSeguinte = null;

                // Gerar intervalos
                var listaIntervalos = new List<string>();
                TimeSpan horaInicio = new TimeSpan(8, 0, 0);  // 08:00
                TimeSpan horaFim = new TimeSpan(20, 0, 0);    // 20:00

                while (horaInicio < horaFim)
                {
                    var proximo = horaInicio.Add(new TimeSpan(0, 30, 0)); // +30 minutos
                    listaIntervalos.Add($"{horaInicio:hh\\:mm} - {proximo:hh\\:mm}");
                    horaInicio = proximo;
                }

                ViewBag.Intervalos = listaIntervalos;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return Content($"Erro ao conectar à base de dados: {ex.Message}");
            }
        }

        public async Task<IActionResult> GradeHorario(int id)
        {
            var todosIds = await _context.Horarios
                .OrderBy(h => h.Id)
                .Select(h => h.Id)
                .ToListAsync();

            int indexAtual = todosIds.IndexOf(id);
            int? idAnterior = indexAtual > 0 ? todosIds[indexAtual - 1] : (int?)null;
            int? idSeguinte = indexAtual < todosIds.Count - 1 ? todosIds[indexAtual + 1] : (int?)null;

            ViewBag.HorarioId = id;
            ViewBag.HorarioAnterior = idAnterior;
            ViewBag.HorarioSeguinte = idSeguinte;

            var blocos = await _context.BlocosHorarios
                .Include(b => b.BlocoAula)
                    .ThenInclude(a => a.Disciplina)
                .Include(b => b.BlocoAula.BlocoAulaProfessores)
                    .ThenInclude(p => p.Professor)
                .Where(b => b.HorarioId == id)
                .ToListAsync();

            var viewModel = blocos.Select(b => new BlocoHorarioView
            {
                DiaSemana = b.DiaSemana,
                HoraInicio = b.HoraInicio,
                HoraFim = b.HoraFim,
                BlocoAula = b.BlocoAula,
                Id = b.Id // Adicione Id
            }).ToList();

            // Gerar intervalos
            var listaIntervalos = new List<string>();
            TimeSpan horaInicio = new TimeSpan(8, 0, 0);  // 08:00
            TimeSpan horaFim = new TimeSpan(20, 0, 0);    // 20:00

            while (horaInicio < horaFim)
            {
                var proximo = horaInicio.Add(new TimeSpan(0, 30, 0)); // +30 minutos
                listaIntervalos.Add($"{horaInicio:hh\\:mm} - {proximo:hh\\:mm}");
                horaInicio = proximo;
            }

            ViewBag.Intervalos = listaIntervalos;

            return View("Index", viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
