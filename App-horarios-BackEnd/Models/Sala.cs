using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_horarios_BackEnd.Models;

public class Sala
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Capacidade { get; set; }
    [Required]
    public int? TipoAulaId { get; set; } // <- IMPORTANTE: int? para aceitar vazio

    [ForeignKey("TipoAulaId")]
    public TipoAula TipoAula { get; set; }

    [Required]
    public int? EscolaId { get; set; } // <- IMPORTANTE: int? para aceitar vazio

    [ForeignKey("EscolaId")]
    public Escola Escola { get; set; }
}