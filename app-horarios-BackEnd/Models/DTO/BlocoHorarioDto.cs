namespace App_horarios_BackEnd.Models.DTO;

public class BlocoHorarioDto
{
    public string DiaSemana { get; set; }
    public string HoraInicio { get; set; }  // "08:30"
    public string HoraFim { get; set; }     // "10:00"

    public int DisciplinaId { get; set; }
    public int SalaId { get; set; }
    public int TipoAulaId { get; set; }
    public int ProfessorId { get; set; }
    public int HorarioId { get; set; }
}
