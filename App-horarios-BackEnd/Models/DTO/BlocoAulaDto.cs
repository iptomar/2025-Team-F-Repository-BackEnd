namespace App_horarios_BackEnd.Models.DTO;

public class BlocoAulaDto
{
    public int Id { get; set; }

    public int Duracao { get; set; }  
    
    public int DisciplinaId { get; set; }
    public int SalaId { get; set; }
    public int TipoAulaId { get; set; }
    public int ProfessorId { get; set; }
}
