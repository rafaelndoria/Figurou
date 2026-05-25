IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Albuns] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(100) NOT NULL,
    [Ano] int NOT NULL,
    [Descricao] varchar(100) NOT NULL,
    [ImagemCapa] varchar(100) NOT NULL,
    [TotalFigurinhas] int NOT NULL,
    [Ativo] bit NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    CONSTRAINT [PK_Albuns] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Usuarios] (
    [Id] uniqueidentifier NOT NULL,
    [Username] varchar(100) NOT NULL,
    [Email] varchar(100) NOT NULL,
    [SenhaCodificada] varchar(100) NOT NULL,
    [Ativo] bit NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PaginasAlbum] (
    [Id] uniqueidentifier NOT NULL,
    [NumeroPagina] int NOT NULL,
    [ImagemPagina] varchar(100) NOT NULL,
    [Largura] decimal(10,4) NOT NULL,
    [Altura] decimal(10,4) NOT NULL,
    [AlbumId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_PaginasAlbum] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PaginasAlbum_Albuns_AlbumId] FOREIGN KEY ([AlbumId]) REFERENCES [Albuns] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Selecoes] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] varchar(100) NOT NULL,
    [Nome] varchar(100) NOT NULL,
    [AlbumId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Selecoes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Selecoes_Albuns_AlbumId] FOREIGN KEY ([AlbumId]) REFERENCES [Albuns] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Matches] (
    [Id] uniqueidentifier NOT NULL,
    [Compatibilidade] int NOT NULL,
    [UsuarioOrigemId] uniqueidentifier NOT NULL,
    [UsuarioDestinoId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Matches] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Match_Compatibilidade] CHECK ([Compatibilidade] >= 0 AND [Compatibilidade] <= 100),
    CONSTRAINT [FK_Matches_Usuarios_UsuarioDestinoId] FOREIGN KEY ([UsuarioDestinoId]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Matches_Usuarios_UsuarioOrigemId] FOREIGN KEY ([UsuarioOrigemId]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Trocas] (
    [Id] uniqueidentifier NOT NULL,
    [Status] int NOT NULL,
    [Observacao] varchar(100) NULL,
    [DataRequisicao] datetime2 NOT NULL,
    [DataFinalizacao] datetime2 NULL,
    [SolicitanteId] uniqueidentifier NOT NULL,
    [DestinatarioId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Trocas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Trocas_Usuarios_DestinatarioId] FOREIGN KEY ([DestinatarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Trocas_Usuarios_SolicitanteId] FOREIGN KEY ([SolicitanteId]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [UsuarioDetalhes] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(100) NOT NULL,
    [Sobrenome] varchar(100) NOT NULL,
    [Estado] varchar(100) NOT NULL,
    [Cidade] varchar(100) NOT NULL,
    [Imagem] varchar(100) NOT NULL,
    [Reputacao] int NOT NULL,
    [Experiencia] int NOT NULL,
    [Nivel] int NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [UsuarioId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UsuarioDetalhes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UsuarioDetalhes_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Figurinhas] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] varchar(100) NOT NULL,
    [Numero] int NOT NULL,
    [Raridade] nvarchar(20) NOT NULL,
    [AlbumId] uniqueidentifier NOT NULL,
    [PaginaAlbumId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Figurinhas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Figurinhas_Albuns_AlbumId] FOREIGN KEY ([AlbumId]) REFERENCES [Albuns] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Figurinhas_PaginasAlbum_PaginaAlbumId] FOREIGN KEY ([PaginaAlbumId]) REFERENCES [PaginasAlbum] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Conversas] (
    [Id] uniqueidentifier NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [TrocaId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Conversas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Conversas_Trocas_TrocaId] FOREIGN KEY ([TrocaId]) REFERENCES [Trocas] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [FigurinhasUsuario] (
    [Id] uniqueidentifier NOT NULL,
    [PossuiNoAlbum] bit NOT NULL,
    [QuantidadeRepetida] int NOT NULL,
    [DisponivelTroca] bit NOT NULL,
    [UsuarioId] uniqueidentifier NOT NULL,
    [FigurinhaId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_FigurinhasUsuario] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FigurinhasUsuario_Figurinhas_FigurinhaId] FOREIGN KEY ([FigurinhaId]) REFERENCES [Figurinhas] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FigurinhasUsuario_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SlotsPaginaAlbum] (
    [Id] uniqueidentifier NOT NULL,
    [PosicaoX] decimal(10,4) NOT NULL,
    [PosicaoY] decimal(10,4) NOT NULL,
    [Largura] decimal(10,4) NOT NULL,
    [Altura] decimal(10,4) NOT NULL,
    [Ordem] int NOT NULL,
    [PaginaAlbumId] uniqueidentifier NOT NULL,
    [FigurinhaId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_SlotsPaginaAlbum] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SlotsPaginaAlbum_Figurinhas_FigurinhaId] FOREIGN KEY ([FigurinhaId]) REFERENCES [Figurinhas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SlotsPaginaAlbum_PaginasAlbum_PaginaAlbumId] FOREIGN KEY ([PaginaAlbumId]) REFERENCES [PaginasAlbum] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TrocaItens] (
    [Id] uniqueidentifier NOT NULL,
    [Quantidade] int NOT NULL,
    [TrocaId] uniqueidentifier NOT NULL,
    [FigurinhaId] uniqueidentifier NOT NULL,
    [UsuarioId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_TrocaItens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TrocaItens_Figurinhas_FigurinhaId] FOREIGN KEY ([FigurinhaId]) REFERENCES [Figurinhas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TrocaItens_Trocas_TrocaId] FOREIGN KEY ([TrocaId]) REFERENCES [Trocas] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TrocaItens_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Mensagens] (
    [Id] uniqueidentifier NOT NULL,
    [Conteudo] varchar(100) NOT NULL,
    [Lida] bit NOT NULL,
    [Editada] bit NOT NULL,
    [Excluida] bit NOT NULL,
    [DataEnvio] datetime2 NOT NULL,
    [UsuarioId] uniqueidentifier NOT NULL,
    [ConversaId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Mensagens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Mensagens_Conversas_ConversaId] FOREIGN KEY ([ConversaId]) REFERENCES [Conversas] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Mensagens_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE NO ACTION
);
GO

CREATE UNIQUE INDEX [IX_Albuns_Nome_Ano] ON [Albuns] ([Nome], [Ano]);
GO

CREATE UNIQUE INDEX [IX_Conversas_TrocaId] ON [Conversas] ([TrocaId]);
GO

CREATE UNIQUE INDEX [IX_Figurinhas_AlbumId_Codigo] ON [Figurinhas] ([AlbumId], [Codigo]);
GO

CREATE UNIQUE INDEX [IX_Figurinhas_AlbumId_Numero] ON [Figurinhas] ([AlbumId], [Numero]);
GO

CREATE INDEX [IX_Figurinhas_PaginaAlbumId] ON [Figurinhas] ([PaginaAlbumId]);
GO

CREATE INDEX [IX_FigurinhasUsuario_FigurinhaId] ON [FigurinhasUsuario] ([FigurinhaId]);
GO

CREATE INDEX [IX_FigurinhasUsuario_UsuarioId] ON [FigurinhasUsuario] ([UsuarioId]);
GO

CREATE INDEX [IX_Matches_UsuarioDestinoId] ON [Matches] ([UsuarioDestinoId]);
GO

CREATE UNIQUE INDEX [IX_Matches_UsuarioOrigemId_UsuarioDestinoId] ON [Matches] ([UsuarioOrigemId], [UsuarioDestinoId]);
GO

CREATE INDEX [IX_Mensagens_ConversaId] ON [Mensagens] ([ConversaId]);
GO

CREATE INDEX [IX_Mensagens_DataEnvio] ON [Mensagens] ([DataEnvio]);
GO

CREATE INDEX [IX_Mensagens_UsuarioId] ON [Mensagens] ([UsuarioId]);
GO

CREATE INDEX [IX_PaginasAlbum_AlbumId] ON [PaginasAlbum] ([AlbumId]);
GO

CREATE UNIQUE INDEX [IX_PaginasAlbum_AlbumId_NumeroPagina] ON [PaginasAlbum] ([AlbumId], [NumeroPagina]);
GO

CREATE INDEX [IX_Selecoes_AlbumId] ON [Selecoes] ([AlbumId]);
GO

CREATE UNIQUE INDEX [IX_Selecoes_AlbumId_Codigo] ON [Selecoes] ([AlbumId], [Codigo]);
GO

CREATE UNIQUE INDEX [IX_Selecoes_AlbumId_Nome] ON [Selecoes] ([AlbumId], [Nome]);
GO

CREATE UNIQUE INDEX [IX_SlotsPaginaAlbum_FigurinhaId] ON [SlotsPaginaAlbum] ([FigurinhaId]);
GO

CREATE INDEX [IX_SlotsPaginaAlbum_PaginaAlbumId] ON [SlotsPaginaAlbum] ([PaginaAlbumId]);
GO

CREATE UNIQUE INDEX [IX_SlotsPaginaAlbum_PaginaAlbumId_Ordem] ON [SlotsPaginaAlbum] ([PaginaAlbumId], [Ordem]);
GO

CREATE INDEX [IX_TrocaItens_FigurinhaId] ON [TrocaItens] ([FigurinhaId]);
GO

CREATE INDEX [IX_TrocaItens_TrocaId] ON [TrocaItens] ([TrocaId]);
GO

CREATE UNIQUE INDEX [IX_TrocaItens_TrocaId_UsuarioId_FigurinhaId] ON [TrocaItens] ([TrocaId], [UsuarioId], [FigurinhaId]);
GO

CREATE INDEX [IX_TrocaItens_UsuarioId] ON [TrocaItens] ([UsuarioId]);
GO

CREATE INDEX [IX_Trocas_DestinatarioId] ON [Trocas] ([DestinatarioId]);
GO

CREATE INDEX [IX_Trocas_SolicitanteId] ON [Trocas] ([SolicitanteId]);
GO

CREATE INDEX [IX_Trocas_Status] ON [Trocas] ([Status]);
GO

CREATE INDEX [IX_UsuarioDetalhes_Cidade] ON [UsuarioDetalhes] ([Cidade]);
GO

CREATE INDEX [IX_UsuarioDetalhes_Estado] ON [UsuarioDetalhes] ([Estado]);
GO

CREATE UNIQUE INDEX [IX_UsuarioDetalhes_UsuarioId] ON [UsuarioDetalhes] ([UsuarioId]);
GO

CREATE UNIQUE INDEX [IX_Usuarios_Email] ON [Usuarios] ([Email]);
GO

CREATE UNIQUE INDEX [IX_Usuarios_Username] ON [Usuarios] ([Username]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260525183657_InitialCreate', N'8.0.27');
GO

COMMIT;
GO

