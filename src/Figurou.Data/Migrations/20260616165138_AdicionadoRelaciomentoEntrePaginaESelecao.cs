using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Figurou.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoRelaciomentoEntrePaginaESelecao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SelecaoId",
                table: "PaginasAlbum",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaginasAlbum_SelecaoId",
                table: "PaginasAlbum",
                column: "SelecaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaginasAlbum_Selecoes_SelecaoId",
                table: "PaginasAlbum",
                column: "SelecaoId",
                principalTable: "Selecoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaginasAlbum_Selecoes_SelecaoId",
                table: "PaginasAlbum");

            migrationBuilder.DropIndex(
                name: "IX_PaginasAlbum_SelecaoId",
                table: "PaginasAlbum");

            migrationBuilder.DropColumn(
                name: "SelecaoId",
                table: "PaginasAlbum");
        }
    }
}
