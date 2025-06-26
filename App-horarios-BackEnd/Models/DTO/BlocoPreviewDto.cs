namespace App_horarios_BackEnd.Models.DTO;

public class BlocoPreviewDto
{
    public int Id { get; set; }
    public string NomeDisciplina { get; set; }
    public string TipoAula { get; set; }
    public string NomeProfessor { get; set; } // Agora é uma lista
    public string NomeSala { get; set; }
    public int Duracao { get; set; }
}
