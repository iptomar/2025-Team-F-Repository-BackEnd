using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Update_tblDiretor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DiretoresCurso",
                table: "DiretoresCurso");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DiretoresCurso");

            migrationBuilder.AlterColumn<int>(
                name: "IdUtilizador",
                table: "DiretoresCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "EscolaId",
                table: "DiretoresCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "CursoId",
                table: "DiretoresCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiretoresCurso",
                table: "DiretoresCurso",
                columns: new[] { "IdUtilizador", "CursoId", "EscolaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DiretoresCurso_Secretariados_IdUtilizador",
                table: "DiretoresCurso",
                column: "IdUtilizador",
                principalTable: "Secretariados",
                principalColumn: "IdUtilizador",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiretoresCurso_Secretariados_IdUtilizador",
                table: "DiretoresCurso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiretoresCurso",
                table: "DiretoresCurso");

            migrationBuilder.AlterColumn<int>(
                name: "EscolaId",
                table: "DiretoresCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "CursoId",
                table: "DiretoresCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "IdUtilizador",
                table: "DiretoresCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DiretoresCurso",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiretoresCurso",
                table: "DiretoresCurso",
                column: "Id");
        }
    }
}
