namespace App_horarios_BackEnd.Models;

public class Aluno
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public bool TemNEE { get; set; }

    public int TurmaId { get; set; }
    public Turma Turma { get; set; }
}