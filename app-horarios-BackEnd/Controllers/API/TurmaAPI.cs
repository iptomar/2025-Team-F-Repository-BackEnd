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

        // GET: api/TurmaAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TurmaDto>> GetTurmaDto(int id)
        {
            var turmaDto = await _context.TurmaDto.FindAsync(id);

            if (turmaDto == null)
            {
                return NotFound();
            }

            return turmaDto;
        }

        // PUT: api/TurmaAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurmaDto(int id, TurmaDto turmaDto)
        {
            if (id != turmaDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(turmaDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurmaDtoExists(id))
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

        // POST: api/TurmaAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TurmaDto>> PostTurmaDto(TurmaDto turmaDto)
        {
            _context.TurmaDto.Add(turmaDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTurmaDto", new { id = turmaDto.Id }, turmaDto);
        }

        // DELETE: api/TurmaAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurmaDto(int id)
        {
            var turmaDto = await _context.TurmaDto.FindAsync(id);
            if (turmaDto == null)
            {
                return NotFound();
            }

            _context.TurmaDto.Remove(turmaDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TurmaDtoExists(int id)
        {
            return _context.TurmaDto.Any(e => e.Id == id);
        }
    }
}
