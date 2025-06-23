namespace App_horarios_BackEnd.Models.DTO;

public class LocalizacaoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Abreviacao { get; set; }
    
    public List<EscolaDto> Escolas { get; set; }
}