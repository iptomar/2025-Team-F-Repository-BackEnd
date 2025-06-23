namespace App_horarios_BackEnd.Models.DTO;

public class TurmaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int? NumeroAlunos { get; set; }
    public bool Aberta { get; set; }
    public int CursoId { get; set; }
}