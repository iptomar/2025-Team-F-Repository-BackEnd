using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models;
using app_horarios_BackEnd.Data;
using App_horarios_BackEnd.Models.DTO;

namespace app_horarios_BackEnd.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscolaAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public EscolaAPI(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: api/EscolaAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscolaDto>>> GetEscolas()
        {
            var escolas = await _context.Escolas
                .Include(e => e.Localizacao)
                .Include(e => e.Salas)
                .ToListAsync();

            var escolasDto = escolas.Select(e => new EscolaDto
            {
                Id = e.Id,
                Nome = e.Nome,
                Localizacao = new LocalizacaoDto
                {
                    Id = e.Localizacao.Id,
                    Nome = e.Localizacao.Nome,
                    Abreviacao = e.Localizacao.Abreviacao
                },
                Salas = e.Salas?.Select(s => new SalaDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Capacidade = s.Capacidade,
                    Tipo = s.Tipo
                }).ToList()
            });

            return Ok(escolasDto);

        }

        // GET: api/EscolaAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Escola>> GetEscola(int id)
        {
            var escola = await _context.Escolas.FindAsync(id);

            if (escola == null)
            {
                return NotFound();
            }

            return escola;
        }

        // PUT: api/EscolaAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEscola(int id, Escola escola)
        {
            if (id != escola.Id)
            {
                return BadRequest();
            }

            _context.Entry(escola).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EscolaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EscolaAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Escola>> PostEscola(Escola escola)
        {
            _context.Escolas.Add(escola);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEscola", new { id = escola.Id }, escola);
        }

        // DELETE: api/EscolaAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEscola(int id)
        {
            var escola = await _context.Escolas.FindAsync(id);
            if (escola == null)
            {
                return NotFound();
            }

            _context.Escolas.Remove(escola);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EscolaExists(int id)
        {
            return _context.Escolas.Any(e => e.Id == id);
        }
    }
}
