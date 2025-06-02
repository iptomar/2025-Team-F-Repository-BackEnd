using System.ComponentModel.DataAnnotations;

namespace App_horarios_BackEnd.Models;

public class Grau
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
    
    public string Duracao { get; set; }
    
    
}