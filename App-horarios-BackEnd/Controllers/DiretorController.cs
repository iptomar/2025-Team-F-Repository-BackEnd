using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models;
using app_horarios_BackEnd.Data;

namespace app_horarios_BackEnd.Controllers
{
    public class DiretorController : Controller
    {
        private readonly HorarioDbContext _context;

        public DiretorController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: Diretor
        public async Task<IActionResult> Index()
        {
            var horarioDbContext = _context.DiretoresCurso.Include(d => d.Curso).Include(d => d.Escola);
            return View(await horarioDbContext.ToListAsync());
        }

        // GET: Diretor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorCurso = await _context.DiretoresCurso
                .Include(d => d.Curso)
                .Include(d => d.Escola)
                .FirstOrDefaultAsync(m => m.IdUtilizador == id);
            if (diretorCurso == null)
            {
                return NotFound();
            }

            return View(diretorCurso);
        }

        // GET: Diretor/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id");
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Id");
            return View();
        }

        // POST: Diretor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EscolaId,CursoId,IdUtilizador")] DiretorCurso diretorCurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diretorCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", diretorCurso.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Id", diretorCurso.EscolaId);
            return View(diretorCurso);
        }

        // GET: Diretor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorCurso = await _context.DiretoresCurso.FindAsync(id);
            if (diretorCurso == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", diretorCurso.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Id", diretorCurso.EscolaId);
            return View(diretorCurso);
        }

        // POST: Diretor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EscolaId,CursoId,IdUtilizador")] DiretorCurso diretorCurso)
        {
            if (id != diretorCurso.IdUtilizador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diretorCurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiretorCursoExists(diretorCurso.IdUtilizador))
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
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", diretorCurso.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Id", diretorCurso.EscolaId);
            return View(diretorCurso);
        }

        // GET: Diretor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diretorCurso = await _context.DiretoresCurso
                .Include(d => d.Curso)
                .Include(d => d.Escola)
                .FirstOrDefaultAsync(m => m.IdUtilizador == id);
            if (diretorCurso == null)
            {
                return NotFound();
            }

            return View(diretorCurso);
        }

        // POST: Diretor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diretorCurso = await _context.DiretoresCurso.FindAsync(id);
            if (diretorCurso != null)
            {
                _context.DiretoresCurso.Remove(diretorCurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiretorCursoExists(int id)
        {
            return _context.DiretoresCurso.Any(e => e.IdUtilizador == id);
        }
    }
}
