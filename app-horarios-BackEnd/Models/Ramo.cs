namespace App_horarios_BackEnd.Models;

public class Ramo
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ICollection<Curso> Cursos { get; set; }
}
