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
    public class SecretariadoController : Controller
    {
        private readonly HorarioDbContext _context;

        public SecretariadoController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: Secretariado
        public async Task<IActionResult> Index()
        {
            var horarioDbContext = _context.Secretariados.Include(s => s.Curso).Include(s => s.Escola);
            return View(await horarioDbContext.ToListAsync());
        }

        // GET: Secretariado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretariado = await _context.Secretariados
                .Include(s => s.Curso)
                .Include(s => s.Escola)
                .FirstOrDefaultAsync(m => m.IdUtilizador == id);
            if (secretariado == null)
            {
                return NotFound();
            }

            return View(secretariado);
        }

        // GET: Secretariado/Create
        public IActionResult Create()
        {
            
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome");
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome");
            return View();
        }

        // POST: Secretariado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUtilizador,Nome,Email,EscolaId,CursoId")] Secretariado secretariado)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(secretariado);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Membro criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            
            // Repopular os dropdowns em caso de erro
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome", secretariado.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", secretariado.EscolaId);
            return View(secretariado);
        }

        // GET: Secretariado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretariado = await _context.Secretariados.FindAsync(id);
            if (secretariado == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome", secretariado.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", secretariado.EscolaId);
            return View(secretariado);
        }

        // POST: Secretariado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUtilizador,Nome,Email,EscolaId,CursoId")] Secretariado secretariado)
        {
            if (id != secretariado.IdUtilizador)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(secretariado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecretariadoExists(secretariado.IdUtilizador))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Membro atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome", secretariado.CursoId);
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", secretariado.EscolaId);
            return View(secretariado);
        }

        // GET: Secretariado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretariado = await _context.Secretariados
                .Include(s => s.Curso)
                .Include(s => s.Escola)
                .FirstOrDefaultAsync(m => m.IdUtilizador == id);
            if (secretariado == null)
            {
                return NotFound();
            }

            return View(secretariado);
        }

        // POST: Secretariado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var secretariado = await _context.Secretariados.FindAsync(id);
            if (secretariado != null)
            {
                _context.Secretariados.Remove(secretariado);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Membro removido com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Pesquisar(string search)
        {
            var query = _context.Secretariados
                .Include(s => s.Escola)
                .Include(s => s.Curso)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s =>
                    s.Nome.Contains(search) ||
                    s.Email.Contains(search) ||
                    s.Escola.Nome.Contains(search) ||
                    s.Curso.Nome.Contains(search) ||
                    s.IdUtilizador.ToString().Contains(search));
            }

            ViewData["Search"] = search;
            var resultados = await query.ToListAsync();
            return View("Index", resultados);
        }

        
        private bool SecretariadoExists(int id)
        {
            return _context.Secretariados.Any(e => e.IdUtilizador == id);
        }
    }
}
