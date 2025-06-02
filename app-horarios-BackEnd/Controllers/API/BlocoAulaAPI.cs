
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
            var blocosQuery = await _context.BlocosAulas
                .Include(b => b.Disciplina)
                .ThenInclude(d => d.DisciplinaCursoProfessores)
                .ThenInclude(dcp => dcp.Professor)
                .Include(b => b.Sala)
                .Include(b => b.TipoAula)
                .Where(b =>
                    b.Disciplina != null &&
                    b.Disciplina.Ano == ano &&
                    b.Disciplina.Semestre == semestre &&
                    b.Disciplina.DisciplinaCursoProfessores.Any(dcp => dcp.CursoId == cursoId))
                .Select(b => new
                {
                    DisciplinaNome = b.Disciplina.Nome,
                    TipoAulaNome = b.TipoAula.Tipo,
                    SalaNome = b.Sala.Nome,
                    DiaSemana = b.DiaSemana,
                    Duracao = b.Duracao,
                    Professores = b.Disciplina.DisciplinaCursoProfessores
                        .Where(dcp => dcp.CursoId == cursoId)
                        .Select(dcp => dcp.Professor.Nome)
                        .ToList()
                })
                .ToListAsync(); // 👈 ESSE ToListAsync funciona porque é sobre tipo anônimo


            var blocos = blocosQuery.Select(b => new BlocoPreviewDto
            {
                NomeDisciplina = b.DisciplinaNome ?? "Disciplina indefinida",
                TipoAula = b.TipoAulaNome ?? "Tipo indefinido",
                NomeSala = b.SalaNome ?? "Sala indefinida",
                DiaSemana = b.DiaSemana ?? "Dia indefinido",
                Duracao = b.Duracao,
                NomeProfessor = b.Professores.ToString()
            }).ToList();





            return blocos;
        }



        


        // POST: api/BlocoAulaAPI
        [HttpPost]
        public async Task<ActionResult> PostBlocoHorario(BlocoAulaDto dto)
        {
            // Cria o bloco
            var bloco = new BlocoAula
            {
                DiaSemana = dto.DiaSemana,
                Duracao = dto.Duracao,
                DisciplinaId = dto.DisciplinaId,
                SalaId = dto.SalaId,
                TipoAulaId = dto.TipoAulaId,
                ProfessorId = dto.ProfessorId,
                HorarioId = dto.HorarioId
            };

            _context.BlocosAulas.Add(bloco);
            await _context.SaveChangesAsync(); // salva para gerar ID

            
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostBlocoHorario), new { id = bloco.Id }, dto);
        }
        

        
    }
}
