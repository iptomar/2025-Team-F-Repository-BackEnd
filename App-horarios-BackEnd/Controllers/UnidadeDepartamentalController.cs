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
    public class UnidadeDepartamentalController : Controller
    {
        private readonly HorarioDbContext _context;

        public UnidadeDepartamentalController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: UnidadeDepartamentalController
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnidadesDepartamentais.ToListAsync());
        }

        // GET: UnidadeDepartamentalController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeDepartamental = await _context.UnidadesDepartamentais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadeDepartamental == null)
            {
                return NotFound();
            }

            return View(unidadeDepartamental);
        }

        // GET: UnidadeDepartamentalController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnidadeDepartamentalController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] UnidadeDepartamental unidadeDepartamental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unidadeDepartamental);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Unidade Departamental criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(unidadeDepartamental);
        }

        // GET: UnidadeDepartamentalController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeDepartamental = await _context.UnidadesDepartamentais.FindAsync(id);
            if (unidadeDepartamental == null)
            {
                return NotFound();
            }
            return View(unidadeDepartamental);
        }

        // POST: UnidadeDepartamentalController/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] UnidadeDepartamental unidadeDepartamental)
        {
            if (id != unidadeDepartamental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidadeDepartamental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadeDepartamentalExists(unidadeDepartamental.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Unidade Departamental atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(unidadeDepartamental);
        }

        // GET: UnidadeDepartamentalController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeDepartamental = await _context.UnidadesDepartamentais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadeDepartamental == null)
            {
                return NotFound();
            }

            return View(unidadeDepartamental);
        }

        // POST: UnidadeDepartamentalController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidadeDepartamental = await _context.UnidadesDepartamentais.FindAsync(id);
            if (unidadeDepartamental != null)
            {
                _context.UnidadesDepartamentais.Remove(unidadeDepartamental);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Unidade Departamental removido com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Pesquisar(string? search)
        {
            var unidades = await _context.UnidadesDepartamentais.ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                string Normalizar(string input) =>
                    new string(input.Normalize(NormalizationForm.FormD)
                            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                            .ToArray())
                        .ToLower();

                string searchNormalizado = Normalizar(search);

                unidades = unidades
                    .Where(u =>
                        u.Id.ToString().Contains(searchNormalizado) || // ID como string
                        Normalizar(u.Nome).Contains(searchNormalizado))
                    .ToList();
            }

            ViewData["Search"] = search;
            return View("Index", unidades);
        }


        
        private bool UnidadeDepartamentalExists(int id)
        {
            return _context.UnidadesDepartamentais.Any(e => e.Id == id);
        }
    }
}
