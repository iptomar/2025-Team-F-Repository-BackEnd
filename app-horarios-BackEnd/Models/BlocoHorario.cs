namespace App_horarios_BackEnd.Models;

public class BlocoHorario
{
    public int Id { get; set; }
    public string DiaSemana { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFim { get; set; }

    public int HorarioId { get; set; }
    public Horario Horario { get; set; }

    public int DisciplinaId { get; set; }
    public Disciplina Disciplina { get; set; }

    public int SalaId { get; set; }
    public Sala Sala { get; set; }

    public int TipoAulaId { get; set; }
    public TipoAula TipoAula { get; set; }
}