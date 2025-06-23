using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models;
using app_horarios_BackEnd.Data;

namespace app_horarios_BackEnd.Controllers
{
    public class SalaController : Controller
    {
        private readonly HorarioDbContext _context;

        public SalaController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: SalaController
        public async Task<IActionResult> Index()
        {
            var horarioDbContext = _context.Salas
                .Include(s => s.Escola)
                .Include(s => s.TipoAula);
            return View(await horarioDbContext.ToListAsync());
        }

        // GET: SalaController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas
                .Include(s => s.Escola)
                .Include(s => s.TipoAula) // Adicionado para incluir o tipo de aula
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // GET: SalaController/Create
        public IActionResult Create()
        {
            ViewBag.EscolaId = new SelectList(_context.Escolas, "Id", "Nome");
            ViewBag.TipoAulaId = new SelectList(_context.TiposAula, "Id", "Tipo");
            return View();
        }


        // POST: SalaController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Capacidade,TipoAulaId,EscolaId")] Sala sala)
        {
            var salaJson = System.Text.Json.JsonSerializer.Serialize(sala);
            Console.WriteLine($"POST payload: {salaJson}");

            if (ModelState.IsValid)
            {
                // Verificações adicionais
                sala.Escola = await _context.Escolas.FindAsync(sala.EscolaId);
                sala.TipoAula = await _context.TiposAula.FindAsync(sala.TipoAulaId);

                if (sala.Escola == null)
                    ModelState.AddModelError(string.Empty, "A escola selecionada não é válida.");

                if (sala.TipoAula == null)
                    ModelState.AddModelError(string.Empty, "O tipo de aula selecionado não é válido.");

                // Recarrega os dropdowns com a seleção atual
                ViewBag.EscolaId = new SelectList(_context.Escolas, "Id", "Nome", sala.EscolaId);
                ViewBag.TipoAulaId = new SelectList(_context.TiposAula, "Id", "Tipo", sala.TipoAulaId);

                return View(sala);
            }

            _context.Add(sala);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Sala criada com sucesso!";
            return RedirectToAction(nameof(Index));
        }



        // GET: SalaController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            ViewBag.EscolaId = new SelectList(_context.Escolas, "Id", "Nome", sala.EscolaId);
            ViewBag.TipoAulaId = new SelectList(_context.TiposAula, "Id", "Tipo", sala.TipoAulaId);
            return View(sala);
        }

        // POST: SalaController/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Capacidade,TipoAulaId,EscolaId")] Sala sala)
        {
            if (id != sala.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Sala atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            // Recarrega dropdowns se a model for inválida
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome", sala.EscolaId);
            ViewData["TipoAulaId"] = new SelectList(_context.TiposAula, "Id", "Tipo", sala.TipoAulaId);

            return View(sala);
        }

        // GET: SalaController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas
                    .Include(s => s.Escola)
                    .Include(s => s.TipoAula)
                    .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // POST: SalaController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala != null)
            {
                _context.Salas.Remove(sala);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Sala removido com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        

        // GET: Sala/Pesquisar?search=Laboratório
        [HttpGet]
        public async Task<IActionResult> Pesquisar(string search)
        {
            ViewData["Search"] = search;

            var query = _context.Salas
                .Include(s => s.Escola)
                .Include(s => s.TipoAula) // Inclui o tipo de aula
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                bool isNumero = int.TryParse(search, out int capacidadeNumero);

                query = query.Where(s =>
                    s.Nome.ToLower().Contains(search) ||
                    (s.TipoAula != null && s.TipoAula.Tipo.ToLower().Contains(search)) ||
                    s.Escola.Nome.ToLower().Contains(search) ||
                    (isNumero && s.Capacidade == capacidadeNumero)
                );
            }

            var resultados = await query.OrderBy(s => s.Nome).ToListAsync();

            return View("Index", resultados);
        }


        private bool SalaExists(int id)
        {
            return _context.Salas.Any(e => e.Id == id);
        }
    }
}
