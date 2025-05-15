
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
                .Where(d => d.Ano == ano && d.Semestre == semestre)
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

        [HttpGet("por-curso/{cursoId}/ano/{ano}/semestre/{semestre}")]
        public async Task<ActionResult<IEnumerable<BlocoHorarioVisualDto>>> GetBlocosPorCursoESemestre(int cursoId, int semestre , int ano)
        {
            var blocos = await _context.BlocosHorario
                .Include(b => b.Disciplina).ThenInclude(d => d.DisciplinaCursoProfessores)
                .Include(b => b.Sala)
                .Include(b => b.TipoAula)
                .Include(b => b.Horario)
                .Include(b => b.BlocosProfessor).ThenInclude(bp => bp.Professor)
                .Where(b => b.Disciplina.Ano == ano && b.Disciplina.Semestre == semestre &&
                            b.Disciplina.DisciplinaCursoProfessores.Any(dcp => dcp.CursoId == cursoId))
                .Select(b => new BlocoHorarioVisualDto
                {
                    Id = b.Id,
                    DiaSemana = b.DiaSemana,
                    HoraInicio = b.HoraInicio.ToString(),
                    HoraFim = b.HoraFim.ToString(),
                    Disciplina = b.Disciplina.Nome,
                    Sala = b.Sala.Nome,
                    Professor = b.BlocosProfessor.Select(bp => bp.Professor.Nome).FirstOrDefault(),
                    TipoAula = b.TipoAula.Tipo,
                    Horas = b.HoraFim.Hour - b.HoraInicio.Hour
                })
                .ToListAsync();

            return Ok(blocos);
        }




        // POST: api/BlocoHorarioAPI
        [HttpPost]
        public async Task<ActionResult> PostBlocoHorario(BlocoHorarioDto dto)
        {
            // Cria o bloco
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
            await _context.SaveChangesAsync(); // salva para gerar ID

            // Cria a ligação com o professor
            var blocoProfessor = new BlocoProfessor
            {
                BlocoHorarioId = bloco.Id,
                ProfessorId = dto.ProfessorId
            };

            _context.BlocosProfessor.Add(blocoProfessor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostBlocoHorario), new { id = bloco.Id }, dto);
        }
        

        
    }
}
