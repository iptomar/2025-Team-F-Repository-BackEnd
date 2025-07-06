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
    public class UtilizadorController : Controller
    {
        private readonly HorarioDbContext _context;

        public UtilizadorController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: Utilizador
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilizadores.ToListAsync());
        }

        // GET: Utilizador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        // GET: Utilizador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utilizador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,PasswordHash,Tipo")] Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                
                // Verifica se já existe utilizador com o mesmo email
                var existente = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.Email == utilizador.Email);

                if (existente != null)
                {
                    ModelState.AddModelError("Email", "Já existe um utilizador com este email.");
                    return View(utilizador);
                }

// Guarda temporariamente com tipo por defeito
                utilizador.Tipo = "Secretariado"; 
                _context.Utilizadores.Add(utilizador);
                await _context.SaveChangesAsync(); // agora o Id é gerado

// Verifica se está em DiretoresCurso
                var isDiretor = await _context.DiretoresCurso
                    .AnyAsync(d => d.IdUtilizador == utilizador.Id);

                if (isDiretor)
                {
                    utilizador.Tipo = "DiretorCurso";
                }
                else
                {
                    var isComissao = await _context.ComissoesCurso
                        .AnyAsync(c => c.IdUtilizador == utilizador.Id);

                    if (isComissao)
                    {
                        utilizador.Tipo = "ComissaoCurso";
                    }
                }

// Atualiza o tipo se for diferente do padrão
                _context.Update(utilizador);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(utilizador);
        }

        // GET: Utilizador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,PasswordHash,Tipo")] Utilizador utilizador)
        {
            if (id != utilizador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadorExists(utilizador.Id))
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
            return View(utilizador);
        }

        // GET: Utilizador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        // POST: Utilizador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador != null)
            {
                _context.Utilizadores.Remove(utilizador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadorExists(int id)
        {
            return _context.Utilizadores.Any(e => e.Id == id);
        }
    }
}
