using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoNADD.Migrations
{
    public partial class SemComplexidadeAvaliacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacao_Complexidade_ComplexidadeID",
                table: "Avaliacao");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacao_ComplexidadeID",
                table: "Avaliacao");

            migrationBuilder.DropColumn(
                name: "ComplexidadeID",
                table: "Avaliacao");

            migrationBuilder.AddColumn<string>(
                name: "Complexidade_Avaliacao",
                table: "Avaliacao",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complexidade_Avaliacao",
                table: "Avaliacao");

            migrationBuilder.AddColumn<int>(
                name: "ComplexidadeID",
                table: "Avaliacao",
                nullable: true);

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
        }
    }
}
