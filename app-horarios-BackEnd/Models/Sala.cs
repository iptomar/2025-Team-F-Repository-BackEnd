namespace App_horarios_BackEnd.Models;

public class Sala
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Capacidade { get; set; }
    public string? Tipo { get; set; }

    public int EscolaId { get; set; }
    public Escola Escola { get; set; }
}