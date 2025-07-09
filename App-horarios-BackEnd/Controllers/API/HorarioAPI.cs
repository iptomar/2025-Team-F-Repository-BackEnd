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

        // GET: api/HorarioAPI/horario-id/turma/1
        [HttpGet("horario-id/turma/{turmaId}")]
        public async Task<ActionResult<int>> GetHorarioIdPorTurma(int turmaId)
        {
            var horario = await _context.Horarios
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.TurmaId == turmaId);

            if (horario == null)
                return NotFound("Horário não encontrado para essa turma.");

            return Ok(horario.Id);
        }
        
        
        
        // 🔎 Localidade
    [HttpGet("por-localidade/{localizacaoId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetHorariosPorLocalidade(int localizacaoId)
    {
        var turmas = await _context.Turmas
            .Include(t => t.Curso)
            .Where(t => t.Curso.LocalizacaoId == localizacaoId)
            .ToListAsync();

        var horarios = await GetHorariosBase(turmas.Select(t => t.Id).ToList());
        return Ok(horarios);
    }

    // 🔎 Escola
    [HttpGet("por-escola/{escolaId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetHorariosPorEscola(int escolaId)
    {
        var turmas = await _context.Turmas
            .Include(t => t.Curso)
            .Where(t => t.Curso.EscolaId == escolaId)
            .ToListAsync();

        var horarios = await GetHorariosBase(turmas.Select(t => t.Id).ToList());
        return Ok(horarios);
    }

    // 🔎 Curso
    [HttpGet("por-curso/{cursoId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetHorariosPorCurso(int cursoId)
    {
        var turmas = await _context.Turmas
            .Where(t => t.CursoId == cursoId)
            .ToListAsync();

        var horarios = await GetHorariosBase(turmas.Select(t => t.Id).ToList());
        return Ok(horarios);
    }

    // 🔎 Sala
    [HttpGet("por-sala/{salaId}")]
    public async Task<ActionResult<IEnumerable<BlocoHorarioView>>> GetHorariosPorSala(int salaId)
    {
        var blocos = await _context.BlocosHorarios
            .Include(bh => bh.BlocoAula)
                .ThenInclude(ba => ba.Disciplina)
            .Include(bh => bh.BlocoAula)
                .ThenInclude(ba => ba.TipoAula)
            .Include(bh => bh.BlocoAula)
                .ThenInclude(ba => ba.Sala)
            .Include(bh => bh.BlocoAula)
                .ThenInclude(ba => ba.BlocoAulaProfessores)
                    .ThenInclude(bap => bap.Professor)
            .Where(bh => bh.BlocoAula.SalaId == salaId)
            .ToListAsync();

        var result = blocos.Select(bh => MapBlocoView(bh)).ToList();
        return Ok(result);
    }

    // 🔎 Professor
    [HttpGet("por-professor/{professorId}")]
    public async Task<ActionResult<IEnumerable<BlocoHorarioView>>> GetHorariosPorProfessor(int professorId)
    {
        var blocoIds = await _context.BlocosAulaProfessores
            .Where(bp => bp.ProfessorId == professorId)
            .Select(bp => bp.BlocoAulaId)
            .ToListAsync();

        var blocos = await _context.BlocosHorarios
            .Include(bh => bh.BlocoAula)
                .ThenInclude(ba => ba.Disciplina)
            .Include(bh => bh.BlocoAula)
                .ThenInclude(ba => ba.TipoAula)
            .Include(bh => bh.BlocoAula)
                .ThenInclude(ba => ba.Sala)
            .Include(bh => bh.BlocoAula)
                .ThenInclude(ba => ba.BlocoAulaProfessores)
                    .ThenInclude(bap => bap.Professor)
            .Where(bh => blocoIds.Contains(bh.BlocoAulaId))
            .ToListAsync();

        var result = blocos.Select(bh => MapBlocoView(bh)).ToList();
        return Ok(result);
    }

    // Método base para Localidade, Escola, Curso
    private async Task<IEnumerable<object>> GetHorariosBase(List<int> turmaIds)
    {
        var horarios = await _context.Horarios
            .Where(h => turmaIds.Contains(h.TurmaId))
            .Include(h => h.BlocosHorarios)
                .ThenInclude(bh => bh.BlocoAula)
                    .ThenInclude(ba => ba.Disciplina)
            .Include(h => h.BlocosHorarios)
                .ThenInclude(bh => bh.BlocoAula)
                    .ThenInclude(ba => ba.TipoAula)
            .Include(h => h.BlocosHorarios)
                .ThenInclude(bh => bh.BlocoAula)
                    .ThenInclude(ba => ba.Sala)
            .Include(h => h.BlocosHorarios)
                .ThenInclude(bh => bh.BlocoAula)
                    .ThenInclude(ba => ba.BlocoAulaProfessores)
                        .ThenInclude(bap => bap.Professor)
            .Include(h => h.Turma)
            .ToListAsync();

        var resultado = horarios.Select(h => new {
            id = h.Id,
            turmaId = h.TurmaId,
            turma = new { id = h.Turma.Id, nome = h.Turma.Nome },
            blocosHorarios = h.BlocosHorarios.Select(bh => MapBlocoView(bh)).ToList()
        });

        return resultado;
    }

    // Helper para mapear um BlocoHorario para BlocoHorarioView
    private BlocoHorarioView1 MapBlocoView(BlocoHorario bh)
    {
        return new BlocoHorarioView1
        {
            Id = bh.Id,
            DiaSemana = bh.DiaSemana,
            HoraInicio = bh.HoraInicio.ToString(@"hh\:mm"),
            HoraFim = bh.HoraFim.ToString(@"hh\:mm"),
            Disciplina = bh.BlocoAula?.Disciplina?.Nome ?? "—",
            TipoAula = bh.BlocoAula?.TipoAula?.Tipo ?? "—",
            Professores = bh.BlocoAula?.BlocoAulaProfessores != null 
                ? string.Join(", ", bh.BlocoAula.BlocoAulaProfessores.Select(p => p.Professor.Nome))
                : "—",
            Sala = bh.BlocoAula?.Sala?.Nome ?? "—"
        };
    }


    }
}
