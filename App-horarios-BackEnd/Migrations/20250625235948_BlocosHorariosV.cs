using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class BlocosHorariosV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_BlocoHorario_Horarios_HorarioId",
                table: "BlocoHorario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlocoHorario",
                table: "BlocoHorario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlocoAula",
                table: "BlocoAula");

            migrationBuilder.RenameTable(
                name: "BlocoHorario",
                newName: "BlocosHorarios");

            migrationBuilder.RenameTable(
                name: "BlocoAula",
                newName: "BlocosAulas");

            migrationBuilder.RenameIndex(
                name: "IX_BlocoHorario_HorarioId",
                table: "BlocosHorarios",
                newName: "IX_BlocosHorarios_HorarioId");

            migrationBuilder.RenameIndex(
                name: "IX_BlocoHorario_BlocoAulaId",
                table: "BlocosHorarios",
                newName: "IX_BlocosHorarios_BlocoAulaId");

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
                name: "PK_BlocosHorarios",
                table: "BlocosHorarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlocosAulas",
                table: "BlocosAulas",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosHorarios_BlocosAulas_BlocoAulaId",
                table: "BlocosHorarios",
                column: "BlocoAulaId",
                principalTable: "BlocosAulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlocosHorarios_Horarios_HorarioId",
                table: "BlocosHorarios",
                column: "HorarioId",
                principalTable: "Horarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropForeignKey(
                name: "FK_BlocosHorarios_BlocosAulas_BlocoAulaId",
                table: "BlocosHorarios");

            migrationBuilder.DropForeignKey(
                name: "FK_BlocosHorarios_Horarios_HorarioId",
                table: "BlocosHorarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlocosHorarios",
                table: "BlocosHorarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlocosAulas",
                table: "BlocosAulas");

            migrationBuilder.RenameTable(
                name: "BlocosHorarios",
                newName: "BlocoHorario");

            migrationBuilder.RenameTable(
                name: "BlocosAulas",
                newName: "BlocoAula");

            migrationBuilder.RenameIndex(
                name: "IX_BlocosHorarios_HorarioId",
                table: "BlocoHorario",
                newName: "IX_BlocoHorario_HorarioId");

            migrationBuilder.RenameIndex(
                name: "IX_BlocosHorarios_BlocoAulaId",
                table: "BlocoHorario",
                newName: "IX_BlocoHorario_BlocoAulaId");

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
                name: "PK_BlocoHorario",
                table: "BlocoHorario",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BlocoHorario_Horarios_HorarioId",
                table: "BlocoHorario",
                column: "HorarioId",
                principalTable: "Horarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
