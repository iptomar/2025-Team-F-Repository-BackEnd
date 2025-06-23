using app_horarios_BackEnd.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app_horarios_BackEnd.Controllers
{
    public class ContaController : Controller
    {
        private readonly HorarioDbContext _context;

        // Construtor único e correto
        public ContaController(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: Conta/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Conta/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewData["Erro"] = "Preencha todos os campos.";
                return View();
            }

            var user = await _context.Utilizadores
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);

            if (user != null)
            {
                // Armazenar na sessão
                HttpContext.Session.SetString("UtilizadorEmail", user.Email);
                HttpContext.Session.SetString("UtilizadorTipo", user.Tipo ?? "Sem Tipo");

                return RedirectToAction("Index", "Home");
            }

            ViewData["Erro"] = "Email ou senha inválidos.";
            return View();
        }

        // GET: Conta/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Conta");
        }
    }
}