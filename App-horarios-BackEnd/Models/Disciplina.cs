namespace App_horarios_BackEnd.Models;

public class Disciplina
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int? Ano { get; set; }
    public string? Semestre { get; set; }
    public string? Tipo { get; set; }

    public int? HorasTeorica { get; set; }
    public int? HorasPratica { get; set; }
    public int? HorasTp { get; set; }
    public int? HorasSeminario { get; set; }
    public int? HorasLaboratorio { get; set; }
    public int? HorasCampo { get; set; }
    public int? HorasOrientacao { get; set; }
    public int? HorasEstagio { get; set; }
    public int? HorasOutras { get; set; }
    
    public int EscolaId { get; set; }
    public Escola Escola { get; set; }

    public int CursoId { get; set; }
    public Curso Curso { get; set; }
    public ICollection<DisciplinaCursoProfessor> DisciplinaCursoProfessores { get; set; }
}