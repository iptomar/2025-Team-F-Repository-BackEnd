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
    public class DisciplinaController : Controller
    {
        private readonly HorarioDbContext _context;

        public DisciplinaController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: Disciplina
        public async Task<IActionResult> Index()
        {
            var horarioDbContext = _context.Disciplinas
                .Include(d => d.Curso)
                .Include(d => d.Escola); 
            return View(await horarioDbContext.ToListAsync());
        }

        // GET: Disciplina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest(); // Melhor prática para parâmetros nulos
            }

            var disciplina = await _context.Disciplinas
                .Include(d => d.Curso)
                .Include(d => d.Escola)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }


        // GET: Disciplina/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome");
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome");
            return View();
        }

        // POST: Disciplina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Ano,Semestre,Tipo,HorasTeorica,HorasPratica,HorasTp,HorasSeminario,HorasLaboratorio,HorasCampo,HorasOrientacao,HorasEstagio,HorasOutras,CursoId,EscolaId")] Disciplina disciplina)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(disciplina);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Disciplina criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            // Repopular os dropdowns em caso de erro
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome", disciplina.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", disciplina.EscolaId);
            return View(disciplina);
        }

        // GET: Disciplina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina == null)
            {
                return NotFound();
            }
            
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome",disciplina.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome",disciplina.EscolaId);
            return View(disciplina);
        }

        // POST: Disciplina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,PlanoId,Ano,Semestre,Tipo,HorasTeorica,HorasPratica,HorasTp,HorasSeminario,HorasLaboratorio,HorasCampo,HorasOrientacao,HorasEstagio,HorasOutras,CursoId,EscolaId")] Disciplina disciplina)
        {
            if (id != disciplina.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplina);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Disciplina atualizada com sucesso.";

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinaExists(disciplina.Id))
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

            // Repopular dropdowns em caso de erro
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome", disciplina.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", disciplina.EscolaId);

            return View(disciplina);
        }


        // GET: Disciplina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplinas
                .Include(d => d.Curso)
                .Include(d => d.Escola)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        // POST: Disciplina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);
    
            if (disciplina == null)
            {
                return NotFound();
            }

            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Disciplina eliminada com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Pesquisar(string? search)
        {
            var disciplinas = await _context.Disciplinas
                .Include(d => d.Curso)
                .Include(d => d.Escola)
                .ToListAsync(); // Força execução no banco antes de usar a função local

            if (!string.IsNullOrWhiteSpace(search))
            {
                // Função para normalizar texto
                string Normalizar(string input) =>
                    new string(input.Normalize(NormalizationForm.FormD)
                            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                            .ToArray())
                        .ToLower();

                string searchNormalizado = Normalizar(search);

                disciplinas = disciplinas
                    .Where(d =>
                        Normalizar(d.Nome).Contains(searchNormalizado) ||
                        Normalizar(d.Ano.ToString()).Contains(searchNormalizado) ||
                        Normalizar(d.Semestre.ToString()).Contains(searchNormalizado) ||
                        Normalizar(d.Tipo ?? "").Contains(searchNormalizado) ||
                        Normalizar(d.Curso?.Nome ?? "").Contains(searchNormalizado) ||
                        Normalizar(d.Escola?.Nome ?? "").Contains(searchNormalizado)
                    )
                    .ToList();
            }

            ViewData["Search"] = search;
            return View("Index", disciplinas);
        }


        
        private bool DisciplinaExists(int id)
        {
            return _context.Disciplinas.Any(e => e.Id == id);
        }
    }

    
}
