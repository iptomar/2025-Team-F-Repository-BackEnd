namespace App_horarios_BackEnd.Models;

public class ComissaoCurso
{
    public int Id { get; set; }

    public int EscolaId { get; set; }
    public Escola Escola { get; set; }

    public int CursoId { get; set; }
    public Curso Curso { get; set; }

    public int IdUtilizador { get; set; }
}