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
    public DbSet<Ramo> Ramos { get; set; }
    public DbSet<Grau> Graus { get; set; }
    public DbSet<Horario> Horarios { get; set; }
    public DbSet<TipoAula> TiposAula { get; set; }
    public DbSet<BlocoHorario> BlocosHorario { get; set; }
    public DbSet<BlocoProfessor> BlocosProfessor { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<TransferenciaTurma> TransferenciasTurma { get; set; }
    public DbSet<Secretariado> Secretariados { get; set; }
    public DbSet<ComissaoCurso> ComissoesCurso { get; set; }
    public DbSet<DiretorCurso> DiretoresCurso { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Secretariado>().HasKey(s => s.IdUtilizador);
    }
}