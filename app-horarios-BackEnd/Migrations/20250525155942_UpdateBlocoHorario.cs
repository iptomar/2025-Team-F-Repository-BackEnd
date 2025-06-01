using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlocoHorario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraFim",
                table: "BlocosHorario");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "BlocosHorario");

            migrationBuilder.AddColumn<int>(
                name: "Duracao",
                table: "BlocosHorario",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "BlocosHorario");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraFim",
                table: "BlocosHorario",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraInicio",
                table: "BlocosHorario",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
