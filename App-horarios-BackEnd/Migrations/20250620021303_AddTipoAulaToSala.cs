using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoAulaToSala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escolas_Localizacoes_LocalizacaoId",
                table: "Escolas");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Salas");

            migrationBuilder.AddColumn<int>(
                name: "TipoAulaId",
                table: "Salas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "LocalizacaoId",
                table: "Escolas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salas_TipoAulaId",
                table: "Salas",
                column: "TipoAulaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Escolas_Localizacoes_LocalizacaoId",
                table: "Escolas",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salas_TiposAula_TipoAulaId",
                table: "Salas",
                column: "TipoAulaId",
                principalTable: "TiposAula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escolas_Localizacoes_LocalizacaoId",
                table: "Escolas");

            migrationBuilder.DropForeignKey(
                name: "FK_Salas_TiposAula_TipoAulaId",
                table: "Salas");

            migrationBuilder.DropIndex(
                name: "IX_Salas_TipoAulaId",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "TipoAulaId",
                table: "Salas");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Salas",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocalizacaoId",
                table: "Escolas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Escolas_Localizacoes_LocalizacaoId",
                table: "Escolas",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "Id");
        }
    }
}
