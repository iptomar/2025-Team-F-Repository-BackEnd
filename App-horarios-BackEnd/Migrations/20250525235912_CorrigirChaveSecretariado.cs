using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace app_horarios_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirChaveSecretariado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComissoesCurso_Cursos_CursoId",
                table: "ComissoesCurso");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Ramos_RamoId",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_DiretoresCurso_Cursos_CursoId",
                table: "DiretoresCurso");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaCursoProfessor_Cursos_CursoId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_Secretariados_Cursos_CursoId",
                table: "Secretariados");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Cursos_CursoId",
                table: "Turmas");

            migrationBuilder.DropTable(
                name: "Ramos");

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
                name: "IX_DisciplinaCursoProfessor_CursoId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropIndex(
                name: "IX_DiretoresCurso_CursoId",
                table: "DiretoresCurso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_RamoId",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_ComissoesCurso_CursoId",
                table: "ComissoesCurso");

            migrationBuilder.DropColumn(
                name: "RamoId",
                table: "Cursos");

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

            migrationBuilder.AlterColumn<string>(
                name: "Semestre",
                table: "Disciplinas",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisciplinaId",
                table: "Disciplinas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlanoId",
                table: "Disciplinas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ramo",
                table: "Disciplinas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RamoId",
                table: "Disciplinas",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DisciplinaCursoProfessor",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "EscolaId",
                table: "DisciplinaCursoProfessor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CursoEscolaId",
                table: "DisciplinaCursoProfessor",
                type: "integer",
                nullable: true);

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
                name: "IX_DisciplinaCursoProfessor_EscolaId",
                table: "DisciplinaCursoProfessor",
                column: "EscolaId");

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
                name: "FK_DisciplinaCursoProfessor_Disciplinas_EscolaId",
                table: "DisciplinaCursoProfessor",
                column: "EscolaId",
                principalTable: "Disciplinas",
                principalColumn: "Id",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_DisciplinaCursoProfessor_Disciplinas_EscolaId",
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
                name: "IX_DisciplinaCursoProfessor_EscolaId",
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
                name: "DisciplinaId",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "PlanoId",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "Ramo",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "RamoId",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropColumn(
                name: "CursoEscolaId",
                table: "DisciplinaCursoProfessor");

            migrationBuilder.DropColumn(
                name: "CursoEscolaId",
                table: "DiretoresCurso");

            migrationBuilder.DropColumn(
                name: "CursoEscolaId",
                table: "ComissoesCurso");

            migrationBuilder.AlterColumn<int>(
                name: "IdUtilizador",
                table: "Secretariados",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Semestre",
                table: "Disciplinas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DisciplinaCursoProfessor",
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

            migrationBuilder.AddColumn<int>(
                name: "RamoId",
                table: "Cursos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Secretariados",
                table: "Secretariados",
                column: "IdUtilizador");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisciplinaCursoProfessor",
                table: "DisciplinaCursoProfessor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Ramos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ramos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_CursoId",
                table: "Turmas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Secretariados_CursoId",
                table: "Secretariados",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaCursoProfessor_CursoId",
                table: "DisciplinaCursoProfessor",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_DiretoresCurso_CursoId",
                table: "DiretoresCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_RamoId",
                table: "Cursos",
                column: "RamoId");

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
                name: "FK_Cursos_Ramos_RamoId",
                table: "Cursos",
                column: "RamoId",
                principalTable: "Ramos",
                principalColumn: "Id");

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
    }
}
