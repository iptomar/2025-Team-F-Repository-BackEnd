namespace App_horarios_BackEnd.Models;

public class DisciplinaCursoProfessor
{
    public int DisciplinaId { get; set; }
    public Disciplina Disciplina { get; set; }

    public int CursoId { get; set; }
    public Curso Curso { get; set; }
    public int? ProfessorId { get; set; }
    public Professor Professor { get; set; }
}