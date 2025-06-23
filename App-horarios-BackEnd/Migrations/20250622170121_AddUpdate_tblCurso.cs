using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdate_tblCurso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComissoesCurso_Cursos_CursoId_CursoEscolaId",
                table: "ComissoesCurso");

            migrationBuilder.DropForeignKey(
                name: "FK_DiretoresCurso_Cursos_CursoId_CursoEscolaId",
                table: "DiretoresCurso");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_Cursos_CursoId_CursoEscolaId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_Cursos_CursoId_EscolaId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_Secretariados_Cursos_CursoId_EscolaId",
                table: "Secretariados");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Cursos_CursoId_CursoEscolaId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_CursoId_CursoEscolaId",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Secretariados",
                table: "Secretariados");

            migrationBuilder.DropIndex(
                name: "IX_Secretariados_CursoId_EscolaId",
                table: "Secretariados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DisciplinaCursoProfessor",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinaCursoProfessor_CursoId_CursoEscolaId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropIndex(
                name: "IX_DiretoresCurso_CursoId_CursoEscolaId",
                table: "DiretoresCurso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_ComissoesCurso_CursoId_CursoEscolaId",
                table: "ComissoesCurso");

            migrationBuilder.DropColumn(
                name: "CursoEscolaId",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "CursoEscolaId",
                table: "DiretoresCurso");

            migrationBuilder.DropColumn(
                name: "CursoEscolaId",
                table: "ComissoesCurso");

            migrationBuilder.RenameColumn(
                name: "CursoEscolaId",
                table: "DisciplinaCursoProfessor",
                newName: "CursoId1");

            migrationBuilder.AlterColumn<int>(
                name: "IdUtilizador",
                table: "Secretariados",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cursos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Secretariados",
                table: "Secretariados",
                column: "IdUtilizador");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisciplinaCursoProfessor",
                table: "DisciplinaCursoProfessor",
                columns: new[] { "CursoId", "DisciplinaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_CursoId",
                table: "Turmas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Secretariados_CursoId",
                table: "Secretariados",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaCursoProfessor_CursoId1",
                table: "DisciplinaCursoProfessor",
                column: "CursoId1");

            migrationBuilder.CreateIndex(
                name: "IX_DiretoresCurso_CursoId",
                table: "DiretoresCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComissoesCurso_CursoId",
                table: "ComissoesCurso",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComissoesCurso_Cursos_CursoId",
                table: "ComissoesCurso",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiretoresCurso_Cursos_CursoId",
                table: "DiretoresCurso",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaCursoProfessor_Cursos_CursoId",
                table: "DisciplinaCursoProfessor",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaCursoProfessor_Cursos_CursoId1",
                table: "DisciplinaCursoProfessor",
                column: "CursoId1",
                principalTable: "Cursos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Secretariados_Cursos_CursoId",
                table: "Secretariados",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Cursos_CursoId",
                table: "Turmas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComissoesCurso_Cursos_CursoId",
                table: "ComissoesCurso");

            migrationBuilder.DropForeignKey(
                name: "FK_DiretoresCurso_Cursos_CursoId",
                table: "DiretoresCurso");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_Cursos_CursoId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_Cursos_CursoId1",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_Secretariados_Cursos_CursoId",
                table: "Secretariados");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Cursos_CursoId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_CursoId",
                table: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Secretariados",
                table: "Secretariados");

            migrationBuilder.DropIndex(
                name: "IX_Secretariados_CursoId",
                table: "Secretariados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DisciplinaCursoProfessor",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinaCursoProfessor_CursoId1",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropIndex(
                name: "IX_DiretoresCurso_CursoId",
                table: "DiretoresCurso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_ComissoesCurso_CursoId",
                table: "ComissoesCurso");

            migrationBuilder.RenameColumn(
                name: "CursoId1",
                table: "DisciplinaCursoProfessor",
                newName: "CursoEscolaId");

            migrationBuilder.AddColumn<int>(
                name: "CursoEscolaId",
                table: "Turmas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdUtilizador",
                table: "Secretariados",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CursoEscolaId",
                table: "DiretoresCurso",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cursos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CursoEscolaId",
                table: "ComissoesCurso",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Secretariados",
                table: "Secretariados",
                columns: new[] { "IdUtilizador", "CursoId", "EscolaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisciplinaCursoProfessor",
                table: "DisciplinaCursoProfessor",
                columns: new[] { "CursoId", "EscolaId", "DisciplinaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos",
                columns: new[] { "Id", "EscolaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_CursoId_CursoEscolaId",
                table: "Turmas",
                columns: new[] { "CursoId", "CursoEscolaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Secretariados_CursoId_EscolaId",
                table: "Secretariados",
                columns: new[] { "CursoId", "EscolaId" });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaCursoProfessor_CursoId_CursoEscolaId",
                table: "DisciplinaCursoProfessor",
                columns: new[] { "CursoId", "CursoEscolaId" });

            migrationBuilder.CreateIndex(
                name: "IX_DiretoresCurso_CursoId_CursoEscolaId",
                table: "DiretoresCurso",
                columns: new[] { "CursoId", "CursoEscolaId" });

            migrationBuilder.CreateIndex(
                name: "IX_ComissoesCurso_CursoId_CursoEscolaId",
                table: "ComissoesCurso",
                columns: new[] { "CursoId", "CursoEscolaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ComissoesCurso_Cursos_CursoId_CursoEscolaId",
                table: "ComissoesCurso",
                columns: new[] { "CursoId", "CursoEscolaId" },
                principalTable: "Cursos",
                principalColumns: new[] { "Id", "EscolaId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiretoresCurso_Cursos_CursoId_CursoEscolaId",
                table: "DiretoresCurso",
                columns: new[] { "CursoId", "CursoEscolaId" },
                principalTable: "Cursos",
                principalColumns: new[] { "Id", "EscolaId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaCursoProfessor_Cursos_CursoId_CursoEscolaId",
                table: "DisciplinaCursoProfessor",
                columns: new[] { "CursoId", "CursoEscolaId" },
                principalTable: "Cursos",
                principalColumns: new[] { "Id", "EscolaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaCursoProfessor_Cursos_CursoId_EscolaId",
                table: "DisciplinaCursoProfessor",
                columns: new[] { "CursoId", "EscolaId" },
                principalTable: "Cursos",
                principalColumns: new[] { "Id", "EscolaId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Secretariados_Cursos_CursoId_EscolaId",
                table: "Secretariados",
                columns: new[] { "CursoId", "EscolaId" },
                principalTable: "Cursos",
                principalColumns: new[] { "Id", "EscolaId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Cursos_CursoId_CursoEscolaId",
                table: "Turmas",
                columns: new[] { "CursoId", "CursoEscolaId" },
                principalTable: "Cursos",
                principalColumns: new[] { "Id", "EscolaId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
