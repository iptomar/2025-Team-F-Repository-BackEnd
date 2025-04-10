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
    public class CursoAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public CursoAPI(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: api/CursoAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoDto>>> GetCursos()
        {
            var cursos = await _context.Cursos
                .Include(c => c.Escola)
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Tipo = c.Tipo,
                    NomeEscola = c.Escola.Nome
                })
                .ToListAsync();

            return cursos;
        }

        // GET: api/CursoAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        // PUT: api/CursoAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest();
            }

            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
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

        // POST: api/CursoAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurso", new { id = curso.Id }, curso);
        }

        // DELETE: api/CursoAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
