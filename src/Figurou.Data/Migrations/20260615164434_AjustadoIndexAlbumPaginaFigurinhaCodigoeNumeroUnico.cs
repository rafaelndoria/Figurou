using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Figurou.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjustadoIndexAlbumPaginaFigurinhaCodigoeNumeroUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Figurinhas_AlbumId_Codigo",
                table: "Figurinhas");

            migrationBuilder.DropIndex(
                name: "IX_Figurinhas_AlbumId_Numero",
                table: "Figurinhas");

            migrationBuilder.CreateIndex(
                name: "IX_Figurinhas_AlbumId_PaginaAlbumId_Codigo",
                table: "Figurinhas",
                columns: new[] { "AlbumId", "PaginaAlbumId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Figurinhas_AlbumId_PaginaAlbumId_Numero",
                table: "Figurinhas",
                columns: new[] { "AlbumId", "PaginaAlbumId", "Numero" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Figurinhas_AlbumId_PaginaAlbumId_Codigo",
                table: "Figurinhas");

            migrationBuilder.DropIndex(
                name: "IX_Figurinhas_AlbumId_PaginaAlbumId_Numero",
                table: "Figurinhas");

            migrationBuilder.CreateIndex(
                name: "IX_Figurinhas_AlbumId_Codigo",
                table: "Figurinhas",
                columns: new[] { "AlbumId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Figurinhas_AlbumId_Numero",
                table: "Figurinhas",
                columns: new[] { "AlbumId", "Numero" },
                unique: true);
        }
    }
}
