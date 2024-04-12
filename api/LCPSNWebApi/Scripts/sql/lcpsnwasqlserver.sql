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

CREATE TABLE [FilesData] (
    [Id] int NOT NULL IDENTITY,
    [GId] uniqueidentifier NOT NULL,
    [ImageBytes] varbinary(max) NULL,
    [FileName] nvarchar(max) NOT NULL,
    [FileType] nvarchar(max) NOT NULL,
    [FileFullPath] nvarchar(max) NULL,
    [FileSize] bigint NOT NULL,
    CONSTRAINT [PK_FilesData] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Posts] (
    [PostId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ImgUrl] nvarchar(max) NULL,
    [Status] nvarchar(max) NULL,
    [DatePostCreated] datetime2 NULL,
    [DatePostUpdated] datetime2 NULL,
    [DatePostDeleted] datetime2 NULL,
    [TypeTxtPost] nvarchar(max) NULL,
    [IsFeatured] bit NULL,
    [UserId] int NULL,
    [CommentId] int NULL,
    [ReplyId] int NULL,
    [ShareId] int NULL,
    [ReactionId] int NULL,
    [AttachmentId] int NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([PostId])
);
GO

CREATE TABLE [Reactions] (
    [ReactionId] int NOT NULL IDENTITY,
    [ReactionType] int NULL,
    [DateReacted] datetime2 NULL,
    [ReactionCounter] int NULL,
    [AttachmentId] int NULL,
    [PostId] int NULL,
    [CommentId] int NULL,
    [ReplyId] int NULL,
    [UserId] int NULL,
    CONSTRAINT [PK_Reactions] PRIMARY KEY ([ReactionId])
);
GO

CREATE TABLE [Replies] (
    [ReplyId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ImgUrl] nvarchar(max) NULL,
    [Status] nvarchar(max) NULL,
    [DateReplyCreated] datetime2 NULL,
    [DateReplyUpdated] datetime2 NULL,
    [DateReplyDeleted] datetime2 NULL,
    [IsFeatured] bit NULL,
    [ReactionId] int NULL,
    [ShareId] int NULL,
    [UserId] int NULL,
    [CommentId] int NULL,
    [PostId] int NULL,
    [AttachmentId] int NULL,
    CONSTRAINT [PK_Replies] PRIMARY KEY ([ReplyId])
);
GO

CREATE TABLE [Shares] (
    [ShareId] int NOT NULL IDENTITY,
    [ShareCounter] int NULL,
    [DateShared] datetime2 NULL,
    [AttachmentId] int NULL,
    [PostId] int NULL,
    [CommentId] int NULL,
    [ReplyId] int NULL,
    [UserId] int NULL,
    CONSTRAINT [PK_Shares] PRIMARY KEY ([ShareId])
);
GO

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    [Password] nvarchar(1000) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [DateBirthday] datetime2 NOT NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [Role] nvarchar(max) NULL,
    [Status] nvarchar(max) NULL,
    [Biography] nvarchar(max) NULL,
    [AvatarUrl] nvarchar(max) NULL,
    [CoverUrl] nvarchar(max) NULL,
    [DateAccountCreated] datetime2 NULL,
    [CurrentToken] nvarchar(max) NULL,
    [RefreshToken] nvarchar(max) NULL,
    [RefreshTokenExpiryTime] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [Attachments] (
    [AttachmentId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [AttachmentUrl] nvarchar(max) NOT NULL,
    [AttachmentType] nvarchar(max) NULL,
    [Status] nvarchar(max) NULL,
    [DateAttachmentUploaded] datetime2 NULL,
    [DateAttachmentUpdated] datetime2 NULL,
    [DateAttachmentDeleted] datetime2 NULL,
    [IsFeatured] bit NULL,
    [PostId] int NULL,
    [CommentId] int NULL,
    [ReplyId] int NULL,
    [ReactionId] int NULL,
    [ShareId] int NULL,
    [UserId] int NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY ([AttachmentId]),
    CONSTRAINT [FK_Attachments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);
GO

CREATE TABLE [ChatMessages] (
    [ChatMessageId] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NULL,
    [IsRead] bit NULL,
    [TypeMsg] nvarchar(max) NULL,
    [DateChatMessageCreated] datetime2 NULL,
    [DateChatMessageReaded] datetime2 NULL,
    [DateChatMessageUpdated] datetime2 NULL,
    [DateChatMessageDeleted] datetime2 NULL,
    [ConnectionId] nvarchar(max) NULL,
    [CommentId] int NULL,
    [ReplyId] int NULL,
    [UserId] int NULL,
    [TargetUserId] int NULL,
    [ReactionId] int NULL,
    [ShareId] int NULL,
    [AttachmentId] int NULL,
    CONSTRAINT [PK_ChatMessages] PRIMARY KEY ([ChatMessageId]),
    CONSTRAINT [FK_ChatMessages_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);
GO

CREATE TABLE [Comments] (
    [CommentId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ImgUrl] nvarchar(max) NULL,
    [Status] nvarchar(max) NULL,
    [DateCommentCreated] datetime2 NULL,
    [DateCommentUpdated] datetime2 NULL,
    [DateCommentDeleted] datetime2 NULL,
    [IsFeatured] bit NULL,
    [UserId] int NULL,
    [PostId] int NULL,
    [ReplyId] int NULL,
    [ShareId] int NULL,
    [ReactionId] int NULL,
    [AttachmentId] int NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([CommentId]),
    CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);
GO

CREATE TABLE [FriendRequests] (
    [FriendRequestId] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NULL,
    [FriendRequestType] int NULL,
    [IsAccepted] bit NULL,
    [DateFriendRequestCreated] datetime2 NULL,
    [DateFriendRequestAccepted] datetime2 NULL,
    [DateFriendRequestDeleted] datetime2 NULL,
    [UserId] int NULL,
    CONSTRAINT [PK_FriendRequests] PRIMARY KEY ([FriendRequestId]),
    CONSTRAINT [FK_FriendRequests_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);
GO

CREATE TABLE [Notifications] (
    [NotificationId] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NULL,
    [IsMarkRead] bit NULL,
    [IsPinned] bit NULL,
    [DateUserNotificationCreated] datetime2 NULL,
    [DateUserNotificationUpdated] datetime2 NULL,
    [DateUserNotificationDeleted] datetime2 NULL,
    [DateUserNotificationMarked] datetime2 NULL,
    [PostId] int NULL,
    [CommentId] int NULL,
    [ReplyId] int NULL,
    [AttachmentId] int NULL,
    [ReactionId] int NULL,
    [UserId] int NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY ([NotificationId]),
    CONSTRAINT [FK_Notifications_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'AvatarUrl', N'Biography', N'CoverUrl', N'CurrentToken', N'DateAccountCreated', N'DateBirthday', N'Email', N'FirstName', N'LastName', N'Password', N'PhoneNumber', N'RefreshToken', N'RefreshTokenExpiryTime', N'Role', N'Status', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([UserId], [AvatarUrl], [Biography], [CoverUrl], [CurrentToken], [DateAccountCreated], [DateBirthday], [Email], [FirstName], [LastName], [Password], [PhoneNumber], [RefreshToken], [RefreshTokenExpiryTime], [Role], [Status], [Username])
VALUES (1, N'images/users/avatars/luis.jpg', N'Hello, I''m Luis Carvalho.', N'images/users/covers/luis_cover.jpg', NULL, '2024-04-12T09:30:21.8367305Z', '1996-06-03T23:00:00.0000000Z', N'luiscarvalho239@gmail.com', N'Luis', N'Carvalho', N'$2a$12$TeO464DHlIzAMAYoG46QheFwIFv9fqOfMwaT7hjy2Cj/GdF6Vnq.2', N'123456789', NULL, '2024-04-12T09:30:21.8367313Z', N'Administrator', N'public', N'admin'),
(2, N'images/users/avatars/guest.png', N'Hello, I''m Guest.', N'images/users/covers/guest_cover.jpeg', NULL, '2024-04-12T09:30:22.1955914Z', '1996-06-03T23:00:00.0000000Z', N'guest@localhost.loc', N'Guest', N'Convidado', N'$2a$12$.bO/K3BzOgZ6kzlAPraA.uG.Mx1CPFBhdQxAueetmz8/7DmPIf5Su', N'123456789', NULL, '2024-04-12T09:30:22.1955921Z', N'Guest', N'public', N'guest');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'AvatarUrl', N'Biography', N'CoverUrl', N'CurrentToken', N'DateAccountCreated', N'DateBirthday', N'Email', N'FirstName', N'LastName', N'Password', N'PhoneNumber', N'RefreshToken', N'RefreshTokenExpiryTime', N'Role', N'Status', N'Username') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

CREATE INDEX [IX_Attachments_UserId] ON [Attachments] ([UserId]);
GO

CREATE INDEX [IX_ChatMessages_UserId] ON [ChatMessages] ([UserId]);
GO

CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
GO

CREATE INDEX [IX_FriendRequests_UserId] ON [FriendRequests] ([UserId]);
GO

CREATE INDEX [IX_Notifications_UserId] ON [Notifications] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240412093023_InitialCreateSQLServer', N'8.0.2');
GO

COMMIT;
GO

