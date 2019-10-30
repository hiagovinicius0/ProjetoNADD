using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoNADD.Migrations
{
    public partial class TipoQuestao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoID",
                table: "Questao",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoQuestaoId_TipoQuestao",
                table: "Questao",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QuestoesMEeD_Avaliacao",
                table: "Avaliacao",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.CreateTable(
                name: "TipoQuestao",
                columns: table => new
                {
                    Id_TipoQuestao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome_TipoQuestao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoQuestao", x => x.Id_TipoQuestao);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questao_TipoQuestaoId_TipoQuestao",
                table: "Questao",
                column: "TipoQuestaoId_TipoQuestao");

            migrationBuilder.AddForeignKey(
                name: "FK_Questao_TipoQuestao_TipoQuestaoId_TipoQuestao",
                table: "Questao",
                column: "TipoQuestaoId_TipoQuestao",
                principalTable: "TipoQuestao",
                principalColumn: "Id_TipoQuestao",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questao_TipoQuestao_TipoQuestaoId_TipoQuestao",
                table: "Questao");

            migrationBuilder.DropTable(
                name: "TipoQuestao");

            migrationBuilder.DropIndex(
                name: "IX_Questao_TipoQuestaoId_TipoQuestao",
                table: "Questao");

            migrationBuilder.DropColumn(
                name: "TipoID",
                table: "Questao");

            migrationBuilder.DropColumn(
                name: "TipoQuestaoId_TipoQuestao",
                table: "Questao");

            migrationBuilder.AlterColumn<bool>(
                name: "QuestoesMEeD_Avaliacao",
                table: "Avaliacao",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
