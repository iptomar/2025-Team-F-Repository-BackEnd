namespace App_horarios_BackEnd.Models;

public class Escola
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? Abreviacao { get; set; }
    public ICollection<Curso> Cursos { get; set; }
    public ICollection<Sala> Salas { get; set; }
}