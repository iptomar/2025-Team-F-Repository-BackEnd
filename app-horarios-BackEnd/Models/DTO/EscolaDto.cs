namespace App_horarios_BackEnd.Models.DTO;

public class EscolaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public LocalizacaoDto Localizacao { get; set; }

    public List<CursoDto> Cursos { get; set; }
    public List<SalaDto> Salas { get; set; }
}