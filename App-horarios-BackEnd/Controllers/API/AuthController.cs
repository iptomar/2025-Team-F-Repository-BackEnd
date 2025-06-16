using app_horarios_BackEnd.Data;
using App_horarios_BackEnd.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app_horarios_BackEnd.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public AuthController(HorarioDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (utilizador == null || utilizador.PasswordHash != request.Password)
            {
                return Unauthorized(new { message = "Email ou password inválido." });
            }

            string tipo;

            bool isDiretor = await _context.DiretoresCurso
                .AnyAsync(d => d.IdUtilizador == utilizador.Id);

            if (isDiretor)
            {
                tipo = "DiretorCurso";
            }
            else
            {
                bool isComissao = await _context.ComissoesCurso
                    .AnyAsync(c => c.IdUtilizador == utilizador.Id);

                tipo = isComissao ? "ComissaoCurso" : "Secretariado";
            }

            // Podes retornar um token JWT aqui se quiseres
            return Ok(new
            {
                utilizador.Id,
                utilizador.Email,
                Tipo = tipo
            });
        }
    }
}
