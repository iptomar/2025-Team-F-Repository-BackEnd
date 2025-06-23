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
            var horarioDbContext = _context.BlocosAulas.Include(b => b.Disciplina).Include(b => b.Professor).Include(b => b.Sala).Include(b => b.TipoAula);
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
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Nome");
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Nome");
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Tipo");
            return View();
        }

        // POST: AulaController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Duracao,DisciplinaId,SalaId,TipoAulaId,ProfessorId")] BlocoAula blocoAula)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(blocoAula);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Aula criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome", blocoAula.DisciplinaId);
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Nome", blocoAula.ProfessorId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Nome", blocoAula.SalaId);
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Tipo", blocoAula.TipoAulaId);
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
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome", blocoAula.DisciplinaId);
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Nome", blocoAula.ProfessorId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Nome", blocoAula.SalaId);
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Tipo", blocoAula.TipoAulaId);
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

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(blocoAula);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Aula atualizada com sucesso.";

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
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome", blocoAula.DisciplinaId);
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Nome", blocoAula.ProfessorId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Nome", blocoAula.SalaId);
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Tipo", blocoAula.TipoAulaId);
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
            TempData["SuccessMessage"] = "Aula eliminada com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Pesquisar(string? search)
        {
            var blocos = await _context.BlocosAulas
                .Include(b => b.Disciplina)
                .Include(b => b.Professor)
                .Include(b => b.Sala)
                .Include(b => b.TipoAula)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                string Normalizar(string input) =>
                    new string(input.Normalize(NormalizationForm.FormD)
                        .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                        .ToArray()).ToLower();

                string termo = Normalizar(search);

                blocos = blocos
                    .Where(b =>
                        Normalizar(b.Disciplina?.Nome ?? "").Contains(termo) ||
                        Normalizar(b.Professor?.Nome ?? "").Contains(termo) ||
                        Normalizar(b.Sala?.Nome ?? "").Contains(termo) ||
                        Normalizar(b.TipoAula?.Tipo ?? "").Contains(termo) ||
                        b.Duracao.ToString().Contains(termo))
                    .ToList();
            }

            ViewData["Search"] = search;
            return View("Index", blocos);
        }
        private bool BlocoAulaExists(int id)
        {
            return _context.BlocosAulas.Any(e => e.Id == id);
        }
    }
}
