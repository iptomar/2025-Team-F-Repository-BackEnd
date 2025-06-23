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
    public class BlocoHorarioAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public BlocoHorarioAPI(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: api/BlocoHorarioAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlocoHorarioDTO>>> GetBlocoHorarioDTO()
        {
            return await _context.BlocoHorarioDTO.ToListAsync();
        }

        // GET: api/BlocoHorarioAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlocoHorarioDTO>> GetBlocoHorarioDTO(int id)
        {
            var blocoHorarioDTO = await _context.BlocoHorarioDTO.FindAsync(id);

            if (blocoHorarioDTO == null)
            {
                return NotFound();
            }

            return blocoHorarioDTO;
        }

        // PUT: api/BlocoHorarioAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlocoHorarioDTO(int id, BlocoHorarioDTO blocoHorarioDTO)
        {
            if (id != blocoHorarioDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(blocoHorarioDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlocoHorarioDTOExists(id))
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

        // POST: api/BlocoHorarioAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlocoHorarioDTO>> PostBlocoHorarioDTO(BlocoHorarioDTO blocoHorarioDTO)
        {
            _context.BlocoHorarioDTO.Add(blocoHorarioDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlocoHorarioDTO", new { id = blocoHorarioDTO.Id }, blocoHorarioDTO);
        }

        // DELETE: api/BlocoHorarioAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlocoHorarioDTO(int id)
        {
            var blocoHorarioDTO = await _context.BlocoHorarioDTO.FindAsync(id);
            if (blocoHorarioDTO == null)
            {
                return NotFound();
            }

            _context.BlocoHorarioDTO.Remove(blocoHorarioDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlocoHorarioDTOExists(int id)
        {
            return _context.BlocoHorarioDTO.Any(e => e.Id == id);
        }
    }
}
