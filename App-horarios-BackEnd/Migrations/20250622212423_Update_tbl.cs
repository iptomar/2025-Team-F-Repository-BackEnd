using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Update_tbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Plano",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "PlanoId",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "Ramo",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "RamoId",
                table: "Disciplinas");

            migrationBuilder.AlterColumn<string>(
                name: "Semestre",
                table: "Disciplinas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Semestre",
                table: "Disciplinas",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Plano",
                table: "Disciplinas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanoId",
                table: "Disciplinas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ramo",
                table: "Disciplinas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RamoId",
                table: "Disciplinas",
                type: "integer",
                nullable: true);
        }
    }
}
