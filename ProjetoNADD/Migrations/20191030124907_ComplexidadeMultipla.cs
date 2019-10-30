using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoNADD.Migrations
{
    public partial class ComplexidadeMultipla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complexidade_Questao",
                table: "Questao");

            migrationBuilder.DropColumn(
                name: "Complexidade_Avaliacao",
                table: "Avaliacao");

            migrationBuilder.AddColumn<int>(
                name: "ComplexidadeID",
                table: "Questao",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComplexidadeID",
                table: "Avaliacao",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Complexidade",
                columns: table => new
                {
                    Id_Complexidade = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome_Complexidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complexidade", x => x.Id_Complexidade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questao_ComplexidadeID",
                table: "Questao",
                column: "ComplexidadeID");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_ComplexidadeID",
                table: "Avaliacao",
                column: "ComplexidadeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacao_Complexidade_ComplexidadeID",
                table: "Avaliacao",
                column: "ComplexidadeID",
                principalTable: "Complexidade",
                principalColumn: "Id_Complexidade",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questao_Complexidade_ComplexidadeID",
                table: "Questao",
                column: "ComplexidadeID",
                principalTable: "Complexidade",
                principalColumn: "Id_Complexidade",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_Complexidade_ComplexidadeID",
                table: "Avaliacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Questao_Complexidade_ComplexidadeID",
                table: "Questao");

            migrationBuilder.DropTable(
                name: "Complexidade");

            migrationBuilder.DropIndex(
                name: "IX_Questao_ComplexidadeID",
                table: "Questao");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacao_ComplexidadeID",
                table: "Avaliacao");

            migrationBuilder.DropColumn(
                name: "ComplexidadeID",
                table: "Questao");

            migrationBuilder.DropColumn(
                name: "ComplexidadeID",
                table: "Avaliacao");

            migrationBuilder.AddColumn<string>(
                name: "Complexidade_Questao",
                table: "Questao",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complexidade_Avaliacao",
                table: "Avaliacao",
                nullable: true);
        }
    }
}
