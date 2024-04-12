CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "FilesData" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_FilesData" PRIMARY KEY AUTOINCREMENT,
    "GId" TEXT NOT NULL,
    "ImageBytes" BLOB NULL,
    "FileName" TEXT NOT NULL,
    "FileType" TEXT NOT NULL,
    "FileFullPath" TEXT NULL,
    "FileSize" INTEGER NOT NULL
);

CREATE TABLE "Posts" (
    "PostId" INTEGER NOT NULL CONSTRAINT "PK_Posts" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "ImgUrl" TEXT NULL,
    "Status" TEXT NULL,
    "DatePostCreated" TEXT NULL,
    "DatePostUpdated" TEXT NULL,
    "DatePostDeleted" TEXT NULL,
    "TypeTxtPost" TEXT NULL,
    "IsFeatured" INTEGER NULL,
    "UserId" INTEGER NULL,
    "CommentId" INTEGER NULL,
    "ReplyId" INTEGER NULL,
    "ShareId" INTEGER NULL,
    "ReactionId" INTEGER NULL,
    "AttachmentId" INTEGER NULL
);

CREATE TABLE "Reactions" (
    "ReactionId" INTEGER NOT NULL CONSTRAINT "PK_Reactions" PRIMARY KEY AUTOINCREMENT,
    "ReactionType" INTEGER NULL,
    "DateReacted" TEXT NULL,
    "ReactionCounter" INTEGER NULL,
    "AttachmentId" INTEGER NULL,
    "PostId" INTEGER NULL,
    "CommentId" INTEGER NULL,
    "ReplyId" INTEGER NULL,
    "UserId" INTEGER NULL
);

CREATE TABLE "Replies" (
    "ReplyId" INTEGER NOT NULL CONSTRAINT "PK_Replies" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "ImgUrl" TEXT NULL,
    "Status" TEXT NULL,
    "DateReplyCreated" TEXT NULL,
    "DateReplyUpdated" TEXT NULL,
    "DateReplyDeleted" TEXT NULL,
    "IsFeatured" INTEGER NULL,
    "ReactionId" INTEGER NULL,
    "ShareId" INTEGER NULL,
    "UserId" INTEGER NULL,
    "CommentId" INTEGER NULL,
    "PostId" INTEGER NULL,
    "AttachmentId" INTEGER NULL
);

CREATE TABLE "Shares" (
    "ShareId" INTEGER NOT NULL CONSTRAINT "PK_Shares" PRIMARY KEY AUTOINCREMENT,
    "ShareCounter" INTEGER NULL,
    "DateShared" TEXT NULL,
    "AttachmentId" INTEGER NULL,
    "PostId" INTEGER NULL,
    "CommentId" INTEGER NULL,
    "ReplyId" INTEGER NULL,
    "UserId" INTEGER NULL
);

CREATE TABLE "Users" (
    "UserId" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY AUTOINCREMENT,
    "Username" TEXT NOT NULL,
    "Password" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "FirstName" TEXT NOT NULL,
    "LastName" TEXT NOT NULL,
    "DateBirthday" TEXT NOT NULL,
    "PhoneNumber" TEXT NULL,
    "Role" TEXT NULL,
    "Status" TEXT NULL,
    "Biography" TEXT NULL,
    "AvatarUrl" TEXT NULL,
    "CoverUrl" TEXT NULL,
    "DateAccountCreated" TEXT NULL,
    "CurrentToken" TEXT NULL,
    "RefreshToken" TEXT NULL,
    "RefreshTokenExpiryTime" TEXT NULL
);

CREATE TABLE "Attachments" (
    "AttachmentId" INTEGER NOT NULL CONSTRAINT "PK_Attachments" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "AttachmentUrl" TEXT NOT NULL,
    "AttachmentType" TEXT NULL,
    "Status" TEXT NULL,
    "DateAttachmentUploaded" TEXT NULL,
    "DateAttachmentUpdated" TEXT NULL,
    "DateAttachmentDeleted" TEXT NULL,
    "IsFeatured" INTEGER NULL,
    "PostId" INTEGER NULL,
    "CommentId" INTEGER NULL,
    "ReplyId" INTEGER NULL,
    "ReactionId" INTEGER NULL,
    "ShareId" INTEGER NULL,
    "UserId" INTEGER NULL,
    CONSTRAINT "FK_Attachments_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId")
);

CREATE TABLE "ChatMessages" (
    "ChatMessageId" INTEGER NOT NULL CONSTRAINT "PK_ChatMessages" PRIMARY KEY AUTOINCREMENT,
    "Description" TEXT NOT NULL,
    "Status" TEXT NULL,
    "IsRead" INTEGER NULL,
    "TypeMsg" TEXT NULL,
    "DateChatMessageCreated" TEXT NULL,
    "DateChatMessageReaded" TEXT NULL,
    "DateChatMessageUpdated" TEXT NULL,
    "DateChatMessageDeleted" TEXT NULL,
    "ConnectionId" TEXT NULL,
    "CommentId" INTEGER NULL,
    "ReplyId" INTEGER NULL,
    "UserId" INTEGER NULL,
    "TargetUserId" INTEGER NULL,
    "ReactionId" INTEGER NULL,
    "ShareId" INTEGER NULL,
    "AttachmentId" INTEGER NULL,
    CONSTRAINT "FK_ChatMessages_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId")
);

CREATE TABLE "Comments" (
    "CommentId" INTEGER NOT NULL CONSTRAINT "PK_Comments" PRIMARY KEY AUTOINCREMENT,
    "Title" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "ImgUrl" TEXT NULL,
    "Status" TEXT NULL,
    "DateCommentCreated" TEXT NULL,
    "DateCommentUpdated" TEXT NULL,
    "DateCommentDeleted" TEXT NULL,
    "IsFeatured" INTEGER NULL,
    "UserId" INTEGER NULL,
    "PostId" INTEGER NULL,
    "ReplyId" INTEGER NULL,
    "ShareId" INTEGER NULL,
    "ReactionId" INTEGER NULL,
    "AttachmentId" INTEGER NULL,
    CONSTRAINT "FK_Comments_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId")
);

CREATE TABLE "FriendRequests" (
    "FriendRequestId" INTEGER NOT NULL CONSTRAINT "PK_FriendRequests" PRIMARY KEY AUTOINCREMENT,
    "Description" TEXT NOT NULL,
    "Status" TEXT NULL,
    "FriendRequestType" INTEGER NULL,
    "IsAccepted" INTEGER NULL,
    "DateFriendRequestCreated" TEXT NULL,
    "DateFriendRequestAccepted" TEXT NULL,
    "DateFriendRequestDeleted" TEXT NULL,
    "UserId" INTEGER NULL,
    CONSTRAINT "FK_FriendRequests_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId")
);

CREATE TABLE "Notifications" (
    "NotificationId" INTEGER NOT NULL CONSTRAINT "PK_Notifications" PRIMARY KEY AUTOINCREMENT,
    "Description" TEXT NOT NULL,
    "Status" TEXT NULL,
    "IsMarkRead" INTEGER NULL,
    "IsPinned" INTEGER NULL,
    "DateUserNotificationCreated" TEXT NULL,
    "DateUserNotificationUpdated" TEXT NULL,
    "DateUserNotificationDeleted" TEXT NULL,
    "DateUserNotificationMarked" TEXT NULL,
    "PostId" INTEGER NULL,
    "CommentId" INTEGER NULL,
    "ReplyId" INTEGER NULL,
    "AttachmentId" INTEGER NULL,
    "ReactionId" INTEGER NULL,
    "UserId" INTEGER NULL,
    CONSTRAINT "FK_Notifications_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId")
);

INSERT INTO "Users" ("UserId", "AvatarUrl", "Biography", "CoverUrl", "CurrentToken", "DateAccountCreated", "DateBirthday", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Status", "Username")
VALUES (1, 'images/users/avatars/luis.jpg', 'Hello, I''m Luis Carvalho.', 'images/users/covers/luis_cover.jpg', NULL, '2024-04-12 09:28:55.6095278', '1996-06-03 23:00:00', 'luiscarvalho239@gmail.com', 'Luis', 'Carvalho', '$2a$12$fUHwnjc5mcDf8ihJoUlhtuhC1t.VV7xcBCj9LVtKUt1KSH.6y3pla', '123456789', NULL, '2024-04-12 09:28:55.6095291', 'Administrator', 'public', 'admin');
SELECT changes();

INSERT INTO "Users" ("UserId", "AvatarUrl", "Biography", "CoverUrl", "CurrentToken", "DateAccountCreated", "DateBirthday", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Status", "Username")
VALUES (2, 'images/users/avatars/guest.png', 'Hello, I''m Guest.', 'images/users/covers/guest_cover.jpeg', NULL, '2024-04-12 09:28:56.071918', '1996-06-03 23:00:00', 'guest@localhost.loc', 'Guest', 'Convidado', '$2a$12$ZvHyZW60rm3eyqKrVlKBfO9jg5jH/rRLPazK/7QuGSS1kYcgUs2My', '123456789', NULL, '2024-04-12 09:28:56.0719191', 'Guest', 'public', 'guest');
SELECT changes();


CREATE INDEX "IX_Attachments_UserId" ON "Attachments" ("UserId");

CREATE INDEX "IX_ChatMessages_UserId" ON "ChatMessages" ("UserId");

CREATE INDEX "IX_Comments_UserId" ON "Comments" ("UserId");

CREATE INDEX "IX_FriendRequests_UserId" ON "FriendRequests" ("UserId");

CREATE INDEX "IX_Notifications_UserId" ON "Notifications" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240412092857_InitialCreateSQLite', '8.0.2');

COMMIT;

