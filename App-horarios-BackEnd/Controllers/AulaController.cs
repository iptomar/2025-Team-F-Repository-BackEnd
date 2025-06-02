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
    public class AulaController : Controller
    {
        private readonly HorarioDbContext _context;

        public AulaController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: AulaController
        public async Task<IActionResult> Index()
        {
            var horarioDbContext = _context.BlocosAulas.Include(b => b.Disciplina).Include(b => b.Horario).Include(b => b.Professor).Include(b => b.Sala).Include(b => b.TipoAula);
            return View(await horarioDbContext.ToListAsync());
        }

        // GET: AulaController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blocoAula = await _context.BlocosAulas
                .Include(b => b.Disciplina)
                .Include(b => b.Horario)
                .Include(b => b.Professor)
                .Include(b => b.Sala)
                .Include(b => b.TipoAula)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blocoAula == null)
            {
                return NotFound();
            }

            return View(blocoAula);
        }

        // GET: AulaController/Create
        public IActionResult Create()
        {
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Id");
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id");
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Id");
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Id");
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Id");
            return View();
        }

        // POST: AulaController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiaSemana,Duracao,HorarioId,DisciplinaId,SalaId,TipoAulaId,ProfessorId")] BlocoAula blocoAula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blocoAula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Id", blocoAula.DisciplinaId);
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", blocoAula.HorarioId);
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Id", blocoAula.ProfessorId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Id", blocoAula.SalaId);
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Id", blocoAula.TipoAulaId);
            return View(blocoAula);
        }

        // GET: AulaController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blocoAula = await _context.BlocosAulas.FindAsync(id);
            if (blocoAula == null)
            {
                return NotFound();
            }
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Id", blocoAula.DisciplinaId);
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", blocoAula.HorarioId);
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Id", blocoAula.ProfessorId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Id", blocoAula.SalaId);
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Id", blocoAula.TipoAulaId);
            return View(blocoAula);
        }

        // POST: AulaController/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DiaSemana,Duracao,HorarioId,DisciplinaId,SalaId,TipoAulaId,ProfessorId")] BlocoAula blocoAula)
        {
            if (id != blocoAula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blocoAula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlocoAulaExists(blocoAula.Id))
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
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Id", blocoAula.DisciplinaId);
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", blocoAula.HorarioId);
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Id", blocoAula.ProfessorId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Id", blocoAula.SalaId);
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Id", blocoAula.TipoAulaId);
            return View(blocoAula);
        }

        // GET: AulaController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blocoAula = await _context.BlocosAulas
                .Include(b => b.Disciplina)
                .Include(b => b.Horario)
                .Include(b => b.Professor)
                .Include(b => b.Sala)
                .Include(b => b.TipoAula)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blocoAula == null)
            {
                return NotFound();
            }

            return View(blocoAula);
        }

        // POST: AulaController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blocoAula = await _context.BlocosAulas.FindAsync(id);
            if (blocoAula != null)
            {
                _context.BlocosAulas.Remove(blocoAula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlocoAulaExists(int id)
        {
            return _context.BlocosAulas.Any(e => e.Id == id);
        }
    }
}
