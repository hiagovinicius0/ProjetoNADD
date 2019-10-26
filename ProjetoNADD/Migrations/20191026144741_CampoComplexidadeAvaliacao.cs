using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoNADD.Migrations
{
    public partial class CampoComplexidadeAvaliacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
