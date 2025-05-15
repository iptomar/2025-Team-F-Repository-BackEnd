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
    public class CursoAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public CursoAPI(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: api/CursoAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoDto>>> GetCursoDto()
        {
            var cursos = await _context.Cursos
                .Include(c => c.Escola)
                .Include(c => c.Grau)
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Tipo = c.Grau != null ? c.Grau.Nome : "Sem Grau",
                    IdEscola = c.Escola.Id
                })
                .ToListAsync();

            return cursos;
        }

        // GET: api/CursoAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDto>> GetCursoDto(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Escola)
                .Include(c => c.Grau)
                .Where(c => c.Id == id)
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Tipo = c.Grau != null ? c.Grau.Nome : "Sem Grau",
                    IdEscola = c.Escola.Id
                })
                .FirstOrDefaultAsync();

            if (curso == null)
                return NotFound();

            return curso;
        }

        // PUT: api/CursoAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCursoDto(int id, CursoDto cursoDto)
        {
            if (id != cursoDto.Id)
                return BadRequest();

            var curso = await _context.Cursos
                .Include(c => c.Grau)
                .Include(c => c.Escola)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null)
                return NotFound();

            curso.Nome = cursoDto.Nome;
            curso.GrauId = await ObterGrauIdPorNome(cursoDto.Tipo);
            curso.EscolaId = cursoDto.IdEscola;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/CursoAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CursoDto>> PostCursoDto(CursoDto cursoDto)
        {
            var curso = new Curso
            {
                Nome = cursoDto.Nome,
                GrauId = await ObterGrauIdPorNome(cursoDto.Tipo),
                EscolaId = cursoDto.IdEscola
            };

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            cursoDto.Id = curso.Id;
            return CreatedAtAction(nameof(GetCursoDto), new { id = curso.Id }, cursoDto);
        }

        // DELETE: api/CursoAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCursoDto(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
                return NotFound();

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<int> ObterGrauIdPorNome(string designacao)
        {
            var grau = await _context.Graus.FirstOrDefaultAsync(g => g.Nome == designacao);
            if (grau == null)
                throw new Exception("Grau não encontrado.");
            return grau.Id;
        }

        private async Task<int> ObterEscolaIdPorNome(string nome)
        {
            var escola = await _context.Escolas.FirstOrDefaultAsync(e => e.Nome == nome);
            if (escola == null)
                throw new Exception("Escola não encontrada.");
            return escola.Id;
        }
        
        
    }
}
