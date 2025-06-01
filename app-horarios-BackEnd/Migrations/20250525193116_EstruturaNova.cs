using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class EstruturaNova : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_BlocosHorario_BlocoHorarioId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropTable(
                name: "BlocosProfessor");

            migrationBuilder.DropTable(
                name: "BlocosHorario");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinaCursoProfessor_BlocoHorarioId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropColumn(
                name: "BlocoHorarioId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.CreateTable(
                name: "BlocosAulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiaSemana = table.Column<string>(type: "text", nullable: false),
                    Duracao = table.Column<int>(type: "integer", nullable: false),
                    HorarioId = table.Column<int>(type: "integer", nullable: false),
                    DisciplinaId = table.Column<int>(type: "integer", nullable: false),
                    SalaId = table.Column<int>(type: "integer", nullable: false),
                    TipoAulaId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlocosAulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlocosAulas_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlocosAulas_Horarios_HorarioId",
                        column: x => x.HorarioId,
                        principalTable: "Horarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlocosAulas_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlocosAulas_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlocosAulas_TiposAula_TipoAulaId",
                        column: x => x.TipoAulaId,
                        principalTable: "TiposAula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlocosAulas_DisciplinaId",
                table: "BlocosAulas",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosAulas_HorarioId",
                table: "BlocosAulas",
                column: "HorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosAulas_ProfessorId",
                table: "BlocosAulas",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosAulas_SalaId",
                table: "BlocosAulas",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosAulas_TipoAulaId",
                table: "BlocosAulas",
                column: "TipoAulaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlocosAulas");

            migrationBuilder.AddColumn<int>(
                name: "BlocoHorarioId",
                table: "DisciplinaCursoProfessor",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlocosHorario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisciplinaId = table.Column<int>(type: "integer", nullable: false),
                    HorarioId = table.Column<int>(type: "integer", nullable: false),
                    SalaId = table.Column<int>(type: "integer", nullable: false),
                    TipoAulaId = table.Column<int>(type: "integer", nullable: false),
                    DiaSemana = table.Column<string>(type: "text", nullable: false),
                    Duracao = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_DisciplinaCursoProfessor_BlocoHorarioId",
                table: "DisciplinaCursoProfessor",
                column: "BlocoHorarioId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaCursoProfessor_BlocosHorario_BlocoHorarioId",
                table: "DisciplinaCursoProfessor",
                column: "BlocoHorarioId",
                principalTable: "BlocosHorario",
                principalColumn: "Id");
        }
    }
}
