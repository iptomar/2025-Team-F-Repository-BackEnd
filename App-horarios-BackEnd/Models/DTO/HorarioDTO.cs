namespace App_horarios_BackEnd.Models.DTO;

public class HorarioDTO
{
    public int Id { get; set; }
    public int TurmaId { get; set; }
    
    public List<BlocoHorarioDTO> BlocosHorarios { get; set; }

}