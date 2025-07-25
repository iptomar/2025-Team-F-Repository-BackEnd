namespace App_horarios_BackEnd.Models;

public class Horario
{
    public int Id { get; set; }
    
    public int TurmaId { get; set; }
    public Turma Turma { get; set; }

    public bool Bloqueado { get; set; } = false;
    
    public ICollection<BlocoHorario> BlocosHorarios { get; set; }
}
