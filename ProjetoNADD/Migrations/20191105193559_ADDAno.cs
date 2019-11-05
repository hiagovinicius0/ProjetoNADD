using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoNADD.Migrations
{
    public partial class ADDAno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ano_id",
                table: "Disciplina",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ano",
                columns: table => new
                {
                    Ano_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ano", x => x.Ano_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_Ano_id",
                table: "Disciplina",
                column: "Ano_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplina_Ano_Ano_id",
                table: "Disciplina",
                column: "Ano_id",
                principalTable: "Ano",
                principalColumn: "Ano_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplina_Ano_Ano_id",
                table: "Disciplina");

            migrationBuilder.DropTable(
                name: "Ano");

            migrationBuilder.DropIndex(
                name: "IX_Disciplina_Ano_id",
                table: "Disciplina");

            migrationBuilder.DropColumn(
                name: "Ano_id",
                table: "Disciplina");
        }
    }
}
