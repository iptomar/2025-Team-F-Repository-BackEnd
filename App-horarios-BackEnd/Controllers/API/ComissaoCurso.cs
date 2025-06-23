using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models;
using app_horarios_BackEnd.Data;

namespace app_horarios_BackEnd.Controllers.API
{
    public class ComissaoCursoController : Controller
    {
        private readonly HorarioDbContext _context;

        public ComissaoCursoController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: ComissaoCurso
        public async Task<IActionResult> Index()
        {
            var horarioDbContext = _context.ComissoesCurso.Include(c => c.Curso).Include(c => c.Escola).Include(c => c.Secretariado);
            return View(await horarioDbContext.ToListAsync());
        }

        // GET: ComissaoCurso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comissaoCurso = await _context.ComissoesCurso
                .Include(c => c.Curso)
                .Include(c => c.Escola)
                .Include(c => c.Secretariado)
                .FirstOrDefaultAsync(m => m.IdUtilizador == id);
            if (comissaoCurso == null)
            {
                return NotFound();
            }

            return View(comissaoCurso);
        }

        // GET: ComissaoCurso/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id");
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Id");
            ViewData["IdUtilizador"] = new SelectList(_context.Secretariados, "IdUtilizador", "IdUtilizador");
            return View();
        }

        // POST: ComissaoCurso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUtilizador,CursoId,EscolaId")] ComissaoCurso comissaoCurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comissaoCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", comissaoCurso.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Id", comissaoCurso.EscolaId);
            ViewData["IdUtilizador"] = new SelectList(_context.Secretariados, "IdUtilizador", "IdUtilizador", comissaoCurso.IdUtilizador);
            return View(comissaoCurso);
        }

        // GET: ComissaoCurso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comissaoCurso = await _context.ComissoesCurso.FindAsync(id);
            if (comissaoCurso == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", comissaoCurso.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Id", comissaoCurso.EscolaId);
            ViewData["IdUtilizador"] = new SelectList(_context.Secretariados, "IdUtilizador", "IdUtilizador", comissaoCurso.IdUtilizador);
            return View(comissaoCurso);
        }

        // POST: ComissaoCurso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUtilizador,CursoId,EscolaId")] ComissaoCurso comissaoCurso)
        {
            if (id != comissaoCurso.IdUtilizador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comissaoCurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComissaoCursoExists(comissaoCurso.IdUtilizador))
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
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", comissaoCurso.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Id", comissaoCurso.EscolaId);
            ViewData["IdUtilizador"] = new SelectList(_context.Secretariados, "IdUtilizador", "IdUtilizador", comissaoCurso.IdUtilizador);
            return View(comissaoCurso);
        }

        // GET: ComissaoCurso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comissaoCurso = await _context.ComissoesCurso
                .Include(c => c.Curso)
                .Include(c => c.Escola)
                .Include(c => c.Secretariado)
                .FirstOrDefaultAsync(m => m.IdUtilizador == id);
            if (comissaoCurso == null)
            {
                return NotFound();
            }

            return View(comissaoCurso);
        }

        // POST: ComissaoCurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comissaoCurso = await _context.ComissoesCurso.FindAsync(id);
            if (comissaoCurso != null)
            {
                _context.ComissoesCurso.Remove(comissaoCurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComissaoCursoExists(int id)
        {
            return _context.ComissoesCurso.Any(e => e.IdUtilizador == id);
        }
    }
}
