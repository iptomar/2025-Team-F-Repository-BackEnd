namespace App_horarios_BackEnd.Models;

public class BlocoHorarioView
{
    public int Id { get; set; }
    public int DiaSemana { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    public string Disciplina { get; set; }
    
    public string TipoAula { get; set; }
    public string Professores { get; set; }
    public string Sala { get; set; }
    
    public BlocoAula BlocoAula { get; set; }
}
