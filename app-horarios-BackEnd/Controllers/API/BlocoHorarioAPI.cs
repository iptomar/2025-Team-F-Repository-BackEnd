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
    public class BlocoHorarioAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public BlocoHorarioAPI(HorarioDbContext context)
        {
            _context = context;
        }
        
        // GET: api/BlocoHorarioAPI/porCursoAnoSemestre?cursoId=1&ano=1&semestre=1
        [HttpGet("porCursoAnoSemestre")]
        public async Task<ActionResult<IEnumerable<BlocoPreviewDto>>> GetDisciplinasParaHorario(int cursoId, int ano, int semestre)
        {
            var blocos = await _context.Disciplinas
                .Where(d => d.Ano == ano && d.Semestre == semestre.ToString())
                .Include(d => d.DisciplinaCursoProfessores)
                .ThenInclude(dcp => dcp.Professor)
                .Include(d => d.DisciplinaCursoProfessores)
                .ThenInclude(dcp => dcp.Curso)
                .Where(d => d.DisciplinaCursoProfessores.Any(dcp => dcp.CursoId == cursoId))
                .Select(d => new BlocoPreviewDto
                {
                    NomeDisciplina = d.Nome,
                    TipoAula = d.Tipo ?? "TP",
                    NomeProfessor = d.DisciplinaCursoProfessores
                        .FirstOrDefault(dcp => dcp.CursoId == cursoId).Professor.Nome,
                    NomeSala = "Sala a definir"
                })
                .ToListAsync();

            return blocos;
        }

        // GET: api/BlocoHorarioAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlocoHorarioDto>> GetBlocoHorarioDto(int id)
        {
            var blocoHorarioDto = await _context.BlocoHorarioDto.FindAsync(id);

            if (blocoHorarioDto == null)
            {
                return NotFound();
            }

            return blocoHorarioDto;
        }

        // POST: api/BlocoHorarioAPI
        [HttpPost]
        public async Task<IActionResult> PostBlocosHorario([FromBody] List<BlocoHorarioDto> blocosDto)
        {
            if (blocosDto == null || !blocosDto.Any())
                return BadRequest("Nenhum bloco enviado.");

            foreach (var dto in blocosDto)
            {
                // Cria o bloco de horário
                var bloco = new BlocoHorario
                {
                    DiaSemana = dto.DiaSemana,
                    HoraInicio = TimeOnly.Parse(dto.HoraInicio),
                    HoraFim = TimeOnly.Parse(dto.HoraFim),
                    DisciplinaId = dto.DisciplinaId,
                    SalaId = dto.SalaId,
                    TipoAulaId = dto.TipoAulaId,
                    HorarioId = dto.HorarioId
                };

                _context.BlocosHorario.Add(bloco);
                await _context.SaveChangesAsync(); // bloco.Id é gerado aqui

                // Cria associação com o professor
                var blocoProfessor = new BlocoProfessor
                {
                    BlocoId = bloco.Id,
                    ProfessorId = dto.ProfessorId
                };

                _context.BlocosProfessor.Add(blocoProfessor);
                await _context.SaveChangesAsync();
            }

            return Ok("Blocos de horário criados com sucesso.");
        }


        // DELETE: api/BlocoHorarioAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlocoHorarioDto(int id)
        {
            var blocoHorarioDto = await _context.BlocoHorarioDto.FindAsync(id);
            if (blocoHorarioDto == null)
            {
                return NotFound();
            }

            _context.BlocoHorarioDto.Remove(blocoHorarioDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
