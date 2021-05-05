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

CREATE TABLE [User] (
    [id] int NOT NULL IDENTITY,
    [name] nvarchar(100) NOT NULL,
    [email] varchar(100) NOT NULL,
    [password] varchar(100) NOT NULL,
    [isAdmin] bit NULL DEFAULT (((1))),
    [isActive] bit NULL DEFAULT (((1))),
    [createdAt] datetime NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Message] (
    [id] int NOT NULL IDENTITY,
    [from] int NOT NULL,
    [to] int NOT NULL,
    [content] nvarchar(1000) NOT NULL,
    [sentAt] datetime NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY ([id]),
    CONSTRAINT [FK__Message__from__2D27B809] FOREIGN KEY ([from]) REFERENCES [User] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK__Message__to__2E1BDC42] FOREIGN KEY ([to]) REFERENCES [User] ([id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Report] (
    [id] int NOT NULL IDENTITY,
    [reporter] int NOT NULL,
    [suspect] int NOT NULL,
    [reason] nvarchar(200) NOT NULL,
    [note] nvarchar(1000) NULL,
    [status] bit NOT NULL,
    [createdAt] datetime NULL,
    CONSTRAINT [PK_Report] PRIMARY KEY ([id]),
    CONSTRAINT [FK__Report__reporter__30F848ED] FOREIGN KEY ([reporter]) REFERENCES [User] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK__Report__suspect__31EC6D26] FOREIGN KEY ([suspect]) REFERENCES [User] ([id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [User_Favs] (
    [user] int NOT NULL,
    [favourite] int NOT NULL,
    CONSTRAINT [PK__User_Fav__A323CB9348DF7B6E] PRIMARY KEY ([user], [favourite]),
    CONSTRAINT [FK__User_Favs__favou__2A4B4B5E] FOREIGN KEY ([favourite]) REFERENCES [User] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK__User_Favs__user__29572725] FOREIGN KEY ([user]) REFERENCES [User] ([id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Message_from] ON [Message] ([from]);
GO

CREATE INDEX [IX_Message_to] ON [Message] ([to]);
GO

CREATE INDEX [IX_Report_reporter] ON [Report] ([reporter]);
GO

CREATE INDEX [IX_Report_suspect] ON [Report] ([suspect]);
GO

CREATE UNIQUE INDEX [UQ__User__AB6E6164410B6ED7] ON [User] ([email]);
GO

CREATE INDEX [IX_User_Favs_favourite] ON [User_Favs] ([favourite]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210308033440_Talktif', N'5.0.3');
GO

COMMIT;
GO

