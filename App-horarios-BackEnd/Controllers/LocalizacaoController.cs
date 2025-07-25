using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models;
using app_horarios_BackEnd.Data;
using System.Globalization;
using System.Text;


namespace app_horarios_BackEnd.Controllers
{
    public class LocalizacaoController : Controller
    {
        private readonly HorarioDbContext _context;

        public LocalizacaoController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: LocalizacaoController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Localizacoes.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Pesquisar(string? search)
        {
            var localizacoes = await _context.Localizacoes.ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                // Função local para normalizar (remover acentos e lowercase)
                string Normalizar(string input) =>
                    new string(input.Normalize(NormalizationForm.FormD)
                            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                            .ToArray())
                        .ToLower();

                string searchNormalizado = Normalizar(search);

                localizacoes = localizacoes
                    .Where(l =>
                        Normalizar(l.Nome).Contains(searchNormalizado) ||
                        Normalizar(l.Abreviacao ?? "").Contains(searchNormalizado))
                    .ToList();
            }

            ViewData["Search"] = search;
            return View("Index", localizacoes);
        }



        // GET: LocalizacaoController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localizacao = await _context.Localizacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localizacao == null)
            {
                return NotFound();
            }

            return View(localizacao);
        }

        // GET: LocalizacaoController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocalizacaoController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Abreviacao")] Localizacao localizacao)
        {
            if (!ModelState.IsValid) {
                foreach (var state in ModelState) {
                    foreach (var error in state.Value.Errors) {
                        Console.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}, Exceptoin: {error.Exception}");
                    }
                }
                return View(localizacao);
            }

            _context.Add(localizacao);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Criada sucesso!";
            return RedirectToAction(nameof(Index));
            
        }

        // GET: LocalizacaoController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao == null)
            {
                return NotFound();
            }
            return View(localizacao);
        }

        // POST: LocalizacaoController/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Abreviacao")] Localizacao localizacao)
        {
            if (id != localizacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localizacao);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Localidade atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalizacaoExists(localizacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(localizacao);
        }

        // GET: LocalizacaoController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localizacao = await _context.Localizacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (localizacao == null)
            {
                return NotFound();
            }

            return View(localizacao);
        }

        // POST: LocalizacaoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao != null)
            {
                _context.Localizacoes.Remove(localizacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalizacaoExists(int id)
        {
            return _context.Localizacoes.Any(e => e.Id == id);
        }
    }
}
