namespace App_horarios_BackEnd.Models;

public class BlocoAulaProfessor
{
    public int BlocoAulaId { get; set; }
    public BlocoAula BlocoAula { get; set; }

    public int ProfessorId { get; set; }
    public Professor Professor { get; set; }
}
