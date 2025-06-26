using  System.Diagnostics;
using app_horarios_BackEnd.Data;
using Microsoft.AspNetCore.Mvc;
using App_horarios_BackEnd.Models;
using Microsoft.EntityFrameworkCore;


namespace App_horarios_BackEnd.Controllers;

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
        int horarioId = 1; // ou buscar dinamicamente

        var blocos = await _context.BlocosHorarios
            .Include(bh => bh.BlocoAula)
            .ThenInclude(ba => ba.Disciplina)
            .Include(bh => bh.BlocoAula)
            .ThenInclude(ba => ba.BlocoAulaProfessores)
            .ThenInclude(bp => bp.Professor)
            .Include(bh => bh.BlocoAula)
            .ThenInclude(ba => ba.Sala)
            .Where(bh => bh.HorarioId == horarioId)
            .ToListAsync();

        var grelha = blocos.Select(bh => new BlocoHorarioView
        {
            DiaSemana = bh.DiaSemana,
            HoraInicio = bh.HoraInicio,
            HoraFim = bh.HoraFim,
            Disciplina = bh.BlocoAula.Disciplina?.Nome ?? "—",
            Professores = string.Join(", ", bh.BlocoAula.BlocoAulaProfessores.Select(p => p.Professor.Nome)),
            Sala = bh.BlocoAula.Sala?.Nome ?? "—"
        }).ToList();

        return View(grelha);
    }

    public async Task<IActionResult> GradeHorario(int id)
    {
        // Todos os IDs existentes
        var todosIds = await _context.Horarios
            .OrderBy(h => h.Id)
            .Select(h => h.Id)
            .ToListAsync();

        // Posição do atual
        int indexAtual = todosIds.IndexOf(id);
        int? idAnterior = indexAtual > 0 ? todosIds[indexAtual - 1] : (int?)null;
        int? idSeguinte = indexAtual < todosIds.Count - 1 ? todosIds[indexAtual + 1] : (int?)null;

        ViewBag.HorarioId = id;
        ViewBag.HorarioAnterior = idAnterior;
        ViewBag.HorarioSeguinte = idSeguinte;

        // Carrega os blocos do horário atual
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
            BlocoAula = b.BlocoAula
        }).ToList();

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