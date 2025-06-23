using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models.DTO;
using app_horarios_BackEnd.Data;
using App_horarios_BackEnd.Models;

namespace app_horarios_BackEnd.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public SalaAPI(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: api/SalaAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaDto>>> GetSala()
        {
            return await _context.Salas
                .Include(s => s.Escola)
                .Select(s => new SalaDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Capacidade = s.Capacidade,
                    Tipo = s.TipoAula.Tipo,
                    NomeEscola = s.Escola.Nome
                })
                .ToListAsync();
        }

        // GET: api/SalaAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaDto>> GetSalaDto(int id)
        {
            var sala = await _context.Salas
                .Include(s => s.Escola)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sala == null)
                return NotFound();

            return new SalaDto
            {
                Id = sala.Id,
                Nome = sala.Nome,
                Capacidade = sala.Capacidade,
                Tipo = sala.TipoAula.Tipo,
                NomeEscola = sala.Escola.Nome
            };
        }

        [HttpPost]
        public async Task<ActionResult<SalaDto>> PostSala(SalaDto dto)
        {
            var escola = await _context.Escolas.FirstOrDefaultAsync(e => e.Nome == dto.NomeEscola);
            if (escola == null) return BadRequest("Escola não encontrada.");

            var sala = new Sala
            {
                Nome = dto.Nome,
                TipoAulaId = dto.Id,
                Capacidade = dto.Capacidade,
                EscolaId = escola.Id
            };

            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();

            dto.Id = sala.Id;
            return CreatedAtAction(nameof(GetSala), new { id = sala.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSala(int id, SalaDto dto)
        {
            var sala = await _context.Salas.FirstOrDefaultAsync(s => s.Id == id);
            if (sala == null) return NotFound();

            var escola = await _context.Escolas.FirstOrDefaultAsync(e => e.Nome == dto.NomeEscola);
            if (escola == null) return BadRequest("Escola não encontrada.");

            sala.Nome = dto.Nome;
            sala.TipoAulaId = dto.Id;
            sala.Capacidade = dto.Capacidade;
            sala.EscolaId = escola.Id;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null) return NotFound();

            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
