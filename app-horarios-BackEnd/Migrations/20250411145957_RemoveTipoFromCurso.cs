using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTipoFromCurso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Cursos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Cursos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
