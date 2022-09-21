USE master
GO

CREATE DATABASE LastMile
GO

USE LastMile
GO

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

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [EMail] nvarchar(max) NULL,
    [PhoneNumber] int NOT NULL,
    [Password] nvarchar(max) NULL,
    [TempToken] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Boxes] (
    [Id] int NOT NULL IDENTITY,
    [LocalId] nvarchar(max) NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [Line1] nvarchar(max) NULL,
    [Line2] nvarchar(max) NULL,
    [OwnerId] int NULL,
    [Size] int NULL,
    CONSTRAINT [PK_Boxes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Boxes_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [LetterBoxStatuses] (
    [Id] int NOT NULL IDENTITY,
    [BoxId] int NOT NULL,
    [DeliverTime] datetime2 NOT NULL,
    [PullTime] datetime2 NULL,
    CONSTRAINT [PK_LetterBoxStatuses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LetterBoxStatuses_Boxes_BoxId] FOREIGN KEY ([BoxId]) REFERENCES [Boxes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Packages] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [SenderName] nvarchar(max) NULL,
    [RecieverName] nvarchar(max) NULL,
    [RecieverPhone] int NOT NULL,
    [BoxId] int NULL,
    [DeliverTime] datetime2 NULL,
    [PullTime] datetime2 NULL,
    CONSTRAINT [PK_Packages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Packages_Boxes_BoxId] FOREIGN KEY ([BoxId]) REFERENCES [Boxes] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Discriminator', N'LocalId', N'Size') AND [object_id] = OBJECT_ID(N'[Boxes]'))
    SET IDENTITY_INSERT [Boxes] ON;
INSERT INTO [Boxes] ([Id], [Discriminator], [LocalId], [Size])
VALUES (2, N'P', N'01', 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Discriminator', N'LocalId', N'Size') AND [object_id] = OBJECT_ID(N'[Boxes]'))
    SET IDENTITY_INSERT [Boxes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'EMail', N'Name', N'Password', N'PhoneNumber', N'TempToken') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [EMail], [Name], [Password], [PhoneNumber], [TempToken])
VALUES (1, N'ye.altman@gmail.com', N'Yehudah', N'1234', 535481815, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'EMail', N'Name', N'Password', N'PhoneNumber', N'TempToken') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Discriminator', N'Line1', N'Line2', N'LocalId', N'OwnerId') AND [object_id] = OBJECT_ID(N'[Boxes]'))
    SET IDENTITY_INSERT [Boxes] ON;
INSERT INTO [Boxes] ([Id], [Discriminator], [Line1], [Line2], [LocalId], [OwnerId])
VALUES (1, N'L', N'אלטמן', N'קומה 1', N'01', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Discriminator', N'Line1', N'Line2', N'LocalId', N'OwnerId') AND [object_id] = OBJECT_ID(N'[Boxes]'))
    SET IDENTITY_INSERT [Boxes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BoxId', N'Code', N'CreatedDate', N'DeliverTime', N'PullTime', N'RecieverName', N'RecieverPhone', N'SenderName') AND [object_id] = OBJECT_ID(N'[Packages]'))
    SET IDENTITY_INSERT [Packages] ON;
INSERT INTO [Packages] ([Id], [BoxId], [Code], [CreatedDate], [DeliverTime], [PullTime], [RecieverName], [RecieverPhone], [SenderName])
VALUES (1, 2, N'R1234', '2022-09-21T16:42:38.1292406+03:00', '2022-09-21T15:42:38.1292412+03:00', NULL, N'יהודה', 535481815, N'AliExpress');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BoxId', N'Code', N'CreatedDate', N'DeliverTime', N'PullTime', N'RecieverName', N'RecieverPhone', N'SenderName') AND [object_id] = OBJECT_ID(N'[Packages]'))
    SET IDENTITY_INSERT [Packages] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BoxId', N'DeliverTime', N'PullTime') AND [object_id] = OBJECT_ID(N'[LetterBoxStatuses]'))
    SET IDENTITY_INSERT [LetterBoxStatuses] ON;
INSERT INTO [LetterBoxStatuses] ([Id], [BoxId], [DeliverTime], [PullTime])
VALUES (1, 1, '2022-09-20T16:42:38.1292313+03:00', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BoxId', N'DeliverTime', N'PullTime') AND [object_id] = OBJECT_ID(N'[LetterBoxStatuses]'))
    SET IDENTITY_INSERT [LetterBoxStatuses] OFF;
GO

CREATE INDEX [IX_Boxes_OwnerId] ON [Boxes] ([OwnerId]);
GO

CREATE INDEX [IX_LetterBoxStatuses_BoxId] ON [LetterBoxStatuses] ([BoxId]);
GO

CREATE INDEX [IX_Packages_BoxId] ON [Packages] ([BoxId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220921134238_Initial', N'6.0.9');
GO

COMMIT;
GO

