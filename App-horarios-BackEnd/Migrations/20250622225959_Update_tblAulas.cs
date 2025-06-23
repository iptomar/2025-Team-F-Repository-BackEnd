using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Update_tblAulas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Horarios_HorarioId",
                table: "BlocosAulas");

            migrationBuilder.DropColumn(
                name: "DiaSemana",
                table: "BlocosAulas");

            migrationBuilder.AlterColumn<int>(
                name: "HorarioId",
                table: "BlocosAulas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Horarios_HorarioId",
                table: "BlocosAulas",
                column: "HorarioId",
                principalTable: "Horarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Horarios_HorarioId",
                table: "BlocosAulas");

            migrationBuilder.AlterColumn<int>(
                name: "HorarioId",
                table: "BlocosAulas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaSemana",
                table: "BlocosAulas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Horarios_HorarioId",
                table: "BlocosAulas",
                column: "HorarioId",
                principalTable: "Horarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
