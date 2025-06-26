
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models.DTO;
using app_horarios_BackEnd.Data;
using App_horarios_BackEnd.Models;

namespace app_horarios_BackEnd.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlocoAulaAPI : ControllerBase
    {
        private readonly HorarioDbContext _context;

        public BlocoAulaAPI(HorarioDbContext context)
        {
            _context = context;
        }
        
       

        [HttpGet("por-curso/{cursoId}/ano/{ano}/semestre/{semestre}")]
        public async Task<ActionResult<IEnumerable<BlocoPreviewDto>>> GetBlocosFiltrados(int cursoId, int ano, string semestre)
        {
            var blocos = await _context.BlocosAulas
                .Include(b => b.Disciplina)
                .ThenInclude(d => d.DisciplinaCursoProfessor)
                .Include(b => b.Professor)
                .Include(b => b.Sala)
                .Include(b => b.TipoAula)
                .Where(b =>
                    b.Disciplina != null &&
                    b.Disciplina.Ano == ano &&
                    b.Disciplina.Semestre == semestre &&
                    b.Disciplina.DisciplinaCursoProfessor.Any(dcp => dcp.CursoId == cursoId)
                )
                .Select(b => new BlocoPreviewDto
                {
                    
                    Id = b.Id,
                    NomeDisciplina = b.Disciplina.Nome ?? "Disciplina indefinida",
                    TipoAula = b.TipoAula.Tipo ?? "Tipo indefinido",
                    NomeSala = b.Sala != null ? b.Sala.Nome : "Sala indefinida",
                    Duracao = b.Duracao,
                    NomeProfessor = b.Professor != null ? b.Professor.Nome : "Sem professor"

                })
                .ToListAsync();

            return blocos;
        }

        // POST: api/BlocoAulaAPI
        [HttpPost]
        public async Task<ActionResult> PostBlocoHorario(BlocoAulaDto dto)
        {
            // Cria o bloco
            var bloco = new BlocoAula
            {
                Duracao = dto.Duracao,
                DisciplinaId = dto.DisciplinaId,
                SalaId = dto.SalaId,
                TipoAulaId = dto.TipoAulaId,
                ProfessorId = dto.ProfessorId
            };

            _context.BlocosAulas.Add(bloco);
            await _context.SaveChangesAsync(); // salva para gerar ID

            
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostBlocoHorario), new { id = bloco.Id }, dto);
        }
        
        
    }
}
