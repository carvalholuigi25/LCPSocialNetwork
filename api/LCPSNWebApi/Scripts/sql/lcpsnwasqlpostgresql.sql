﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Feedbacks" (
    "FeedbackId" integer GENERATED BY DEFAULT AS IDENTITY,
    "Title" text,
    "Description" text,
    "IsLocked" boolean,
    "IsFeatured" boolean,
    "TypeFeedback" text,
    "StatusFeedback" text,
    "DateFeedbackCreated" timestamp without time zone,
    "DateFeedbackUpdated" timestamp without time zone,
    "DateFeedbackDeleted" timestamp without time zone,
    "Counter" integer,
    "UserId" integer,
    CONSTRAINT "PK_Feedbacks" PRIMARY KEY ("FeedbackId")
);

CREATE TABLE "FilesData" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "GId" uuid NOT NULL,
    "ImageBytes" bytea,
    "FileName" text NOT NULL,
    "FileType" text NOT NULL,
    "FileFullPath" text,
    "FileSize" bigint NOT NULL,
    CONSTRAINT "PK_FilesData" PRIMARY KEY ("Id")
);

CREATE TABLE "Posts" (
    "PostId" integer GENERATED BY DEFAULT AS IDENTITY,
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "ImgUrl" text,
    "Status" text,
    "DatePostCreated" timestamp without time zone,
    "DatePostUpdated" timestamp without time zone,
    "DatePostDeleted" timestamp without time zone,
    "TypeTxtPost" text,
    "IsFeatured" boolean,
    "UserId" integer,
    "CommentId" integer,
    "ReplyId" integer,
    "ShareId" integer,
    "ReactionId" integer,
    "AttachmentId" integer,
    CONSTRAINT "PK_Posts" PRIMARY KEY ("PostId")
);

CREATE TABLE "Reactions" (
    "ReactionId" integer GENERATED BY DEFAULT AS IDENTITY,
    "ReactionType" integer,
    "ReactionIcon" text,
    "DateReacted" timestamp without time zone,
    "ReactionCounter" integer,
    "AttachmentId" integer,
    "PostId" integer,
    "CommentId" integer,
    "ReplyId" integer,
    "UserId" integer,
    CONSTRAINT "PK_Reactions" PRIMARY KEY ("ReactionId")
);

CREATE TABLE "Replies" (
    "ReplyId" integer GENERATED BY DEFAULT AS IDENTITY,
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "ImgUrl" text,
    "Status" text,
    "DateReplyCreated" timestamp without time zone,
    "DateReplyUpdated" timestamp without time zone,
    "DateReplyDeleted" timestamp without time zone,
    "IsFeatured" boolean,
    "ReactionId" integer,
    "ShareId" integer,
    "UserId" integer,
    "CommentId" integer,
    "PostId" integer,
    "AttachmentId" integer,
    CONSTRAINT "PK_Replies" PRIMARY KEY ("ReplyId")
);

CREATE TABLE "Shares" (
    "ShareId" integer GENERATED BY DEFAULT AS IDENTITY,
    "ShareCounter" integer,
    "DateShared" timestamp without time zone,
    "AttachmentId" integer,
    "PostId" integer,
    "CommentId" integer,
    "ReplyId" integer,
    "UserId" integer,
    CONSTRAINT "PK_Shares" PRIMARY KEY ("ShareId")
);

CREATE TABLE "Users" (
    "UserId" integer GENERATED BY DEFAULT AS IDENTITY,
    "Username" text NOT NULL,
    "Password" character varying(1000) NOT NULL,
    "Email" text NOT NULL,
    "FirstName" text NOT NULL,
    "LastName" text NOT NULL,
    "DateBirthday" timestamp without time zone NOT NULL,
    "PhoneNumber" text,
    "Role" text,
    "Status" text,
    "Biography" text,
    "AvatarUrl" text,
    "CoverUrl" text,
    "DateAccountCreated" timestamp without time zone,
    "CurrentToken" text,
    "RefreshToken" text,
    "RefreshTokenExpiryTime" timestamp without time zone,
    CONSTRAINT "PK_Users" PRIMARY KEY ("UserId")
);

CREATE TABLE "Attachments" (
    "AttachmentId" integer GENERATED BY DEFAULT AS IDENTITY,
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "AttachmentUrl" text NOT NULL,
    "AttachmentType" text,
    "Status" text,
    "DateAttachmentUploaded" timestamp without time zone,
    "DateAttachmentUpdated" timestamp without time zone,
    "DateAttachmentDeleted" timestamp without time zone,
    "IsFeatured" boolean,
    "PostId" integer,
    "CommentId" integer,
    "ReplyId" integer,
    "ReactionId" integer,
    "ShareId" integer,
    "UserId" integer,
    CONSTRAINT "PK_Attachments" PRIMARY KEY ("AttachmentId"),
    CONSTRAINT "FK_Attachments_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId")
);

CREATE TABLE "ChatMessages" (
    "ChatMessageId" integer GENERATED BY DEFAULT AS IDENTITY,
    "Description" text NOT NULL,
    "Status" text,
    "IsRead" boolean,
    "TypeMsg" text,
    "DateChatMessageCreated" timestamp without time zone,
    "DateChatMessageReaded" timestamp without time zone,
    "DateChatMessageUpdated" timestamp without time zone,
    "DateChatMessageDeleted" timestamp without time zone,
    "ConnectionId" text,
    "CommentId" integer,
    "ReplyId" integer,
    "UserId" integer,
    "TargetUserId" integer,
    "ReactionId" integer,
    "ShareId" integer,
    "AttachmentId" integer,
    CONSTRAINT "PK_ChatMessages" PRIMARY KEY ("ChatMessageId"),
    CONSTRAINT "FK_ChatMessages_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId")
);

CREATE TABLE "Comments" (
    "CommentId" integer GENERATED BY DEFAULT AS IDENTITY,
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "ImgUrl" text,
    "Status" text,
    "DateCommentCreated" timestamp without time zone,
    "DateCommentUpdated" timestamp without time zone,
    "DateCommentDeleted" timestamp without time zone,
    "IsFeatured" boolean,
    "UserId" integer,
    "PostId" integer,
    "ReplyId" integer,
    "ShareId" integer,
    "ReactionId" integer,
    "AttachmentId" integer,
    CONSTRAINT "PK_Comments" PRIMARY KEY ("CommentId"),
    CONSTRAINT "FK_Comments_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId")
);

CREATE TABLE "FriendRequests" (
    "FriendRequestId" integer GENERATED BY DEFAULT AS IDENTITY,
    "Description" text NOT NULL,
    "Status" text,
    "FriendRequestType" integer,
    "IsAccepted" boolean,
    "DateFriendRequestCreated" timestamp without time zone,
    "DateFriendRequestAccepted" timestamp without time zone,
    "DateFriendRequestDeleted" timestamp without time zone,
    "UserId" integer,
    CONSTRAINT "PK_FriendRequests" PRIMARY KEY ("FriendRequestId"),
    CONSTRAINT "FK_FriendRequests_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId")
);

CREATE TABLE "Notifications" (
    "NotificationId" integer GENERATED BY DEFAULT AS IDENTITY,
    "Description" text NOT NULL,
    "Status" text,
    "IsMarkRead" boolean,
    "IsPinned" boolean,
    "DateUserNotificationCreated" timestamp without time zone,
    "DateUserNotificationUpdated" timestamp without time zone,
    "DateUserNotificationDeleted" timestamp without time zone,
    "DateUserNotificationMarked" timestamp without time zone,
    "PostId" integer,
    "CommentId" integer,
    "ReplyId" integer,
    "AttachmentId" integer,
    "ReactionId" integer,
    "UserId" integer,
    CONSTRAINT "PK_Notifications" PRIMARY KEY ("NotificationId"),
    CONSTRAINT "FK_Notifications_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("UserId")
);

INSERT INTO "Users" ("UserId", "AvatarUrl", "Biography", "CoverUrl", "CurrentToken", "DateAccountCreated", "DateBirthday", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Status", "Username")
VALUES (1, 'images/users/avatars/luis.jpg', 'Hello, I''m Luis Carvalho.', 'images/users/covers/luis_cover.jpg', NULL, TIMESTAMP '2024-04-30T17:45:02.740533', TIMESTAMP '1996-06-03T23:00:00', 'luiscarvalho239@gmail.com', 'Luis', 'Carvalho', '$2a$12$4hARHhqRVIN4vMOuS9tEHujacy6CGqpHAyq/SJ/gmFPapIofGlYjm', '123456789', NULL, TIMESTAMP '2024-04-30T17:45:02.740534', 'Administrator', 'public', 'admin');
INSERT INTO "Users" ("UserId", "AvatarUrl", "Biography", "CoverUrl", "CurrentToken", "DateAccountCreated", "DateBirthday", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Status", "Username")
VALUES (2, 'images/users/avatars/guest.png', 'Hello, I''m Guest.', 'images/users/covers/guest_cover.jpeg', NULL, TIMESTAMP '2024-04-30T17:45:03.070016', TIMESTAMP '1996-06-03T23:00:00', 'guest@localhost.loc', 'Guest', 'Convidado', '$2a$12$6lo7dxHdkKLBbaB.wfCxpeQ5Na7l4bxok8hSN1/9YX6u2aeF7QzQC', '123456789', NULL, TIMESTAMP '2024-04-30T17:45:03.070017', 'Guest', 'public', 'guest');

CREATE INDEX "IX_Attachments_UserId" ON "Attachments" ("UserId");

CREATE INDEX "IX_ChatMessages_UserId" ON "ChatMessages" ("UserId");

CREATE INDEX "IX_Comments_UserId" ON "Comments" ("UserId");

CREATE INDEX "IX_FriendRequests_UserId" ON "FriendRequests" ("UserId");

CREATE INDEX "IX_Notifications_UserId" ON "Notifications" ("UserId");

SELECT setval(
    pg_get_serial_sequence('"Users"', 'UserId'),
    GREATEST(
        (SELECT MAX("UserId") FROM "Users") + 1,
        nextval(pg_get_serial_sequence('"Users"', 'UserId'))),
    false);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240430174503_InitialCreatePostgreSQL', '8.0.2');

COMMIT;

