namespace App_horarios_BackEnd.Models.DTO;

public class ProfessorDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; // ← evita warning
    public string Email { get; set; } = string.Empty;
    public string UnidadeDepartamental { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
}