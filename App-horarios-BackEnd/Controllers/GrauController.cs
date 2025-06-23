using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models;
using app_horarios_BackEnd.Data;

namespace app_horarios_BackEnd.Controllers
{
    public class GrauController : Controller
    {
        private readonly HorarioDbContext _context;

        public GrauController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: Grau
        public async Task<IActionResult> Index()
        {
            return View(await _context.Graus.ToListAsync());
        }

        // GET: Grau/Pesquisar?search=licenciatura
        [HttpGet]
        public async Task<IActionResult> Pesquisar(string search)
        {
            ViewData["Search"] = search;

            var query = _context.Graus.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(g =>
                    g.Nome.ToLower().Contains(search.ToLower()) ||
                    g.Duracao.ToLower().Contains(search.ToLower()));
            }

            var resultados = await query
                .OrderBy(g => g.Nome)
                .ToListAsync();

            return View("Index", resultados); // reutiliza a View Index.cshtml
        }

        // GET: Grau/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var grau = await _context.Graus.FirstOrDefaultAsync(m => m.Id == id);
            if (grau == null)
                return NotFound();

            return View(grau);
        }

        // GET: Grau/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grau/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Duracao")] Grau grau)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grau);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Grau criado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            return View(grau);
        }

        // GET: Grau/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var grau = await _context.Graus.FindAsync(id);
            if (grau == null)
                return NotFound();

            return View(grau);
        }

        // POST: Grau/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Duracao")] Grau grau)
        {
            if (id != grau.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grau);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Grau atualizado com sucesso.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrauExists(grau.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(grau);
        }

        // GET: Grau/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var grau = await _context.Graus.FirstOrDefaultAsync(m => m.Id == id);
            if (grau == null)
                return NotFound();

            return View(grau);
        }

        // POST: Grau/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grau = await _context.Graus.FindAsync(id);
            if (grau != null)
            {
                _context.Graus.Remove(grau);
                await _context.SaveChangesAsync();
            }

                TempData["SuccessMessage"] = "Grau removido com sucesso.";
                return RedirectToAction(nameof(Index));
        }

        private bool GrauExists(int id)
        {
            return _context.Graus.Any(e => e.Id == id);
        }
    }
}
