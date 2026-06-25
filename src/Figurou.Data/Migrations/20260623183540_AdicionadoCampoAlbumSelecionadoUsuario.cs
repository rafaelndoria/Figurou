using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Figurou.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoCampoAlbumSelecionadoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AlbumEscolhidoId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_AlbumEscolhidoId",
                table: "Usuarios",
                column: "AlbumEscolhidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Albuns_AlbumEscolhidoId",
                table: "Usuarios",
                column: "AlbumEscolhidoId",
                principalTable: "Albuns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Albuns_AlbumEscolhidoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_AlbumEscolhidoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AlbumEscolhidoId",
                table: "Usuarios");
        }
    }
}
