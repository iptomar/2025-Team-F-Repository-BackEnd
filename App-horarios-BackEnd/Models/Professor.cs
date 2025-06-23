namespace App_horarios_BackEnd.Models;

public class Professor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? Email { get; set; }

    public int UnidadeDepartamentalId { get; set; }
    public UnidadeDepartamental UnidadeDepartamental { get; set; }

    public int CategoriaId { get; set; }
    public CategoriaDocente Categoria { get; set; }
}