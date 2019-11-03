using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoNADD.Migrations
{
    public partial class CampoCursoNoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Curso",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Curso",
                table: "AspNetUsers");
        }
    }
}
