namespace App_horarios_BackEnd.Models;

public class Curso
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? Tipo { get; set; }

    public int EscolaId { get; set; }
    public Escola Escola { get; set; }

    public ICollection<Turma> Turmas { get; set; }
}