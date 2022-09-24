BEGIN TRANSACTION;
GO

ALTER TABLE [Packages] DROP COLUMN [SenderName];
GO

CREATE TABLE [PackageSenders] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [IconName] nvarchar(max) NULL,
    CONSTRAINT [PK_PackageSenders] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IconName', N'Name') AND [object_id] = OBJECT_ID(N'[PackageSenders]'))
    SET IDENTITY_INSERT [PackageSenders] ON;
INSERT INTO [PackageSenders] ([Id], [IconName], [Name])
VALUES (1, N'aliexpress.jpg', N'Ali Express');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IconName', N'Name') AND [object_id] = OBJECT_ID(N'[PackageSenders]'))
    SET IDENTITY_INSERT [PackageSenders] OFF;
GO

ALTER TABLE [Packages] ADD [SenderId] int NULL;
GO

UPDATE [Packages] SET [SenderId] = 1

GO

ALTER TABLE [Packages] ALTER COLUMN [SenderId] int NOT NULL;
GO

CREATE INDEX [IX_Packages_SenderId] ON [Packages] ([SenderId]);
GO

ALTER TABLE [Packages] ADD CONSTRAINT [FK_Packages_PackageSenders_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [PackageSenders] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220924195502_sender', N'6.0.9');
GO

COMMIT;
GO

