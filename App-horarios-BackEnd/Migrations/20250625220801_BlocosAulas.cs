using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class BlocosAulas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Salas_SalaId",
                table: "BlocosAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_Professores_ProfessorId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.AlterColumn<int>(
                name: "SalaId",
                table: "BlocosAulas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "TurmaId",
                table: "BlocosAulas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlocosAulas_TurmaId",
                table: "BlocosAulas",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Salas_SalaId",
                table: "BlocosAulas",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Turmas_TurmaId",
                table: "BlocosAulas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaCursoProfessor_Professores_ProfessorId",
                table: "DisciplinaCursoProfessor",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Salas_SalaId",
                table: "BlocosAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Turmas_TurmaId",
                table: "BlocosAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_Professores_ProfessorId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropIndex(
                name: "IX_BlocosAulas_TurmaId",
                table: "BlocosAulas");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "BlocosAulas");

            migrationBuilder.AlterColumn<int>(
                name: "SalaId",
                table: "BlocosAulas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Salas_SalaId",
                table: "BlocosAulas",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaCursoProfessor_Professores_ProfessorId",
                table: "DisciplinaCursoProfessor",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id");
        }
    }
}
