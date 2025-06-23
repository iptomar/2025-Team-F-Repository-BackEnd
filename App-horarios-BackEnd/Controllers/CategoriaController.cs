using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models;
using app_horarios_BackEnd.Data;

namespace app_horarios_BackEnd.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly HorarioDbContext _context;

        public CategoriaController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriasDocentes.ToListAsync());
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaDocente = await _context.CategoriasDocentes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaDocente == null)
            {
                return NotFound();
            }

            return View(categoriaDocente);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] CategoriaDocente categoriaDocente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaDocente);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Categoria criada com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaDocente);
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaDocente = await _context.CategoriasDocentes.FindAsync(id);
            if (categoriaDocente == null)
            {
                return NotFound();
            }
            return View(categoriaDocente);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] CategoriaDocente categoriaDocente)
        {
            if (id != categoriaDocente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaDocente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaDocenteExists(categoriaDocente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Categoria atualizada com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaDocente);
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaDocente = await _context.CategoriasDocentes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaDocente == null)
            {
                return NotFound();
            }

            return View(categoriaDocente);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaDocente = await _context.CategoriasDocentes.FindAsync(id);
            if (categoriaDocente != null)
            {
                _context.CategoriasDocentes.Remove(categoriaDocente);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Categoria removido com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Pesquisar(string? search)
        {
            var categorias = await _context.CategoriasDocentes.ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                string Normalizar(string input) =>
                    new string(input.Normalize(NormalizationForm.FormD)
                        .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                        .ToArray()).ToLower();

                string termo = Normalizar(search);

                categorias = categorias.Where(c =>
                    Normalizar(c.Nome).Contains(termo) ||
                    c.Id.ToString().Contains(termo)).ToList();
            }

            ViewData["Search"] = search;
            return View("Index", categorias);
        }

        
        private bool CategoriaDocenteExists(int id)
        {
            return _context.CategoriasDocentes.Any(e => e.Id == id);
        }
    }
}
