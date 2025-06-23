using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Update_tbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Horarios_HorarioId",
                table: "BlocosAulas");

            migrationBuilder.DropIndex(
                name: "IX_BlocosAulas_HorarioId",
                table: "BlocosAulas");

            migrationBuilder.DropColumn(
                name: "HorarioId",
                table: "BlocosAulas");

            migrationBuilder.CreateTable(
                name: "BlocoHorario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiaSemana = table.Column<int>(type: "integer", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    HoraFim = table.Column<TimeSpan>(type: "interval", nullable: false),
                    BlocoAulaId = table.Column<int>(type: "integer", nullable: false),
                    HorarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlocoHorario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlocoHorario_BlocosAulas_BlocoAulaId",
                        column: x => x.BlocoAulaId,
                        principalTable: "BlocosAulas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlocoHorario_Horarios_HorarioId",
                        column: x => x.HorarioId,
                        principalTable: "Horarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlocoHorario_BlocoAulaId",
                table: "BlocoHorario",
                column: "BlocoAulaId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocoHorario_HorarioId",
                table: "BlocoHorario",
                column: "HorarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlocoHorario");

            migrationBuilder.AddColumn<int>(
                name: "HorarioId",
                table: "BlocosAulas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlocosAulas_HorarioId",
                table: "BlocosAulas",
                column: "HorarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Horarios_HorarioId",
                table: "BlocosAulas",
                column: "HorarioId",
                principalTable: "Horarios",
                principalColumn: "Id");
        }
    }
}
