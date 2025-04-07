namespace App_horarios_BackEnd.Models;

public class TransferenciaTurma
{
    public int Id { get; set; }

    public int AlunoId { get; set; }
    public Aluno Aluno { get; set; }

    public int TurmaOrigemId { get; set; }
    public int TurmaDestinoId { get; set; }

    public DateOnly DataTransferencia { get; set; }
    public string? Motivo { get; set; }
}