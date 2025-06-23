using app_horarios_BackEnd.Data;
using App_horarios_BackEnd.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app_horarios_BackEnd.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public LoginAPI(HorarioDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.PasswordHash == request.Password);

            if (utilizador == null)
            {
                return Unauthorized(new { message = "Credenciais inválidas" });
            }

            return Ok(new
            {
                utilizador.Id,
                utilizador.Email,
                utilizador.Tipo
            });
        }
    }
}