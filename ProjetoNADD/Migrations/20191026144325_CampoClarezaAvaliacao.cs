using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoNADD.Migrations
{
    public partial class CampoClarezaAvaliacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Clareza_Avaliacao",
                table: "Avaliacao",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clareza_Avaliacao",
                table: "Avaliacao");
        }
    }
}
