using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTblAulas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BlocosAulaProfessores",
                columns: table => new
                {
                    BlocoAulaId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlocosAulaProfessores", x => new { x.BlocoAulaId, x.ProfessorId });
                    table.ForeignKey(
                        name: "FK_BlocosAulaProfessores_BlocosAulas_BlocoAulaId",
                        column: x => x.BlocoAulaId,
                        principalTable: "BlocosAulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlocosAulaProfessores_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlocosAulaProfessores_ProfessorId",
                table: "BlocosAulaProfessores",
                column: "ProfessorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlocosAulaProfessores");

            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "BlocosAulas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlocosAulas_ProfessorId",
                table: "BlocosAulas",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Professores_ProfessorId",
                table: "BlocosAulas",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id");
        }
    }
}
