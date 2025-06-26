using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models.DTO;
using app_horarios_BackEnd.Data;

namespace app_horarios_BackEnd.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public TurmaAPI(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: api/TurmaAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurmaDto>>> GetTurma()
        {
            return await _context.Turmas
                .Include(t => t.Curso)
                .Select(t => new TurmaDto
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    NumeroAlunos = t.NumeroAlunos,
                    Aberta = t.Aberta,
                    CursoId = t.CursoId
                })
                .ToListAsync();
        }
        
        [HttpPut("turma/{id}/bloquear")]
        public async Task<IActionResult> BloquearTurma(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            if (turma == null)
                return NotFound("Turma não encontrada.");

            turma.Aberta = false;
            await _context.SaveChangesAsync();

            return Ok("Turma bloqueada com sucesso.");
        }

        
    }
}
