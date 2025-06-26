using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class SincronizarComBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_Professores_ProfessorId",
                table: "DisciplinaCursoProfessor");

          migrationBuilder.AlterColumn<int>(
                name: "ProfessorId",
                table: "DisciplinaCursoProfessor",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaCursoProfessor_Professores_ProfessorId",
                table: "DisciplinaCursoProfessor",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
