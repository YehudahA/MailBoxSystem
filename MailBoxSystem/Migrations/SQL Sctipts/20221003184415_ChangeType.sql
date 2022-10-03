BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Boxes].[LocalId]', N'LocalNumber', N'COLUMN';
GO

ALTER TABLE [Boxes] ALTER COLUMN [LocalNumber] int NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221003184415_ChangeType', N'6.0.9');
GO

COMMIT;
GO

