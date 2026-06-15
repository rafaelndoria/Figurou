using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Figurou.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoNomeJogadorFigurinha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeJogador",
                table: "Figurinhas",
                type: "varchar(100)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeJogador",
                table: "Figurinhas");
        }
    }
}
