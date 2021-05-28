using Microsoft.EntityFrameworkCore.Migrations;

namespace Locadora.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Filme");

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "Filme",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Filme_GeneroId",
                table: "Filme",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filme_Genero_GeneroId",
                table: "Filme",
                column: "GeneroId",
                principalTable: "Genero",
                principalColumn: "GeneroId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filme_Genero_GeneroId",
                table: "Filme");

            migrationBuilder.DropIndex(
                name: "IX_Filme_GeneroId",
                table: "Filme");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "Filme");

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Filme",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
