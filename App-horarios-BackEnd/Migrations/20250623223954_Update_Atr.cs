using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Update_Atr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_Disciplinas_EscolaId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinaCursoProfessor_EscolaId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "DisciplinaCursoProfessor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EscolaId",
                table: "DisciplinaCursoProfessor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaCursoProfessor_EscolaId",
                table: "DisciplinaCursoProfessor",
                column: "EscolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaCursoProfessor_Disciplinas_EscolaId",
                table: "DisciplinaCursoProfessor",
                column: "EscolaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
