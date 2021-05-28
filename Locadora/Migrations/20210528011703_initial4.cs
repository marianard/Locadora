using Microsoft.EntityFrameworkCore.Migrations;

namespace Locadora.Migrations
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filme_Locacao_LocacaoId",
                table: "Filme");

            migrationBuilder.DropIndex(
                name: "IX_Filme_LocacaoId",
                table: "Filme");

            migrationBuilder.DropColumn(
                name: "LocacaoId",
                table: "Filme");

            migrationBuilder.CreateTable(
                name: "FilmeLocacao",
                columns: table => new
                {
                    ListaFilmeFilmeId = table.Column<int>(type: "int", nullable: false),
                    ListaLocacaoLocacaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmeLocacao", x => new { x.ListaFilmeFilmeId, x.ListaLocacaoLocacaoId });
                    table.ForeignKey(
                        name: "FK_FilmeLocacao_Filme_ListaFilmeFilmeId",
                        column: x => x.ListaFilmeFilmeId,
                        principalTable: "Filme",
                        principalColumn: "FilmeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmeLocacao_Locacao_ListaLocacaoLocacaoId",
                        column: x => x.ListaLocacaoLocacaoId,
                        principalTable: "Locacao",
                        principalColumn: "LocacaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmeLocacao_ListaLocacaoLocacaoId",
                table: "FilmeLocacao",
                column: "ListaLocacaoLocacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmeLocacao");

            migrationBuilder.AddColumn<int>(
                name: "LocacaoId",
                table: "Filme",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Filme_LocacaoId",
                table: "Filme",
                column: "LocacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filme_Locacao_LocacaoId",
                table: "Filme",
                column: "LocacaoId",
                principalTable: "Locacao",
                principalColumn: "LocacaoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
