using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class PermitirProfessorNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Professores_ProfessorId",
                table: "BlocosAulas");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorId",
                table: "BlocosAulas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Professores_ProfessorId",
                table: "BlocosAulas",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Professores_ProfessorId",
                table: "BlocosAulas");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorId",
                table: "BlocosAulas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Professores_ProfessorId",
                table: "BlocosAulas",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
