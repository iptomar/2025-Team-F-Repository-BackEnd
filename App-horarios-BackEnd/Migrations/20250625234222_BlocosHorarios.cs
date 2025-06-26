using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class BlocosHorarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlocoHorario_BlocosAulas_BlocoAulaId",
                table: "BlocoHorario");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Disciplinas_DisciplinaId",
                table: "BlocosAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Professores_ProfessorId",
                table: "BlocosAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Salas_SalaId",
                table: "BlocosAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_TiposAula_TipoAulaId",
                table: "BlocosAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocosAulas_Turmas_TurmaId",
                table: "BlocosAulas");

            migrationBuilder.DropTable(
                name: "BlocoHorarioDTO");

            migrationBuilder.DropTable(
                name: "HorarioDTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlocosAulas",
                table: "BlocosAulas");

            migrationBuilder.RenameTable(
                name: "BlocosAulas",
                newName: "BlocoAula");

            migrationBuilder.RenameIndex(
                name: "IX_BlocosAulas_TurmaId",
                table: "BlocoAula",
                newName: "IX_BlocoAula_TurmaId");

            migrationBuilder.RenameIndex(
                name: "IX_BlocosAulas_TipoAulaId",
                table: "BlocoAula",
                newName: "IX_BlocoAula_TipoAulaId");

            migrationBuilder.RenameIndex(
                name: "IX_BlocosAulas_SalaId",
                table: "BlocoAula",
                newName: "IX_BlocoAula_SalaId");

            migrationBuilder.RenameIndex(
                name: "IX_BlocosAulas_ProfessorId",
                table: "BlocoAula",
                newName: "IX_BlocoAula_ProfessorId");

            migrationBuilder.RenameIndex(
                name: "IX_BlocosAulas_DisciplinaId",
                table: "BlocoAula",
                newName: "IX_BlocoAula_DisciplinaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlocoAula",
                table: "BlocoAula",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocoAula_Disciplinas_DisciplinaId",
                table: "BlocoAula",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlocoAula_Professores_ProfessorId",
                table: "BlocoAula",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocoAula_Salas_SalaId",
                table: "BlocoAula",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocoAula_TiposAula_TipoAulaId",
                table: "BlocoAula",
                column: "TipoAulaId",
                principalTable: "TiposAula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlocoAula_Turmas_TurmaId",
                table: "BlocoAula",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlocoHorario_BlocoAula_BlocoAulaId",
                table: "BlocoHorario",
                column: "BlocoAulaId",
                principalTable: "BlocoAula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlocoAula_Disciplinas_DisciplinaId",
                table: "BlocoAula");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocoAula_Professores_ProfessorId",
                table: "BlocoAula");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocoAula_Salas_SalaId",
                table: "BlocoAula");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocoAula_TiposAula_TipoAulaId",
                table: "BlocoAula");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocoAula_Turmas_TurmaId",
                table: "BlocoAula");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocoHorario_BlocoAula_BlocoAulaId",
                table: "BlocoHorario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlocoAula",
                table: "BlocoAula");

            migrationBuilder.RenameTable(
                name: "BlocoAula",
                newName: "BlocosAulas");

            migrationBuilder.RenameIndex(
                name: "IX_BlocoAula_TurmaId",
                table: "BlocosAulas",
                newName: "IX_BlocosAulas_TurmaId");

            migrationBuilder.RenameIndex(
                name: "IX_BlocoAula_TipoAulaId",
                table: "BlocosAulas",
                newName: "IX_BlocosAulas_TipoAulaId");

            migrationBuilder.RenameIndex(
                name: "IX_BlocoAula_SalaId",
                table: "BlocosAulas",
                newName: "IX_BlocosAulas_SalaId");

            migrationBuilder.RenameIndex(
                name: "IX_BlocoAula_ProfessorId",
                table: "BlocosAulas",
                newName: "IX_BlocosAulas_ProfessorId");

            migrationBuilder.RenameIndex(
                name: "IX_BlocoAula_DisciplinaId",
                table: "BlocosAulas",
                newName: "IX_BlocosAulas_DisciplinaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlocosAulas",
                table: "BlocosAulas",
                column: "Id");

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
                    BlocoAulaId = table.Column<int>(type: "integer", nullable: false),
                    DiaSemana = table.Column<int>(type: "integer", nullable: false),
                    HoraFim = table.Column<string>(type: "text", nullable: false),
                    HoraInicio = table.Column<string>(type: "text", nullable: false),
                    HorarioDTOId = table.Column<int>(type: "integer", nullable: true),
                    HorarioId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_BlocoHorario_BlocosAulas_BlocoAulaId",
                table: "BlocoHorario",
                column: "BlocoAulaId",
                principalTable: "BlocosAulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Disciplinas_DisciplinaId",
                table: "BlocosAulas",
                column: "DisciplinaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Professores_ProfessorId",
                table: "BlocosAulas",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Salas_SalaId",
                table: "BlocosAulas",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_TiposAula_TipoAulaId",
                table: "BlocosAulas",
                column: "TipoAulaId",
                principalTable: "TiposAula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosAulas_Turmas_TurmaId",
                table: "BlocosAulas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
