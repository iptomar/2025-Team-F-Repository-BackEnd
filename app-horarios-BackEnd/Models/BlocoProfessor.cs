namespace App_horarios_BackEnd.Models;

public class BlocoProfessor
{
    public int Id { get; set; }

    public int BlocoHorarioId { get; set; }
    public BlocoHorario BlocoHorario { get; set; }

    public int ProfessorId { get; set; }
    public Professor Professor { get; set; }
}