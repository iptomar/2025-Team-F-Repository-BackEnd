using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class CreateUtilizador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Localizacoes_LocalizacaoId",
                table: "Cursos");

            migrationBuilder.AlterColumn<int>(
                name: "LocalizacaoId",
                table: "Cursos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Localizacoes_LocalizacaoId",
                table: "Cursos",
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

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.AlterColumn<int>(
                name: "LocalizacaoId",
                table: "Cursos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Localizacoes_LocalizacaoId",
                table: "Cursos",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
