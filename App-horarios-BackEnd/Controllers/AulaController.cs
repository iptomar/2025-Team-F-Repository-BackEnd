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
            var horarioDbContext = _context.BlocosAulas
                .Include(b => b.Disciplina)
                .Include(b => b.Sala)
                .Include(b => b.TipoAula)
                .Include(b => b.BlocoAulaProfessores)
                .ThenInclude(bp => bp.Professor); // ✅ Carrega os professores associados
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
                .Include(b => b.Sala)
                .Include(b => b.TipoAula)
                .Include(b => b.BlocoAulaProfessores)
                .ThenInclude(bp => bp.Professor) // ✅ Carrega os professores associados
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
            ViewData["Professores"] = new MultiSelectList(_context.Professores, "Id", "Nome");
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Nome");
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Tipo");
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Nome");
            return View();
        }

        // POST: AulaController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Duracao,DisciplinaId,SalaId,TipoAulaId,TurmaId")] BlocoAula blocoAula, List<int> professorIds)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(blocoAula);
                await _context.SaveChangesAsync();

                foreach (var profId in professorIds)
                {
                    _context.BlocosAulaProfessores.Add(new BlocoAulaProfessor
                    {
                        BlocoAulaId = blocoAula.Id,
                        ProfessorId = profId
                    });
                }
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Aula criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome", blocoAula.DisciplinaId);
            ViewData["Professores"] = new MultiSelectList(_context.Professores, "Id", "Nome", professorIds);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Nome", blocoAula.SalaId);
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Tipo", blocoAula.TipoAulaId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Nome", blocoAula.TurmaId);
            return View(blocoAula);
            
        }

        // GET: AulaController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blocoAula = await _context.BlocosAulas
                .Include(b => b.BlocoAulaProfessores)
                .Include(b => b.Turma) // Caso uses Turma em detalhes
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blocoAula == null)
            {
                return NotFound();
            }

            // Obter IDs dos professores associados
            var professorIds = blocoAula.BlocoAulaProfessores.Select(p => p.ProfessorId).ToList();

            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome", blocoAula.DisciplinaId);
            ViewData["Professores"] = new MultiSelectList(_context.Professores, "Id", "Nome", professorIds);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Nome", blocoAula.SalaId);
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Tipo", blocoAula.TipoAulaId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Nome", blocoAula.TurmaId);

            return View(blocoAula);
        }


        // POST: AulaController/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Duracao,HorarioId,DisciplinaId,SalaId,TipoAulaId,TurmaId")] BlocoAula blocoAula, List<int> professorIds)
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

                    // Remove professores antigos
                    var antigos = _context.BlocosAulaProfessores.Where(bp => bp.BlocoAulaId == blocoAula.Id);
                    _context.BlocosAulaProfessores.RemoveRange(antigos);

                    // Adiciona professores novos
                    foreach (var profId in professorIds)
                    {
                        _context.BlocosAulaProfessores.Add(new BlocoAulaProfessor
                        {
                            BlocoAulaId = blocoAula.Id,
                            ProfessorId = profId
                        });
                    }

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

            // Se der erro de validação, recarregar selects
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome", blocoAula.DisciplinaId);
            ViewData["Professores"] = new MultiSelectList(_context.Professores, "Id", "Nome", professorIds);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Nome", blocoAula.SalaId);
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Tipo", blocoAula.TipoAulaId);
            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Nome", blocoAula.TurmaId);

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
                .Include(b => b.Sala)
                .Include(b => b.TipoAula)
                .Include(b => b.BlocoAulaProfessores)
                .ThenInclude(bp => bp.Professor)
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
            var blocoAula = await _context.BlocosAulas
                .Include(b => b.BlocoAulaProfessores)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (blocoAula != null)
            {
                // Remove associações
                _context.BlocosAulaProfessores.RemoveRange(blocoAula.BlocoAulaProfessores);
                _context.BlocosAulas.Remove(blocoAula);

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Aula eliminada com sucesso.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Pesquisar(string? search)
        {
            var blocos = await _context.BlocosAulas
                .Include(b => b.Disciplina)
                .Include(b => b.Sala)
                .Include(b => b.TipoAula)
                .Include(b => b.BlocoAulaProfessores)
                .ThenInclude(bp => bp.Professor)
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
                        Normalizar(b.Disciplina.Id.ToString() ?? "").Contains(termo) ||
                        Normalizar(b.Disciplina?.Nome ?? "").Contains(termo) ||
                        Normalizar(string.Join(" ", b.BlocoAulaProfessores.Select(p => p.Professor.Nome))) // ✅ Professores múltiplos
                            .Contains(termo) ||
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
