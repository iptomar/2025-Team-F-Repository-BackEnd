using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class CriartblDCP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisciplinaCursoProfessor",
                columns: table => new
                {
                    CursoId = table.Column<int>(type: "integer", nullable: false),
                    DisciplinaId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaCursoProfessor", x => new { x.CursoId, x.DisciplinaId });
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaCursoProfessor_DisciplinaId",
                table: "DisciplinaCursoProfessor",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaCursoProfessor_ProfessorId",
                table: "DisciplinaCursoProfessor",
                column: "ProfessorId");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplinaCursoProfessor");
        }
    }
}
