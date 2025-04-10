namespace App_horarios_BackEnd.Models.DTO;

public class BlocoHorarioDto
{
    public int Id { get; set; }
    public string DiaSemana { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    public string Disciplina { get; set; }
    public string TipoAula { get; set; }
    public string Sala { get; set; }
    public List<string> Professores { get; set; }
}