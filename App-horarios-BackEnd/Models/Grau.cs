using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_horarios_BackEnd.Models;

public class Grau
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Importante!
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
    
    public string Duracao { get; set; }
    
    
}