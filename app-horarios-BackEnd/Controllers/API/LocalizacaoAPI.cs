using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models;
using app_horarios_BackEnd.Data;

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
        public async Task<ActionResult<IEnumerable<Localizacao>>> GetLocalizacoes()
        {
            return await _context.Localizacoes.ToListAsync();
        }

        // GET: api/LocalizacaoAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>> GetLocalizacao(int id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);

            if (localizacao == null)
            {
                return NotFound();
            }

            return localizacao;
        }

        // PUT: api/LocalizacaoAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocalizacao(int id, Localizacao localizacao)
        {
            if (id != localizacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(localizacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalizacaoExists(id))
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

        // POST: api/LocalizacaoAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Localizacao>> PostLocalizacao(Localizacao localizacao)
        {
            _context.Localizacoes.Add(localizacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocalizacao", new { id = localizacao.Id }, localizacao);
        }

        // DELETE: api/LocalizacaoAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocalizacao(int id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao == null)
            {
                return NotFound();
            }

            _context.Localizacoes.Remove(localizacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocalizacaoExists(int id)
        {
            return _context.Localizacoes.Any(e => e.Id == id);
        }
    }
}
