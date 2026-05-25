using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Figurou.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albuns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 250, nullable: false),
                    ImagemCapa = table.Column<string>(type: "varchar(100)", maxLength: 300, nullable: false),
                    TotalFigurinhas = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albuns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 150, nullable: false),
                    SenhaCodificada = table.Column<string>(type: "varchar(100)", maxLength: 500, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaginasAlbum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroPagina = table.Column<int>(type: "int", nullable: false),
                    ImagemPagina = table.Column<string>(type: "varchar(100)", maxLength: 500, nullable: false),
                    Largura = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Altura = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaginasAlbum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaginasAlbum_Albuns_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Selecoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(100)", maxLength: 3, nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Selecoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Selecoes_Albuns_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Compatibilidade = table.Column<int>(type: "int", nullable: false),
                    UsuarioOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioDestinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.CheckConstraint("CK_Match_Compatibilidade", "[Compatibilidade] >= 0 AND [Compatibilidade] <= 100");
                    table.ForeignKey(
                        name: "FK_Matches_Usuarios_UsuarioDestinoId",
                        column: x => x.UsuarioDestinoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Usuarios_UsuarioOrigemId",
                        column: x => x.UsuarioOrigemId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trocas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(100)", maxLength: 200, nullable: true),
                    DataRequisicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinalizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SolicitanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinatarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trocas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trocas_Usuarios_DestinatarioId",
                        column: x => x.DestinatarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trocas_Usuarios_SolicitanteId",
                        column: x => x.SolicitanteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioDetalhes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Sobrenome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "varchar(100)", maxLength: 2, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Imagem = table.Column<string>(type: "varchar(100)", maxLength: 150, nullable: false),
                    Reputacao = table.Column<int>(type: "int", nullable: false),
                    Experiencia = table.Column<int>(type: "int", nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioDetalhes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioDetalhes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Figurinhas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(100)", maxLength: 10, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Raridade = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaginaAlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Figurinhas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Figurinhas_Albuns_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Figurinhas_PaginasAlbum_PaginaAlbumId",
                        column: x => x.PaginaAlbumId,
                        principalTable: "PaginasAlbum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Conversas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrocaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversas_Trocas_TrocaId",
                        column: x => x.TrocaId,
                        principalTable: "Trocas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FigurinhasUsuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PossuiNoAlbum = table.Column<bool>(type: "bit", nullable: false),
                    QuantidadeRepetida = table.Column<int>(type: "int", nullable: false),
                    DisponivelTroca = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FigurinhaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FigurinhasUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FigurinhasUsuario_Figurinhas_FigurinhaId",
                        column: x => x.FigurinhaId,
                        principalTable: "Figurinhas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FigurinhasUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlotsPaginaAlbum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PosicaoX = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    PosicaoY = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Largura = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Altura = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    PaginaAlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FigurinhaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotsPaginaAlbum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlotsPaginaAlbum_Figurinhas_FigurinhaId",
                        column: x => x.FigurinhaId,
                        principalTable: "Figurinhas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SlotsPaginaAlbum_PaginasAlbum_PaginaAlbumId",
                        column: x => x.PaginaAlbumId,
                        principalTable: "PaginasAlbum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrocaItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    TrocaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FigurinhaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrocaItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrocaItens_Figurinhas_FigurinhaId",
                        column: x => x.FigurinhaId,
                        principalTable: "Figurinhas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrocaItens_Trocas_TrocaId",
                        column: x => x.TrocaId,
                        principalTable: "Trocas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrocaItens_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mensagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Conteudo = table.Column<string>(type: "varchar(100)", maxLength: 1000, nullable: false),
                    Lida = table.Column<bool>(type: "bit", nullable: false),
                    Editada = table.Column<bool>(type: "bit", nullable: false),
                    Excluida = table.Column<bool>(type: "bit", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensagens_Conversas_ConversaId",
                        column: x => x.ConversaId,
                        principalTable: "Conversas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mensagens_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albuns_Nome_Ano",
                table: "Albuns",
                columns: new[] { "Nome", "Ano" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversas_TrocaId",
                table: "Conversas",
                column: "TrocaId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Figurinhas_PaginaAlbumId",
                table: "Figurinhas",
                column: "PaginaAlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_FigurinhasUsuario_FigurinhaId",
                table: "FigurinhasUsuario",
                column: "FigurinhaId");

            migrationBuilder.CreateIndex(
                name: "IX_FigurinhasUsuario_UsuarioId",
                table: "FigurinhasUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UsuarioDestinoId",
                table: "Matches",
                column: "UsuarioDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UsuarioOrigemId_UsuarioDestinoId",
                table: "Matches",
                columns: new[] { "UsuarioOrigemId", "UsuarioDestinoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_ConversaId",
                table: "Mensagens",
                column: "ConversaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_DataEnvio",
                table: "Mensagens",
                column: "DataEnvio");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_UsuarioId",
                table: "Mensagens",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PaginasAlbum_AlbumId",
                table: "PaginasAlbum",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_PaginasAlbum_AlbumId_NumeroPagina",
                table: "PaginasAlbum",
                columns: new[] { "AlbumId", "NumeroPagina" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Selecoes_AlbumId",
                table: "Selecoes",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Selecoes_AlbumId_Codigo",
                table: "Selecoes",
                columns: new[] { "AlbumId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Selecoes_AlbumId_Nome",
                table: "Selecoes",
                columns: new[] { "AlbumId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SlotsPaginaAlbum_FigurinhaId",
                table: "SlotsPaginaAlbum",
                column: "FigurinhaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SlotsPaginaAlbum_PaginaAlbumId",
                table: "SlotsPaginaAlbum",
                column: "PaginaAlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_SlotsPaginaAlbum_PaginaAlbumId_Ordem",
                table: "SlotsPaginaAlbum",
                columns: new[] { "PaginaAlbumId", "Ordem" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrocaItens_FigurinhaId",
                table: "TrocaItens",
                column: "FigurinhaId");

            migrationBuilder.CreateIndex(
                name: "IX_TrocaItens_TrocaId",
                table: "TrocaItens",
                column: "TrocaId");

            migrationBuilder.CreateIndex(
                name: "IX_TrocaItens_TrocaId_UsuarioId_FigurinhaId",
                table: "TrocaItens",
                columns: new[] { "TrocaId", "UsuarioId", "FigurinhaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrocaItens_UsuarioId",
                table: "TrocaItens",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_DestinatarioId",
                table: "Trocas",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_SolicitanteId",
                table: "Trocas",
                column: "SolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_Status",
                table: "Trocas",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioDetalhes_Cidade",
                table: "UsuarioDetalhes",
                column: "Cidade");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioDetalhes_Estado",
                table: "UsuarioDetalhes",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioDetalhes_UsuarioId",
                table: "UsuarioDetalhes",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Username",
                table: "Usuarios",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FigurinhasUsuario");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Mensagens");

            migrationBuilder.DropTable(
                name: "Selecoes");

            migrationBuilder.DropTable(
                name: "SlotsPaginaAlbum");

            migrationBuilder.DropTable(
                name: "TrocaItens");

            migrationBuilder.DropTable(
                name: "UsuarioDetalhes");

            migrationBuilder.DropTable(
                name: "Conversas");

            migrationBuilder.DropTable(
                name: "Figurinhas");

            migrationBuilder.DropTable(
                name: "Trocas");

            migrationBuilder.DropTable(
                name: "PaginasAlbum");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Albuns");
        }
    }
}
