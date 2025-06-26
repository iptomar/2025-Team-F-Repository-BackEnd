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
        public async Task<ActionResult<IEnumerable<BlocoHorarioDTO>>> GetTodosBlocos()
        {
            var blocos = await _context.BlocosHorarios
                .Include(b => b.BlocoAula)
                .Select(b => new BlocoHorarioDTO
                {
                    BlocoAulaId = b.BlocoAulaId,
                    DiaSemana = b.DiaSemana,
                    HoraInicio = b.HoraInicio.ToString(@"hh\:mm"),
                    HoraFim = b.HoraFim.ToString(@"hh\:mm")
                }).ToListAsync();

            return blocos;
        }
        
    }
}
