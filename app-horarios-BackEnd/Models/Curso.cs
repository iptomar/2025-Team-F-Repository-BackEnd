namespace App_horarios_BackEnd.Models;

public class Curso
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int EscolaId { get; set; }
    public Escola Escola { get; set; }

    public int? RamoId { get; set; }
    public Ramo Ramo { get; set; }
    
    public int GrauId { get; set; }
    public Grau Grau { get; set; }

    public ICollection<Turma> Turmas { get; set; }
    public ICollection<DisciplinaCursoProfessor> DisciplinaCursoProfessor { get; set; }
}
