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
                .Include(e => e.Cursos).ThenInclude(c => c.Grau)
                .Include(e => e.Salas)
                .Select(e => new EscolaDto
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    Localizacao = new LocalizacaoDto
                    {
                        Id = e.Localizacao.Id,
                        Nome = e.Localizacao.Nome,
                        Abreviacao = e.Localizacao.Abreviacao
                    },
                    Cursos = e.Cursos.Select(c => new CursoDto
                    {
                        Id = c.Id,
                        Nome = c.Nome,
                        Tipo = c.Grau.Nome,
                        IdEscola = e.Id
                    }).ToList(),
                    Salas = e.Salas.Select(s => new SalaDto
                    {
                        Id = s.Id,
                        Nome = s.Nome,
                        Capacidade = s.Capacidade,
                        Tipo = s.Tipo,
                        NomeEscola = e.Nome
                    }).ToList()
                })
                .ToListAsync();

            return escolas;
        }

        // GET: api/EscolaAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EscolaDto>> GetEscola(int id)
        {
            var escola = await _context.Escolas
                .Include(e => e.Localizacao)
                .Include(e => e.Cursos).ThenInclude(c => c.Grau)
                .Include(e => e.Salas)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (escola == null)
                return NotFound();

            var dto = new EscolaDto
            {
                Id = escola.Id,
                Nome = escola.Nome,
                Localizacao = new LocalizacaoDto
                {
                    Id = escola.Localizacao.Id,
                    Nome = escola.Localizacao.Nome,
                    Abreviacao = escola.Localizacao.Abreviacao
                },
                Cursos = escola.Cursos.Select(c => new CursoDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Tipo = c.Grau.Nome,
                    IdEscola = escola.Id
                }).ToList(),
                Salas = escola.Salas.Select(s => new SalaDto
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Tipo = s.Tipo,
                    Capacidade = s.Capacidade,
                    NomeEscola = escola.Nome
                }).ToList()
            };

            return dto;
        }

        // POST: api/EscolaAPI
        [HttpPost]
        public async Task<ActionResult<EscolaDto>> PostEscola(EscolaDto escolaDto)
        {
            var escola = new Escola
            {
                Nome = escolaDto.Nome,
                LocalizacaoId = escolaDto.Localizacao.Id
            };

            _context.Escolas.Add(escola);
            await _context.SaveChangesAsync();

            escolaDto.Id = escola.Id;
            return CreatedAtAction(nameof(GetEscola), new { id = escola.Id }, escolaDto);
        }

        // PUT: api/EscolaAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEscola(int id, EscolaDto escolaDto)
        {
            if (id != escolaDto.Id)
                return BadRequest();

            var escola = await _context.Escolas.FindAsync(id);
            if (escola == null)
                return NotFound();

            escola.Nome = escolaDto.Nome;
            escola.LocalizacaoId = escolaDto.Localizacao.Id;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/EscolaAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEscola(int id)
        {
            var escola = await _context.Escolas.FindAsync(id);
            if (escola == null)
                return NotFound();

            _context.Escolas.Remove(escola);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
