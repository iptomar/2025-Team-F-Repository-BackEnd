using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Update_tblComissao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ComissoesCurso",
                table: "ComissoesCurso");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ComissoesCurso");

            migrationBuilder.AlterColumn<int>(
                name: "IdUtilizador",
                table: "ComissoesCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "EscolaId",
                table: "ComissoesCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "CursoId",
                table: "ComissoesCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComissoesCurso",
                table: "ComissoesCurso",
                columns: new[] { "IdUtilizador", "CursoId", "EscolaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ComissoesCurso_Secretariados_IdUtilizador",
                table: "ComissoesCurso",
                column: "IdUtilizador",
                principalTable: "Secretariados",
                principalColumn: "IdUtilizador",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComissoesCurso_Secretariados_IdUtilizador",
                table: "ComissoesCurso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComissoesCurso",
                table: "ComissoesCurso");

            migrationBuilder.AlterColumn<int>(
                name: "EscolaId",
                table: "ComissoesCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "CursoId",
                table: "ComissoesCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "IdUtilizador",
                table: "ComissoesCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ComissoesCurso",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComissoesCurso",
                table: "ComissoesCurso",
                column: "Id");
        }
    }
}
