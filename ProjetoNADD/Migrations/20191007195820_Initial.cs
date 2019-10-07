using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoNADD.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id_Area = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome_Area = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id_Area);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id_Professor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome_Professor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id_Professor);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id_Curso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome_Curso = table.Column<string>(nullable: true),
                    AreaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id_Curso);
                    table.ForeignKey(
                        name: "FK_Curso_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id_Area",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplina",
                columns: table => new
                {
                    Id_Disciplina = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome_Disciplina = table.Column<string>(nullable: true),
                    Periodo_Disciplina = table.Column<int>(nullable: false),
                    Ano_Disciplina = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplina", x => x.Id_Disciplina);
                    table.ForeignKey(
                        name: "FK_Disciplina_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id_Curso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    Id_Avaliacao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome_Avaliacao = table.Column<string>(nullable: true),
                    ValorExplicitoProva_Avaliacao = table.Column<bool>(nullable: false),
                    ValorExplicitoQuestoes_Avaliacao = table.Column<bool>(nullable: false),
                    SomatorioQuestoes_Avaliacao = table.Column<bool>(nullable: false),
                    Referencias_Avaliacao = table.Column<bool>(nullable: false),
                    QuestoesMEeD_Avaliacao = table.Column<bool>(nullable: false),
                    ValorProva_Avaliacao = table.Column<double>(nullable: false),
                    NumeroQuestoes_Avaliacao = table.Column<int>(nullable: false),
                    EquilibrioValorQuestoes_Avaliacao = table.Column<bool>(nullable: false),
                    Diversificacao_Avaliacao = table.Column<bool>(nullable: false),
                    Contextualidade_Avaliacao = table.Column<bool>(nullable: false),
                    Observacoes_Avaliacao = table.Column<string>(nullable: true),
                    DisciplinaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id_Avaliacao);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id_Disciplina",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaProfessor",
                columns: table => new
                {
                    Professor_id = table.Column<int>(nullable: false),
                    Disciplina_id = table.Column<int>(nullable: false),
                    DisciplinaId_Disciplina = table.Column<int>(nullable: true),
                    ProfessorId_Professor = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaProfessor", x => new { x.Disciplina_id, x.Professor_id });
                    table.ForeignKey(
                        name: "FK_DisciplinaProfessor_Disciplina_DisciplinaId_Disciplina",
                        column: x => x.DisciplinaId_Disciplina,
                        principalTable: "Disciplina",
                        principalColumn: "Id_Disciplina",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DisciplinaProfessor_Professor_ProfessorId_Professor",
                        column: x => x.ProfessorId_Professor,
                        principalTable: "Professor",
                        principalColumn: "Id_Professor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questao",
                columns: table => new
                {
                    Id_Questao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Id_Numero = table.Column<int>(nullable: false),
                    Id_Avaliacao = table.Column<int>(nullable: false),
                    Contextualizacao_Questao = table.Column<bool>(nullable: false),
                    Clareza_Questao = table.Column<bool>(nullable: false),
                    Complexidade_Questao = table.Column<bool>(nullable: false),
                    Observacoes_Questao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questao", x => x.Id_Questao);
                    table.ForeignKey(
                        name: "FK_Questao_Avaliacao_Id_Avaliacao",
                        column: x => x.Id_Avaliacao,
                        principalTable: "Avaliacao",
                        principalColumn: "Id_Avaliacao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_DisciplinaId",
                table: "Avaliacao",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_AreaId",
                table: "Curso",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_CursoId",
                table: "Disciplina",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaProfessor_DisciplinaId_Disciplina",
                table: "DisciplinaProfessor",
                column: "DisciplinaId_Disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaProfessor_ProfessorId_Professor",
                table: "DisciplinaProfessor",
                column: "ProfessorId_Professor");

            migrationBuilder.CreateIndex(
                name: "IX_Questao_Id_Avaliacao",
                table: "Questao",
                column: "Id_Avaliacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplinaProfessor");

            migrationBuilder.DropTable(
                name: "Questao");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "Disciplina");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Area");
        }
    }
}
