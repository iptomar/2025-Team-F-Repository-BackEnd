using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlocosssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlocoId",
                table: "BlocosProfessor");

            migrationBuilder.AddColumn<int>(
                name: "BlocoHorarioId",
                table: "DisciplinaCursoProfessor",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaCursoProfessor_BlocoHorarioId",
                table: "DisciplinaCursoProfessor",
                column: "BlocoHorarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaCursoProfessor_BlocosHorario_BlocoHorarioId",
                table: "DisciplinaCursoProfessor",
                column: "BlocoHorarioId",
                principalTable: "BlocosHorario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_BlocosHorario_BlocoHorarioId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinaCursoProfessor_BlocoHorarioId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropColumn(
                name: "BlocoHorarioId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.AddColumn<int>(
                name: "BlocoId",
                table: "BlocosProfessor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
