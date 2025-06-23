namespace App_horarios_BackEnd.Models;

public class Secretariado
{
    public int IdUtilizador { get; set; }
    public string Nome { get; set; }
    public string? Email { get; set; }

    public int EscolaId { get; set; }
    public Escola Escola { get; set; }

    public int CursoId { get; set; }
    public Curso Curso { get; set; }
}