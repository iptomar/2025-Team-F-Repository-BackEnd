namespace App_horarios_BackEnd.Models;

public class Turma
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public int? NumeroAlunos { get; set; }
    public bool Aberta { get; set; } = true;
    public int CursoId { get; set; }
    public Curso Curso { get; set; }

    public ICollection<Aluno> Alunos { get; set; }
}