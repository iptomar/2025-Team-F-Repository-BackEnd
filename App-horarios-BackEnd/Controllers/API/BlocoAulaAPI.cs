
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
                .Include(b => b.BlocoAulaProfessores)
                .ThenInclude(bp => bp.Professor)
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
                    NomeProfessor = string.Join(", ", b.BlocoAulaProfessores.Select(bp => bp.Professor.Nome)) // ✅ Múltiplos nomes
                })
                .ToListAsync();

            return blocos;
        }
        
        
    }
}
