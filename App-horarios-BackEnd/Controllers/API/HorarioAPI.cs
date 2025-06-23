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
    public class HorarioAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public HorarioAPI(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: api/HorarioAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioDTO>>> GetHorarioDTO()
        {
            return await _context.HorarioDTO.ToListAsync();
        }

        // GET: api/HorarioAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioDTO>> GetHorarioDTO(int id)
        {
            var horarioDTO = await _context.HorarioDTO.FindAsync(id);

            if (horarioDTO == null)
            {
                return NotFound();
            }

            return horarioDTO;
        }

        // PUT: api/HorarioAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorarioDTO(int id, HorarioDTO horarioDTO)
        {
            if (id != horarioDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(horarioDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioDTOExists(id))
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

        // POST: api/HorarioAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HorarioDTO>> PostHorarioDTO(HorarioDTO horarioDTO)
        {
            _context.HorarioDTO.Add(horarioDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHorarioDTO", new { id = horarioDTO.Id }, horarioDTO);
        }

        // DELETE: api/HorarioAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorarioDTO(int id)
        {
            var horarioDTO = await _context.HorarioDTO.FindAsync(id);
            if (horarioDTO == null)
            {
                return NotFound();
            }

            _context.HorarioDTO.Remove(horarioDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HorarioDTOExists(int id)
        {
            return _context.HorarioDTO.Any(e => e.Id == id);
        }
    }
}
