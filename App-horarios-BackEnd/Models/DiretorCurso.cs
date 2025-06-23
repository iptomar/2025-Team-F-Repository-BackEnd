using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App_horarios_BackEnd.Models;

public class DiretorCurso
{
    [Key, Column(Order = 0)]
    public int IdUtilizador { get; set; }

    [Key, Column(Order = 1)]
    public int EscolaId { get; set; }

    [Key, Column(Order = 2)]
    public int CursoId { get; set; }

    public Secretariado Secretariado { get; set; }
    public Curso Curso { get; set; }
    public Escola Escola { get; set; }
}