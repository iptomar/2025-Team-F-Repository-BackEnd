namespace App_horarios_BackEnd.Models;

public class Localizacao
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? Abreviacao { get; set; }

    public ICollection<Escola> Escolas { get; set; }
}