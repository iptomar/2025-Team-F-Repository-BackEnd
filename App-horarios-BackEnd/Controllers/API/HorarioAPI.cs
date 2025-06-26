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
    public class HorarioAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public HorarioAPI(HorarioDbContext context)
        {
            _context = context;
        }

        // GET: api/HorarioAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horario>>> GetHorarios()
        {
            return await _context.Horarios.ToListAsync();
        }

        // GET: api/HorarioAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Horario>> GetHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);

            if (horario == null)
            {
                return NotFound();
            }

            return horario;
        }

        // PUT: api/HorarioAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, Horario horario)
        {
            if (id != horario.Id)
            {
                return BadRequest();
            }

            _context.Entry(horario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioExists(id))
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
        public async Task<ActionResult<Horario>> PostHorario(Horario horario)
        {
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHorario", new { id = horario.Id }, horario);
        }

        // DELETE: api/HorarioAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // ✅ GET - Obter todos os blocos de horário de uma turma
        [HttpGet("turma/{turmaId}")]
        public async Task<ActionResult<IEnumerable<BlocoHorario>>> GetHorarioPorTurma(int turmaId)
        {
            var horario = await _context.Horarios
                .Include(h => h.BlocosHorarios)
                .ThenInclude(bh => bh.BlocoAula)
                .FirstOrDefaultAsync(h => h.TurmaId == turmaId);

            if (horario == null)
                return NotFound("Horário não encontrado para a turma.");

            return Ok(horario.BlocosHorarios);
        }

        [HttpPost("salvar-horario")]
        public async Task<ActionResult> SalvarHorario([FromBody] List<BlocoHorario> blocos)
        {
            if (blocos == null || blocos.Count == 0)
                return BadRequest("Lista de blocos vazia.");

            int blocoAulaId = blocos.First().BlocoAulaId;

            var blocoAula = await _context.BlocosAulas
                .Include(b => b.Turma)
                .FirstOrDefaultAsync(b => b.Id == blocoAulaId);

            if (blocoAula == null)
                return BadRequest("BlocoAula inválido.");

            int turmaId = blocoAula.TurmaId;

            var horario = await _context.Horarios
                .Include(h => h.BlocosHorarios)
                .FirstOrDefaultAsync(h => h.TurmaId == turmaId);

            if (horario == null)
            {
                horario = new Horario { TurmaId = turmaId };
                _context.Horarios.Add(horario);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.BlocosHorarios.RemoveRange(horario.BlocosHorarios);
                await _context.SaveChangesAsync();
            }

            foreach (var bloco in blocos)
            {
                bloco.HorarioId = horario.Id;
                _context.BlocosHorarios.Add(bloco);
            }

            await _context.SaveChangesAsync();
            return Ok("Horário salvo com sucesso.");
        }


        private bool HorarioExists(int id)
        {
            return _context.Horarios.Any(e => e.Id == id);
        }
        
        // POST: api/BlocoHorarioAPI/salvar
        [HttpPost("salvar")]
        public async Task<IActionResult> SalvarHorario([FromBody] HorarioDTO dto)
        {
            if (dto == null || dto.BlocosHorarios == null || dto.BlocosHorarios.Count == 0)
                return BadRequest("Dados inválidos.");

            // Verifica se já existe horário para a turma
            var horarioExistente = await _context.Horarios
                .Include(h => h.BlocosHorarios)
                .FirstOrDefaultAsync(h => h.TurmaId == dto.TurmaId);

            if (horarioExistente != null)
            {
                if (horarioExistente.Bloqueado)
                    return BadRequest("Horário bloqueado para alterações.");

                _context.BlocosHorarios.RemoveRange(horarioExistente.BlocosHorarios);
                await _context.SaveChangesAsync();
            }
            else
            {
                horarioExistente = new Horario
                {
                    TurmaId = dto.TurmaId,
                    BlocosHorarios = new List<BlocoHorario>()
                };
                _context.Horarios.Add(horarioExistente);
                await _context.SaveChangesAsync();
            }


            // Adiciona os blocos recebidos ao horário
            foreach (var b in dto.BlocosHorarios)
            {
                horarioExistente.BlocosHorarios.Add(new BlocoHorario
                {
                    BlocoAulaId = b.BlocoAulaId,
                    DiaSemana = b.DiaSemana,
                    HoraInicio = TimeSpan.Parse(b.HoraInicio),
                    HoraFim = TimeSpan.Parse(b.HoraFim),
                    HorarioId = horarioExistente.Id
                });
            }

            await _context.SaveChangesAsync();
            return Ok("Horário salvo com sucesso.");
        }
        
        [HttpPut("{id}/bloquear")]
        public async Task<IActionResult> BloquearHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
                return NotFound("Horário não encontrado.");

            horario.Bloqueado = true;
            await _context.SaveChangesAsync();

            return Ok("Horário bloqueado com sucesso.");
        }

    }
}
