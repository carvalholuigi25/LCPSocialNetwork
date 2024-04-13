CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Feedbacks` (
    `FeedbackId` int NOT NULL AUTO_INCREMENT,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `IsLocked` tinyint(1) NULL,
    `IsFeatured` tinyint(1) NULL,
    `TypeFeedback` longtext CHARACTER SET utf8mb4 NULL,
    `StatusFeedback` longtext CHARACTER SET utf8mb4 NULL,
    `DateFeedbackCreated` datetime(6) NULL,
    `DateFeedbackUpdated` datetime(6) NULL,
    `DateFeedbackDeleted` datetime(6) NULL,
    `Counter` int NULL,
    `UserId` int NULL,
    CONSTRAINT `PK_Feedbacks` PRIMARY KEY (`FeedbackId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `FilesData` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `GId` char(36) COLLATE ascii_general_ci NOT NULL,
    `ImageBytes` longblob NULL,
    `FileName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FileType` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FileFullPath` longtext CHARACTER SET utf8mb4 NULL,
    `FileSize` bigint NOT NULL,
    CONSTRAINT `PK_FilesData` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Posts` (
    `PostId` int NOT NULL AUTO_INCREMENT,
    `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ImgUrl` longtext CHARACTER SET utf8mb4 NULL,
    `Status` longtext CHARACTER SET utf8mb4 NULL,
    `DatePostCreated` datetime(6) NULL,
    `DatePostUpdated` datetime(6) NULL,
    `DatePostDeleted` datetime(6) NULL,
    `TypeTxtPost` longtext CHARACTER SET utf8mb4 NULL,
    `IsFeatured` tinyint(1) NULL,
    `UserId` int NULL,
    `CommentId` int NULL,
    `ReplyId` int NULL,
    `ShareId` int NULL,
    `ReactionId` int NULL,
    `AttachmentId` int NULL,
    CONSTRAINT `PK_Posts` PRIMARY KEY (`PostId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Reactions` (
    `ReactionId` int NOT NULL AUTO_INCREMENT,
    `ReactionType` int NULL,
    `DateReacted` datetime(6) NULL,
    `ReactionCounter` int NULL,
    `AttachmentId` int NULL,
    `PostId` int NULL,
    `CommentId` int NULL,
    `ReplyId` int NULL,
    `UserId` int NULL,
    CONSTRAINT `PK_Reactions` PRIMARY KEY (`ReactionId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Replies` (
    `ReplyId` int NOT NULL AUTO_INCREMENT,
    `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ImgUrl` longtext CHARACTER SET utf8mb4 NULL,
    `Status` longtext CHARACTER SET utf8mb4 NULL,
    `DateReplyCreated` datetime(6) NULL,
    `DateReplyUpdated` datetime(6) NULL,
    `DateReplyDeleted` datetime(6) NULL,
    `IsFeatured` tinyint(1) NULL,
    `ReactionId` int NULL,
    `ShareId` int NULL,
    `UserId` int NULL,
    `CommentId` int NULL,
    `PostId` int NULL,
    `AttachmentId` int NULL,
    CONSTRAINT `PK_Replies` PRIMARY KEY (`ReplyId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Shares` (
    `ShareId` int NOT NULL AUTO_INCREMENT,
    `ShareCounter` int NULL,
    `DateShared` datetime(6) NULL,
    `AttachmentId` int NULL,
    `PostId` int NULL,
    `CommentId` int NULL,
    `ReplyId` int NULL,
    `UserId` int NULL,
    CONSTRAINT `PK_Shares` PRIMARY KEY (`ShareId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Users` (
    `UserId` int NOT NULL AUTO_INCREMENT,
    `Username` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Password` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
    `FirstName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `DateBirthday` datetime(6) NOT NULL,
    `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
    `Role` longtext CHARACTER SET utf8mb4 NULL,
    `Status` longtext CHARACTER SET utf8mb4 NULL,
    `Biography` longtext CHARACTER SET utf8mb4 NULL,
    `AvatarUrl` longtext CHARACTER SET utf8mb4 NULL,
    `CoverUrl` longtext CHARACTER SET utf8mb4 NULL,
    `DateAccountCreated` datetime(6) NULL,
    `CurrentToken` longtext CHARACTER SET utf8mb4 NULL,
    `RefreshToken` longtext CHARACTER SET utf8mb4 NULL,
    `RefreshTokenExpiryTime` datetime(6) NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`UserId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Attachments` (
    `AttachmentId` int NOT NULL AUTO_INCREMENT,
    `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `AttachmentUrl` longtext CHARACTER SET utf8mb4 NOT NULL,
    `AttachmentType` longtext CHARACTER SET utf8mb4 NULL,
    `Status` longtext CHARACTER SET utf8mb4 NULL,
    `DateAttachmentUploaded` datetime(6) NULL,
    `DateAttachmentUpdated` datetime(6) NULL,
    `DateAttachmentDeleted` datetime(6) NULL,
    `IsFeatured` tinyint(1) NULL,
    `PostId` int NULL,
    `CommentId` int NULL,
    `ReplyId` int NULL,
    `ReactionId` int NULL,
    `ShareId` int NULL,
    `UserId` int NULL,
    CONSTRAINT `PK_Attachments` PRIMARY KEY (`AttachmentId`),
    CONSTRAINT `FK_Attachments_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ChatMessages` (
    `ChatMessageId` int NOT NULL AUTO_INCREMENT,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Status` longtext CHARACTER SET utf8mb4 NULL,
    `IsRead` tinyint(1) NULL,
    `TypeMsg` longtext CHARACTER SET utf8mb4 NULL,
    `DateChatMessageCreated` datetime(6) NULL,
    `DateChatMessageReaded` datetime(6) NULL,
    `DateChatMessageUpdated` datetime(6) NULL,
    `DateChatMessageDeleted` datetime(6) NULL,
    `ConnectionId` longtext CHARACTER SET utf8mb4 NULL,
    `CommentId` int NULL,
    `ReplyId` int NULL,
    `UserId` int NULL,
    `TargetUserId` int NULL,
    `ReactionId` int NULL,
    `ShareId` int NULL,
    `AttachmentId` int NULL,
    CONSTRAINT `PK_ChatMessages` PRIMARY KEY (`ChatMessageId`),
    CONSTRAINT `FK_ChatMessages_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Comments` (
    `CommentId` int NOT NULL AUTO_INCREMENT,
    `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ImgUrl` longtext CHARACTER SET utf8mb4 NULL,
    `Status` longtext CHARACTER SET utf8mb4 NULL,
    `DateCommentCreated` datetime(6) NULL,
    `DateCommentUpdated` datetime(6) NULL,
    `DateCommentDeleted` datetime(6) NULL,
    `IsFeatured` tinyint(1) NULL,
    `UserId` int NULL,
    `PostId` int NULL,
    `ReplyId` int NULL,
    `ShareId` int NULL,
    `ReactionId` int NULL,
    `AttachmentId` int NULL,
    CONSTRAINT `PK_Comments` PRIMARY KEY (`CommentId`),
    CONSTRAINT `FK_Comments_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `FriendRequests` (
    `FriendRequestId` int NOT NULL AUTO_INCREMENT,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Status` longtext CHARACTER SET utf8mb4 NULL,
    `FriendRequestType` int NULL,
    `IsAccepted` tinyint(1) NULL,
    `DateFriendRequestCreated` datetime(6) NULL,
    `DateFriendRequestAccepted` datetime(6) NULL,
    `DateFriendRequestDeleted` datetime(6) NULL,
    `UserId` int NULL,
    CONSTRAINT `PK_FriendRequests` PRIMARY KEY (`FriendRequestId`),
    CONSTRAINT `FK_FriendRequests_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Notifications` (
    `NotificationId` int NOT NULL AUTO_INCREMENT,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Status` longtext CHARACTER SET utf8mb4 NULL,
    `IsMarkRead` tinyint(1) NULL,
    `IsPinned` tinyint(1) NULL,
    `DateUserNotificationCreated` datetime(6) NULL,
    `DateUserNotificationUpdated` datetime(6) NULL,
    `DateUserNotificationDeleted` datetime(6) NULL,
    `DateUserNotificationMarked` datetime(6) NULL,
    `PostId` int NULL,
    `CommentId` int NULL,
    `ReplyId` int NULL,
    `AttachmentId` int NULL,
    `ReactionId` int NULL,
    `UserId` int NULL,
    CONSTRAINT `PK_Notifications` PRIMARY KEY (`NotificationId`),
    CONSTRAINT `FK_Notifications_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`)
) CHARACTER SET=utf8mb4;

INSERT INTO `Users` (`UserId`, `AvatarUrl`, `Biography`, `CoverUrl`, `CurrentToken`, `DateAccountCreated`, `DateBirthday`, `Email`, `FirstName`, `LastName`, `Password`, `PhoneNumber`, `RefreshToken`, `RefreshTokenExpiryTime`, `Role`, `Status`, `Username`)
VALUES (1, 'images/users/avatars/luis.jpg', 'Hello, I''m Luis Carvalho.', 'images/users/covers/luis_cover.jpg', NULL, TIMESTAMP '2024-04-13 15:47:45', TIMESTAMP '1996-06-03 23:00:00', 'luiscarvalho239@gmail.com', 'Luis', 'Carvalho', '$2a$12$dTc4mUGbHACChVxaMOYL2eQ6x4ZBBc1nEqKNNasF.pqpzE/SVlcH6', '123456789', NULL, TIMESTAMP '2024-04-13 15:47:45', 'Administrator', 'public', 'admin'),
(2, 'images/users/avatars/guest.png', 'Hello, I''m Guest.', 'images/users/covers/guest_cover.jpeg', NULL, TIMESTAMP '2024-04-13 15:47:46', TIMESTAMP '1996-06-03 23:00:00', 'guest@localhost.loc', 'Guest', 'Convidado', '$2a$12$dRBFURymepdJGFnfV/FDgePaSP7c2HiVJ84UZm.uGFOlFa3ZO2ZkC', '123456789', NULL, TIMESTAMP '2024-04-13 15:47:46', 'Guest', 'public', 'guest');

CREATE INDEX `IX_Attachments_UserId` ON `Attachments` (`UserId`);

CREATE INDEX `IX_ChatMessages_UserId` ON `ChatMessages` (`UserId`);

CREATE INDEX `IX_Comments_UserId` ON `Comments` (`UserId`);

CREATE INDEX `IX_FriendRequests_UserId` ON `FriendRequests` (`UserId`);

CREATE INDEX `IX_Notifications_UserId` ON `Notifications` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240413154747_InitialCreatePostgreSQL', '8.0.2');

COMMIT;

