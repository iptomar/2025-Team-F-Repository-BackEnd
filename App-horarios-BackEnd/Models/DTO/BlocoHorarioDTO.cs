namespace App_horarios_BackEnd.Models.DTO
{
    public class BlocoHorarioDTO
    {
        public int Id { get; set; }

        // 1 = Segunda, 2 = Terça, ..., 6 = Sábado
        public int DiaSemana { get; set; }

        // Formato "HH:mm" para frontend
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }

     
        public int BlocoAulaId { get; set; }         // Apenas o ID do BlocoAula
        public int HorarioId { get; set; }           // ID do horário (Turma)
    }
}