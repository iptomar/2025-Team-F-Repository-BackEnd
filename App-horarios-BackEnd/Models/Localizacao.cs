using System.ComponentModel.DataAnnotations;

namespace App_horarios_BackEnd.Models;

public class Localizacao
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }
    public string? Abreviacao { get; set; }

    public ICollection<Escola> Escolas { get; set; } = new List<Escola>();

}