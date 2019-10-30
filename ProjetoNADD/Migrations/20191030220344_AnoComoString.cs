using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoNADD.Migrations
{
    public partial class AnoComoString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ano_Disciplina",
                table: "Disciplina",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Ano_Disciplina",
                table: "Disciplina",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
