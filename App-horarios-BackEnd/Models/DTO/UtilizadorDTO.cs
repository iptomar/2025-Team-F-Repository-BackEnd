namespace App_horarios_BackEnd.Models.DTO;

public class UtilizadorDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Tipo { get; set; } // "Secretariado", "DiretorCurso", etc.
}