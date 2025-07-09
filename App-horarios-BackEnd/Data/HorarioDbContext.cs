using App_horarios_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace app_horarios_BackEnd.Data;

public class HorarioDbContext : DbContext
{
    public HorarioDbContext(DbContextOptions<HorarioDbContext> options) : base(options) {}

    public DbSet<Localizacao> Localizacoes { get; set; }
    public DbSet<Escola> Escolas { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<UnidadeDepartamental> UnidadesDepartamentais { get; set; }
    public DbSet<CategoriaDocente> CategoriasDocentes { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Disciplina> Disciplinas { get; set; }
    public DbSet<DisciplinaCursoProfessor> DisciplinaCursoProfessor { get; set; }
    public DbSet<Grau> Graus { get; set; }
    public DbSet<Horario> Horarios { get; set; }
    public DbSet<TipoAula> TiposAula { get; set; }
    public DbSet<BlocoAula> BlocosAulas { get; set; }
    
    public DbSet<BlocoHorario> BlocosHorarios { get; set; }
    
    public DbSet<BlocoAulaProfessor> BlocosAulaProfessores { get; set; }

    public DbSet<Aluno> Alunos { get; set; }

    public DbSet<TransferenciaTurma> TransferenciasTurma { get; set; }
    public DbSet<Secretariado> Secretariados { get; set; }
    public DbSet<ComissaoCurso> ComissoesCurso { get; set; }
    public DbSet<DiretorCurso> DiretoresCurso { get; set; }
    public DbSet<Utilizador> Utilizadores { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // PK simples
        modelBuilder.Entity<Curso>()
            .HasKey(c => c.Id); // apenas o Id
        
        modelBuilder.Entity<Curso>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd(); // permite geração automática, mas aceita valores se fornecidos
        
        // Secretariado
        modelBuilder.Entity<Secretariado>()
            .HasKey(s => s.IdUtilizador);

        modelBuilder.Entity<Secretariado>()
            .HasOne(s => s.Curso)
            .WithMany()
            .HasForeignKey(s => s.CursoId); // apenas CursoId

        // PK para DisciplinaCursoProfessor
        modelBuilder.Entity<DisciplinaCursoProfessor>()
            .HasKey(dcp => new { dcp.CursoId, dcp.DisciplinaId });

        // Relação com Disciplina
        modelBuilder.Entity<DisciplinaCursoProfessor>()
            .HasOne(dcp => dcp.Disciplina)
            .WithMany(d => d.DisciplinaCursoProfessor) // agora explícito!
            .HasForeignKey(dcp => dcp.DisciplinaId);

        // Relação com Professor (opcional)
        modelBuilder.Entity<DisciplinaCursoProfessor>()
            .HasOne(dcp => dcp.Professor)
            .WithMany(p => p.DisciplinaCursoProfessor) // se quiser também aqui
            .HasForeignKey(dcp => dcp.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict); // ou .SetNull, conforme tua lógica

        // Relação com Curso
        modelBuilder.Entity<DisciplinaCursoProfessor>()
            .HasOne(dcp => dcp.Curso)
            .WithMany(c => c.DisciplinaCursoProfessor) // aqui está o segredo!
            .HasForeignKey(dcp => dcp.CursoId);

        
        //ComissaoCurso
        modelBuilder.Entity<ComissaoCurso>()
            .HasKey(cc => new { cc.IdUtilizador, cc.CursoId, cc.EscolaId });

        modelBuilder.Entity<ComissaoCurso>()
            .HasOne(cc => cc.Secretariado)
            .WithMany()
            .HasForeignKey(cc => cc.IdUtilizador);

        modelBuilder.Entity<ComissaoCurso>()
            .HasOne(cc => cc.Curso)
            .WithMany()
            .HasForeignKey(cc => cc.CursoId);

        modelBuilder.Entity<ComissaoCurso>()
            .HasOne(cc => cc.Escola)
            .WithMany()
            .HasForeignKey(cc => cc.EscolaId);
        
        //DiretorCurso
        modelBuilder.Entity<DiretorCurso>()
            .HasKey(dc => new { dc.IdUtilizador, dc.CursoId, dc.EscolaId });

        modelBuilder.Entity<DiretorCurso>()
            .HasOne(dc => dc.Secretariado)
            .WithMany()
            .HasForeignKey(dc => dc.IdUtilizador);

        modelBuilder.Entity<DiretorCurso>()
            .HasOne(dc => dc.Curso)
            .WithMany()
            .HasForeignKey(dc => dc.CursoId);

        modelBuilder.Entity<DiretorCurso>()
            .HasOne(dc => dc.Escola)
            .WithMany()
            .HasForeignKey(dc => dc.EscolaId);
        
        modelBuilder.Entity<BlocoAula>()
            .HasOne(b => b.Turma)
            .WithMany() // ou .WithMany(t => t.BlocosAula) se tiveres navegação inversa
            .HasForeignKey(b => b.TurmaId)
            .OnDelete(DeleteBehavior.Cascade); // ou Restrict, dependendo da tua lógica

        // Relação muitos-para-muitos entre BlocoAula e Professor
        modelBuilder.Entity<BlocoAulaProfessor>()
            .HasKey(bap => new { bap.BlocoAulaId, bap.ProfessorId });

        modelBuilder.Entity<BlocoAulaProfessor>()
            .HasOne(bap => bap.BlocoAula)
            .WithMany(b => b.BlocoAulaProfessores)
            .HasForeignKey(bap => bap.BlocoAulaId);

        modelBuilder.Entity<BlocoAulaProfessor>()
            .HasOne(bap => bap.Professor)
            .WithMany(p => p.BlocoAulaProfessores)
            .HasForeignKey(bap => bap.ProfessorId);


    }
    
}