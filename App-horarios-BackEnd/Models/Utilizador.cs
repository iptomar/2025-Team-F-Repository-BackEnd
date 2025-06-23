using System.ComponentModel.DataAnnotations;

namespace App_horarios_BackEnd.Models
{
    public class Utilizador
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Email ")]
        [Required(ErrorMessage = "Email é obrigatorio!")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        // Propriedade apenas informativa, não obrigatória de armazenar

        public string? Tipo { get; set; } // "Secretariado", "DiretorCurso", "ComissaoCurso"
    }
}
