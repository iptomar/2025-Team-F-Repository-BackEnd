namespace App_horarios_BackEnd.Models.DTO;

public class BlocoHorarioVisualDto
{
    public int Id { get; set; }
    public string DiaSemana { get; set; }
    public string HoraInicio { get; set; }
    public string HoraFim { get; set; }

    public string Disciplina { get; set; }
    public string Sala { get; set; }
    public string Professor { get; set; }
    public string TipoAula { get; set; }

    public int Horas { get; set; } // duração do bloco, útil no frontend
}
