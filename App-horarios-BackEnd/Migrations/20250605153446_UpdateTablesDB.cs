using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escolas_Localizacoes_LocalizacaoId",
                table: "Escolas");

            migrationBuilder.AlterColumn<int>(
                name: "LocalizacaoId",
                table: "Escolas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "LocalizacaoId",
                table: "Cursos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_LocalizacaoId",
                table: "Cursos",
                column: "LocalizacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Localizacoes_LocalizacaoId",
                table: "Cursos",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Escolas_Localizacoes_LocalizacaoId",
                table: "Escolas",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Localizacoes_LocalizacaoId",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Escolas_Localizacoes_LocalizacaoId",
                table: "Escolas");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_LocalizacaoId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "LocalizacaoId",
                table: "Cursos");

            migrationBuilder.AlterColumn<int>(
                name: "LocalizacaoId",
                table: "Escolas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Escolas_Localizacoes_LocalizacaoId",
                table: "Escolas",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
