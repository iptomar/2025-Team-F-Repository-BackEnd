using app_horarios_BackEnd.Data;
using App_horarios_BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace app_horarios_BackEnd.Controllers;

public class EscolaController : Controller
{
    private readonly HorarioDbContext _context;

    public EscolaController(HorarioDbContext context)
    {
        _context = context;
    }

    // GET: Escola
    public async Task<IActionResult> Index()
    {
        var horarioDbContext = _context.Escolas;
        return View(await horarioDbContext.ToListAsync());
    }
    
    public async Task<IActionResult> Pesquisar(string? search)
    {
        ViewData["Search"] = search;

        var escolas = _context.Escolas
            .Include(e => e.Localizacao)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();

            escolas = escolas.Where(e =>
                e.Nome.ToLower().Contains(search) ||
                (e.Abreviacao != null && e.Abreviacao.ToLower().Contains(search)) ||
                e.Localizacao.Nome.ToLower().Contains(search) ||
                (e.Localizacao.Abreviacao != null && e.Localizacao.Abreviacao.ToLower().Contains(search)));
        }

        return View("Index", await escolas.ToListAsync());
    }


    
    // GET: Escola/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var escola = await _context.Escolas
            .Include(e => e.Localizacao) // <-- aqui está o essencial
            .FirstOrDefaultAsync(m => m.Id == id);

        if (escola == null) return NotFound();

        return View(escola);
    }


    // GET: Escola/Create
    public IActionResult Create()
    {
        ViewData["LocalizacaoId"] = new SelectList(_context.Localizacoes, "Id", "Nome");
        return View();
    }

    // POST: Escola/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,LocalizacaoId,Abreviacao,")] Escola escola)
    {
        if (!ModelState.IsValid)
        {
            _context.Add(escola);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["LocalizacaoId"] = new SelectList(_context.Localizacoes, "Id", "Nome", escola.LocalizacaoId);
        return View(escola);
    }


    // GET: Escola/Edit/5
    // GET: Escola/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var escola = await _context.Escolas.FindAsync(id);
        if (escola == null) return NotFound();

        ViewData["LocalizacaoId"] = new SelectList(_context.Localizacoes, "Id", "Nome", escola.LocalizacaoId);
        return View(escola);
    }


    // POST: Escola/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,LocalizacaoId,Abreviacao")]
        Escola escola)
    {
        if (id != escola.Id)
        {
            return NotFound();
        }
    
        if (ModelState.IsValid)
        {
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}, Exception: {error.Exception}");
                }
            }
            return View(escola);
        }

        if (!ModelState.IsValid)
        {
            try
            {
                _context.Update(escola);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Escola atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EscolaExists(escola.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(escola);
    }

    // GET: Escola/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var escola = await _context.Escolas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (escola == null) return NotFound();

        return View(escola);
    }

    // POST: Escola/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var escola = await _context.Escolas.FindAsync(id);
        if (escola != null) _context.Escolas.Remove(escola);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EscolaExists(int id)
    {
        return _context.Escolas.Any(e => e.Id == id);
    }
}