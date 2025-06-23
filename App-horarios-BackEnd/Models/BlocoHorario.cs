using System;

namespace App_horarios_BackEnd.Models
{
    public class BlocoHorario
    {
        public int Id { get; set; }

        // Dia da semana: 1 (segunda) até 6 (sábado)
        public int DiaSemana { get; set; }

        // Hora de início e fim no formato TimeOnly (C# 10+) ou string/"HH:mm"
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        // Bloco associado
        public int BlocoAulaId { get; set; }
        public BlocoAula BlocoAula { get; set; }

        // Horário ao qual pertence
        public int HorarioId { get; set; }
        public Horario Horario { get; set; }
    }
}