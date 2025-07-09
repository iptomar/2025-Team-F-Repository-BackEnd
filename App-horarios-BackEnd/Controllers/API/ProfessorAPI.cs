using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models.DTO;
using App_horarios_BackEnd.Models;
using app_horarios_BackEnd.Data;

namespace app_horarios_BackEnd.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public ProfessorAPI(HorarioDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/ProfessorAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessorDto>>> GetProfessores()
        {
            var professores = await _context.Professores
                .Include(p => p.UnidadeDepartamental)
                .Include(p => p.Categoria)
                .Select(p => new ProfessorDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Email = p.Email,
                    UnidadeDepartamental = p.UnidadeDepartamental.Nome,
                    Categoria = p.Categoria.Nome
                })
                .ToListAsync();

            return Ok(professores);
        }

        // ✅ GET: api/ProfessorAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessorDto>> GetProfessor(int id)
        {
            var professor = await _context.Professores
                .Include(p => p.UnidadeDepartamental)
                .Include(p => p.Categoria)
                .Where(p => p.Id == id)
                .Select(p => new ProfessorDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Email = p.Email,
                    UnidadeDepartamental = p.UnidadeDepartamental.Nome,
                    Categoria = p.Categoria.Nome
                })
                .FirstOrDefaultAsync();

            if (professor == null)
            {
                return NotFound();
            }

            return Ok(professor);
        }

        // 🚫 Remover PUT, POST e DELETE em DTO
        // Se quiser, cria endpoints específicos na entidade Professor

        // Exemplo de verificação de existência (caso precises para outras operações)
        private bool ProfessorExists(int id)
        {
            return _context.Professores.Any(e => e.Id == id);
        }
    }
}
