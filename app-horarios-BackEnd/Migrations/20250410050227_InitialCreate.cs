using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasDocentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasDocentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Plano = table.Column<string>(type: "text", nullable: true),
                    Grau = table.Column<string>(type: "text", nullable: true),
                    Ano = table.Column<int>(type: "integer", nullable: true),
                    Semestre = table.Column<string>(type: "text", nullable: true),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    HorasTeorica = table.Column<int>(type: "integer", nullable: true),
                    HorasPratica = table.Column<int>(type: "integer", nullable: true),
                    HorasTp = table.Column<int>(type: "integer", nullable: true),
                    HorasSeminario = table.Column<int>(type: "integer", nullable: true),
                    HorasLaboratorio = table.Column<int>(type: "integer", nullable: true),
                    HorasCampo = table.Column<int>(type: "integer", nullable: true),
                    HorasOrientacao = table.Column<int>(type: "integer", nullable: true),
                    HorasEstagio = table.Column<int>(type: "integer", nullable: true),
                    HorasOutras = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localizacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Abreviacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposAula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Acronimo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAula", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesDepartamentais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesDepartamentais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escolas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    LocalizacaoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Escolas_Localizacoes_LocalizacaoId",
                        column: x => x.LocalizacaoId,
                        principalTable: "Localizacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    UnidadeDepartamentalId = table.Column<int>(type: "integer", nullable: false),
                    CategoriaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professores_CategoriasDocentes_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriasDocentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Professores_UnidadesDepartamentais_UnidadeDepartamentalId",
                        column: x => x.UnidadeDepartamentalId,
                        principalTable: "UnidadesDepartamentais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    EscolaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Capacidade = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    EscolaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salas_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComissoesCurso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EscolaId = table.Column<int>(type: "integer", nullable: false),
                    CursoId = table.Column<int>(type: "integer", nullable: false),
                    IdUtilizador = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComissoesCurso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComissoesCurso_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComissoesCurso_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiretoresCurso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EscolaId = table.Column<int>(type: "integer", nullable: false),
                    CursoId = table.Column<int>(type: "integer", nullable: false),
                    IdUtilizador = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiretoresCurso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiretoresCurso_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiretoresCurso_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaCursoProfessor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisciplinaId = table.Column<int>(type: "integer", nullable: false),
                    CursoId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaCursoProfessor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplinaCursoProfessor_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplinaCursoProfessor_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplinaCursoProfessor_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Secretariados",
                columns: table => new
                {
                    IdUtilizador = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    EscolaId = table.Column<int>(type: "integer", nullable: false),
                    CursoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretariados", x => x.IdUtilizador);
                    table.ForeignKey(
                        name: "FK_Secretariados_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Secretariados_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    NumeroAlunos = table.Column<int>(type: "integer", nullable: true),
                    Aberta = table.Column<bool>(type: "boolean", nullable: false),
                    CursoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turmas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    TemNEE = table.Column<bool>(type: "boolean", nullable: false),
                    TurmaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TurmaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horarios_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferenciasTurma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlunoId = table.Column<int>(type: "integer", nullable: false),
                    TurmaOrigemId = table.Column<int>(type: "integer", nullable: false),
                    TurmaDestinoId = table.Column<int>(type: "integer", nullable: false),
                    DataTransferencia = table.Column<DateOnly>(type: "date", nullable: false),
                    Motivo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferenciasTurma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferenciasTurma_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlocosHorario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiaSemana = table.Column<string>(type: "text", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    HoraFim = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    HorarioId = table.Column<int>(type: "integer", nullable: false),
                    DisciplinaId = table.Column<int>(type: "integer", nullable: false),
                    SalaId = table.Column<int>(type: "integer", nullable: false),
                    TipoAulaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlocosHorario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlocosHorario_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlocosHorario_Horarios_HorarioId",
                        column: x => x.HorarioId,
                        principalTable: "Horarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlocosHorario_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlocosHorario_TiposAula_TipoAulaId",
                        column: x => x.TipoAulaId,
                        principalTable: "TiposAula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlocosProfessor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlocoId = table.Column<int>(type: "integer", nullable: false),
                    BlocoHorarioId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlocosProfessor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlocosProfessor_BlocosHorario_BlocoHorarioId",
                        column: x => x.BlocoHorarioId,
                        principalTable: "BlocosHorario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlocosProfessor_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_TurmaId",
                table: "Alunos",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorario_DisciplinaId",
                table: "BlocosHorario",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorario_HorarioId",
                table: "BlocosHorario",
                column: "HorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorario_SalaId",
                table: "BlocosHorario",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorario_TipoAulaId",
                table: "BlocosHorario",
                column: "TipoAulaId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosProfessor_BlocoHorarioId",
                table: "BlocosProfessor",
                column: "BlocoHorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosProfessor_ProfessorId",
                table: "BlocosProfessor",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComissoesCurso_CursoId",
                table: "ComissoesCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComissoesCurso_EscolaId",
                table: "ComissoesCurso",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_EscolaId",
                table: "Cursos",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_DiretoresCurso_CursoId",
                table: "DiretoresCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_DiretoresCurso_EscolaId",
                table: "DiretoresCurso",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaCursoProfessor_CursoId",
                table: "DisciplinaCursoProfessor",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaCursoProfessor_DisciplinaId",
                table: "DisciplinaCursoProfessor",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaCursoProfessor_ProfessorId",
                table: "DisciplinaCursoProfessor",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Escolas_LocalizacaoId",
                table: "Escolas",
                column: "LocalizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_TurmaId",
                table: "Horarios",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Professores_CategoriaId",
                table: "Professores",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Professores_UnidadeDepartamentalId",
                table: "Professores",
                column: "UnidadeDepartamentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Salas_EscolaId",
                table: "Salas",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Secretariados_CursoId",
                table: "Secretariados",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Secretariados_EscolaId",
                table: "Secretariados",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferenciasTurma_AlunoId",
                table: "TransferenciasTurma",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_CursoId",
                table: "Turmas",
                column: "CursoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlocosProfessor");

            migrationBuilder.DropTable(
                name: "ComissoesCurso");

            migrationBuilder.DropTable(
                name: "DiretoresCurso");

            migrationBuilder.DropTable(
                name: "DisciplinaCursoProfessor");

            migrationBuilder.DropTable(
                name: "Secretariados");

            migrationBuilder.DropTable(
                name: "TransferenciasTurma");

            migrationBuilder.DropTable(
                name: "BlocosHorario");

            migrationBuilder.DropTable(
                name: "Professores");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "TiposAula");

            migrationBuilder.DropTable(
                name: "CategoriasDocentes");

            migrationBuilder.DropTable(
                name: "UnidadesDepartamentais");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Escolas");

            migrationBuilder.DropTable(
                name: "Localizacoes");
        }
    }
}
