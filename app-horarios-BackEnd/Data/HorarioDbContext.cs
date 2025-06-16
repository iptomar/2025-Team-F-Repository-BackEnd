using App_horarios_BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using App_horarios_BackEnd.Models.DTO;

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
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<TransferenciaTurma> TransferenciasTurma { get; set; }
    public DbSet<Secretariado> Secretariados { get; set; }
    public DbSet<ComissaoCurso> ComissoesCurso { get; set; }
    public DbSet<DiretorCurso> DiretoresCurso { get; set; }
    public DbSet<Utilizador> Utilizadores { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Secretariado>()
            .HasKey(s => new { s.IdUtilizador, s.CursoId, s.EscolaId });

        modelBuilder.Entity<Secretariado>()
            .HasOne(s => s.Curso)
            .WithMany()
            .HasForeignKey(s => new { s.CursoId, s.EscolaId });

        
        modelBuilder.Entity<Curso>()
            .HasKey(c => new { c.Id, c.EscolaId });
        
        // PK composta para DisciplinaCursoProfessor
        modelBuilder.Entity<DisciplinaCursoProfessor>()
            .HasKey(dcp => new { dcp.CursoId, dcp.EscolaId, dcp.DisciplinaId });

        // Relação com Curso (chave composta)
        modelBuilder.Entity<DisciplinaCursoProfessor>()
            .HasOne(dcp => dcp.Curso)
            .WithMany()
            .HasForeignKey(dcp => new { dcp.CursoId, dcp.EscolaId });

        // Relação com Disciplina
        modelBuilder.Entity<DisciplinaCursoProfessor>()
            .HasOne(dcp => dcp.Disciplina)
            .WithMany(d => d.DisciplinaCursoProfessores)
            .HasForeignKey(dcp => dcp.DisciplinaId);

        // Relação com Professor (opcional)
        modelBuilder.Entity<DisciplinaCursoProfessor>()
            .HasOne(dcp => dcp.Professor)
            .WithMany()
            .HasForeignKey(dcp => dcp.ProfessorId);


    }
    
}