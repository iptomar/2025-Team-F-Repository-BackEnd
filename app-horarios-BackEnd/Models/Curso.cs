namespace App_horarios_BackEnd.Models;

public class Curso
{
    public int Id { get; set; } // Parte 1 da PK
    public int EscolaId { get; set; } // Parte 2 da PK
    public Escola Escola { get; set; }
    public string Nome { get; set; }
    
    public int GrauId { get; set; }
    public Grau Grau { get; set; }
    

    public ICollection<Turma> Turmas { get; set; }
    public ICollection<DisciplinaCursoProfessor> DisciplinaCursoProfessor { get; set; }
}
