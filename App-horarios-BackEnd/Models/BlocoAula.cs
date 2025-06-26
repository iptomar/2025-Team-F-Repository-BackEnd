namespace App_horarios_BackEnd.Models;

public class BlocoAula
{
    public int Id { get; set; }
    public int Duracao { get; set; }
    
    public int DisciplinaId { get; set; }
    public Disciplina Disciplina { get; set; }

    public int? SalaId { get; set; }
    public Sala Sala { get; set; }

    public int TipoAulaId { get; set; }
    public TipoAula TipoAula { get; set; }
   
    public int TurmaId { get; set; }
    public Turma Turma { get; set; }
    
    // ✅ Professores associados
    public ICollection<BlocoAulaProfessor> BlocoAulaProfessores { get; set; }

}