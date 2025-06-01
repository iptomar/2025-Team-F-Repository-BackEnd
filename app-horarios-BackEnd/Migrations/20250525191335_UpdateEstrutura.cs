using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEstrutura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_BlocosAulas_BlocoHorarioId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropTable(
                name: "BlocosProfessor");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinaCursoProfessor_BlocoHorarioId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropColumn(
                name: "BlocoHorarioId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "BlocosAulas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlocosAulas_ProfessorId",
                table: "BlocosAulas",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Professores_ProfessorId",
                table: "BlocosAulas",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Professores_ProfessorId",
                table: "BlocosAulas");

            migrationBuilder.DropIndex(
                name: "IX_BlocosAulas_ProfessorId",
                table: "BlocosAulas");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "BlocosAulas");

            migrationBuilder.AddColumn<int>(
                name: "BlocoHorarioId",
                table: "DisciplinaCursoProfessor",
                type: "integer",
                nullable: true);

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
                        name: "FK_BlocosProfessor_BlocosAulas_BlocoHorarioId",
                        column: x => x.BlocoHorarioId,
                        principalTable: "BlocosAulas",
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
                name: "IX_BlocosProfessor_BlocoHorarioId",
                table: "BlocosProfessor",
                column: "BlocoHorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosProfessor_ProfessorId",
                table: "BlocosProfessor",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaCursoProfessor_BlocosAulas_BlocoHorarioId",
                table: "DisciplinaCursoProfessor",
                column: "BlocoHorarioId",
                principalTable: "BlocosAulas",
                principalColumn: "Id");
        }
    }
}
