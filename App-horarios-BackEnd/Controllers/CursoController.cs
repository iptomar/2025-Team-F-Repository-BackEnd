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
    public class CursoController : Controller
    {
        private readonly HorarioDbContext _context;

        public CursoController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: Curso
        public async Task<IActionResult> Index()
        {
            var horarioDbContext = _context.Cursos.Include(c => c.Escola).Include(c => c.Grau).Include(c => c.Localizacao);
            return View(await horarioDbContext.ToListAsync());
        }

        // GET: Curso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos
                .Include(c => c.Escola)
                .Include(c => c.Grau)
                .Include(c => c.Localizacao) // Adiciona isto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // GET: Curso/Create
        public IActionResult Create()
        {
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome");
            ViewData["GrauId"] = new SelectList(_context.Graus, "Id", "Nome");
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacoes, "Id", "Nome");
            return View();
        }

        // POST: Curso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,EscolaId,GrauId,LocalizacaoId")] Curso curso)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(curso);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Curso criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", curso.EscolaId);
            ViewData["GrauId"] = new SelectList(_context.Graus, "Id", "Nome", curso.GrauId);
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacoes, "Id", "Nome", curso.LocalizacaoId);
            return View(curso);
        }



        // GET: Curso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", curso.EscolaId);
            ViewData["GrauId"] = new SelectList(_context.Graus, "Id", "Nome", curso.GrauId);
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacoes, "Id", "Nome",curso.LocalizacaoId);
            return View(curso);
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,EscolaId,RamoId,GrauId,LocalizacaoId")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Curso atualizada com sucesso.";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.Id))
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
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", curso.EscolaId);
            ViewData["GrauId"] = new SelectList(_context.Graus, "Id", "Nome", curso.GrauId);
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacoes, "Id", "Nome",curso.LocalizacaoId);
            return View(curso);
        }

        // GET: Curso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos
                .Include(c => c.Escola)
                .Include(c => c.Grau)
                .Include(c => c.Localizacao) // Adiciona isto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Curso eliminada com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Pesquisar(string? search)
        {
            var cursos = await _context.Cursos
                .Include(c => c.Escola)
                .Include(c => c.Grau)
                .Include(c => c.Localizacao)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                string Normalizar(string input) =>
                    new string(input.Normalize(NormalizationForm.FormD)
                            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                            .ToArray())
                        .ToLower();

                string termo = Normalizar(search);

                cursos = cursos.Where(c =>
                        c.Id.ToString().Contains(termo) ||
                        Normalizar(c.Nome).Contains(termo) ||
                        Normalizar(c.Grau?.Nome ?? "").Contains(termo) ||
                        Normalizar(c.Escola?.Nome ?? "").Contains(termo) ||
                        Normalizar(c.Localizacao?.Nome ?? "").Contains(termo)
                    )
                    .ToList();
            }

            ViewData["Search"] = search;
            return View("Index", cursos);
        }

        
        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
