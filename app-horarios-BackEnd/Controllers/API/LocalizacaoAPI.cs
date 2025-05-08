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
    public class LocalizacaoAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public LocalizacaoAPI(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: api/LocalizacaoAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocalizacaoDto>>> GetLocalizacaoDto()
        {
            var localizacoes = await _context.Localizacoes
                .Select(l => new LocalizacaoDto
                {
                    Id = l.Id,
                    Nome = l.Nome,
                    Abreviacao = l.Abreviacao
                })
                .ToListAsync();

            return localizacoes;
        }

        // GET: api/LocalizacaoAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocalizacaoDto>> GetLocalizacaoDto(int id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);

            if (localizacao == null)
                return NotFound();

            var dto = new LocalizacaoDto
            {
                Id = localizacao.Id,
                Nome = localizacao.Nome,
                Abreviacao = localizacao.Abreviacao
            };

            return dto;
        }

        // PUT: api/LocalizacaoAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocalizacaoDto(int id, LocalizacaoDto localizacaoDto)
        {
            if (id != localizacaoDto.Id)
                return BadRequest();

            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao == null)
                return NotFound();

            localizacao.Nome = localizacaoDto.Nome;
            localizacao.Abreviacao = localizacaoDto.Abreviacao;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/LocalizacaoAPI
        [HttpPost]
        public async Task<ActionResult<LocalizacaoDto>> PostLocalizacao(LocalizacaoDto localizacaoDto)
        {
            var localizacao = new Localizacao
            {
                Nome = localizacaoDto.Nome,
                Abreviacao = localizacaoDto.Abreviacao
            };

            _context.Localizacoes.Add(localizacao);
            await _context.SaveChangesAsync();

            localizacaoDto.Id = localizacao.Id;

            return CreatedAtAction(nameof(GetLocalizacaoDto), new { id = localizacao.Id }, localizacaoDto);
        }
        
        

        private bool LocalizacaoDtoExists(int id)
        {
            return _context.LocalizacaoDto.Any(e => e.Id == id);
        }
    }
}
