using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Update_newtbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "HorarioDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TurmaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlocoHorarioDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiaSemana = table.Column<int>(type: "integer", nullable: false),
                    HoraInicio = table.Column<string>(type: "text", nullable: false),
                    HoraFim = table.Column<string>(type: "text", nullable: false),
                    BlocoAulaId = table.Column<int>(type: "integer", nullable: false),
                    HorarioId = table.Column<int>(type: "integer", nullable: false),
                    HorarioDTOId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlocoHorarioDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlocoHorarioDTO_HorarioDTO_HorarioDTOId",
                        column: x => x.HorarioDTOId,
                        principalTable: "HorarioDTO",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlocoHorarioDTO_HorarioDTOId",
                table: "BlocoHorarioDTO",
                column: "HorarioDTOId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlocoHorarioDTO");

            migrationBuilder.DropTable(
                name: "HorarioDTO");

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

            migrationBuilder.AlterColumn<int>(
                name: "IdUtilizador",
                table: "ComissoesCurso",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 0);
        }
    }
}
