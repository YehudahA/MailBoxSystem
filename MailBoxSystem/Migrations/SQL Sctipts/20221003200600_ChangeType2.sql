﻿BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ALTER COLUMN [TempToken] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221003200600_ChangeType2', N'6.0.9');
GO

COMMIT;
GO

