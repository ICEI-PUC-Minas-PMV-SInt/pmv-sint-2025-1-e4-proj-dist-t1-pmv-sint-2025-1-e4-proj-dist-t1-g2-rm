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
CREATE TABLE [Agendamentos] (
    [Id] int NOT NULL IDENTITY,
    [Data] datetime2 NOT NULL,
    [Hora] time NOT NULL,
    [PontuacaoTotal] int NOT NULL,
    CONSTRAINT [PK_Agendamentos] PRIMARY KEY ([Id])
);

CREATE TABLE [Produtos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Descricao] nvarchar(max) NOT NULL,
    [Pontuacao] int NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id])
);

CREATE TABLE [ItensColeta] (
    [Id] int NOT NULL IDENTITY,
    [ProdutoId] int NOT NULL,
    [Quantidade] int NOT NULL,
    [Estado] int NOT NULL,
    [AgendamentoId] int NOT NULL,
    CONSTRAINT [PK_ItensColeta] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItensColeta_Agendamentos_AgendamentoId] FOREIGN KEY ([AgendamentoId]) REFERENCES [Agendamentos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ItensColeta_Produtos_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produtos] ([Id]) ON DELETE NO ACTION
);

CREATE INDEX [IX_ItensColeta_AgendamentoId] ON [ItensColeta] ([AgendamentoId]);

CREATE INDEX [IX_ItensColeta_ProdutoId] ON [ItensColeta] ([ProdutoId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250321004127_InitialMigration', N'9.0.3');

CREATE TABLE [Noticias] (
    [Id] int NOT NULL IDENTITY,
    [Titulo] nvarchar(max) NULL,
    [Conteudo] nvarchar(max) NULL,
    [Autor] nvarchar(max) NULL,
    [DataPublicacao] datetime2 NOT NULL,
    [ImagemUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_Noticias] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250502154757_CreateNoticias', N'9.0.3');

CREATE TABLE [Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Estado] nvarchar(max) NOT NULL,
    [Cidade] nvarchar(max) NOT NULL,
    [Bairro] nvarchar(max) NOT NULL,
    [Rua] nvarchar(max) NOT NULL,
    [CEP] int NOT NULL,
    [Numero] int NOT NULL,
    [Complemento] nvarchar(max) NULL,
    [Username] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Tipo] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);

CREATE TABLE [Administradores] (
    [UsuarioId] int NOT NULL,
    [NomeAdmin] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Administradores] PRIMARY KEY ([UsuarioId]),
    CONSTRAINT [FK_Administradores_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Municipes] (
    [UsuarioId] int NOT NULL,
    [DataNascimento] date NOT NULL,
    [cpf] nvarchar(max) NULL,
    [Pontuacao] int NOT NULL,
    CONSTRAINT [PK_Municipes] PRIMARY KEY ([UsuarioId]),
    CONSTRAINT [FK_Municipes_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [OrgaosPublicos] (
    [UsuarioId] int NOT NULL,
    [Orgao] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_OrgaosPublicos] PRIMARY KEY ([UsuarioId]),
    CONSTRAINT [FK_OrgaosPublicos_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250510181634_M1usuarios', N'9.0.3');

EXEC sp_rename N'[OrgaosPublicos].[Orgao]', N'CNPJ', 'COLUMN';

EXEC sp_rename N'[Municipes].[cpf]', N'Cpf', 'COLUMN';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250515005348_M2orgaopublico', N'9.0.3');

CREATE TABLE [Beneficios] (
    [Id] int NOT NULL IDENTITY,
    [Titulo] nvarchar(max) NOT NULL,
    [Descricao] nvarchar(max) NOT NULL,
    [PontosNecessarios] int NOT NULL,
    CONSTRAINT [PK_Beneficios] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250523210449_Beneficios', N'9.0.3');

CREATE TABLE [FaleConosco] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Telefone] nvarchar(max) NULL,
    CONSTRAINT [PK_FaleConosco] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250620190938_CreateFaleConoscoCorrigida', N'9.0.3');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250620192125_CreateFaleConoscoFinal', N'9.0.3');

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FaleConosco]') AND [c].[name] = N'Nome');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [FaleConosco] DROP CONSTRAINT [' + @var + '];');
UPDATE [FaleConosco] SET [Nome] = N'' WHERE [Nome] IS NULL;
ALTER TABLE [FaleConosco] ALTER COLUMN [Nome] nvarchar(max) NOT NULL;
ALTER TABLE [FaleConosco] ADD DEFAULT N'' FOR [Nome];

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FaleConosco]') AND [c].[name] = N'Email');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [FaleConosco] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [FaleConosco] SET [Email] = N'' WHERE [Email] IS NULL;
ALTER TABLE [FaleConosco] ALTER COLUMN [Email] nvarchar(max) NOT NULL;
ALTER TABLE [FaleConosco] ADD DEFAULT N'' FOR [Email];

ALTER TABLE [FaleConosco] ADD [DataEnvio] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [FaleConosco] ADD [Mensagem] nvarchar(max) NOT NULL DEFAULT N'';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250620212053_AtualizaModelos', N'9.0.3');

COMMIT;
GO

