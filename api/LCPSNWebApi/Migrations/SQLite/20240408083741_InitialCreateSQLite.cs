using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LCPSNWebApi.Migrations.SQLite
{
    /// <inheritdoc />
    public partial class InitialCreateSQLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilesData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ImageBytes = table.Column<byte[]>(type: "BLOB", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FileType = table.Column<string>(type: "TEXT", nullable: false),
                    FileFullPath = table.Column<string>(type: "TEXT", nullable: true),
                    FileSize = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImgUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    DatePostCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DatePostUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DatePostDeleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TypeTxtPost = table.Column<string>(type: "TEXT", nullable: true),
                    IsFeatured = table.Column<bool>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommentId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReplyId = table.Column<int>(type: "INTEGER", nullable: true),
                    ShareId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReactionId = table.Column<int>(type: "INTEGER", nullable: true),
                    AttachmentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    ReactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReactionType = table.Column<int>(type: "INTEGER", nullable: true),
                    DateReacted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReactionCounter = table.Column<int>(type: "INTEGER", nullable: true),
                    AttachmentId = table.Column<int>(type: "INTEGER", nullable: true),
                    PostId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommentId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReplyId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.ReactionId);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    ReplyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImgUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    DateReplyCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateReplyUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateReplyDeleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsFeatured = table.Column<bool>(type: "INTEGER", nullable: true),
                    ReactionId = table.Column<int>(type: "INTEGER", nullable: true),
                    ShareId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommentId = table.Column<int>(type: "INTEGER", nullable: true),
                    PostId = table.Column<int>(type: "INTEGER", nullable: true),
                    AttachmentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.ReplyId);
                });

            migrationBuilder.CreateTable(
                name: "Shares",
                columns: table => new
                {
                    ShareId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShareCounter = table.Column<int>(type: "INTEGER", nullable: true),
                    DateShared = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AttachmentId = table.Column<int>(type: "INTEGER", nullable: true),
                    PostId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommentId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReplyId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shares", x => x.ShareId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    DateBirthday = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Role = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Biography = table.Column<string>(type: "TEXT", nullable: true),
                    AvatarUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CoverUrl = table.Column<string>(type: "TEXT", nullable: true),
                    DateAccountCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CurrentToken = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    AttachmentUrl = table.Column<string>(type: "TEXT", nullable: false),
                    AttachmentType = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    DateAttachmentUploaded = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateAttachmentUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateAttachmentDeleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsFeatured = table.Column<bool>(type: "INTEGER", nullable: true),
                    PostId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommentId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReplyId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReactionId = table.Column<int>(type: "INTEGER", nullable: true),
                    ShareId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_Attachments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    ChatMessageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: true),
                    DateChatMessageCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateChatMessageReaded = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateChatMessageUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateChatMessageDeleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CommentId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReplyId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    TargetUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReactionId = table.Column<int>(type: "INTEGER", nullable: true),
                    ShareId = table.Column<int>(type: "INTEGER", nullable: true),
                    AttachmentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.ChatMessageId);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImgUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    DateCommentCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateCommentUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateCommentDeleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsFeatured = table.Column<bool>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    PostId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReplyId = table.Column<int>(type: "INTEGER", nullable: true),
                    ShareId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReactionId = table.Column<int>(type: "INTEGER", nullable: true),
                    AttachmentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    FriendRequestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    FriendRequestType = table.Column<int>(type: "INTEGER", nullable: true),
                    IsAccepted = table.Column<bool>(type: "INTEGER", nullable: true),
                    DateFriendRequestCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateFriendRequestAccepted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateFriendRequestDeleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.FriendRequestId);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    IsMarkRead = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsPinned = table.Column<bool>(type: "INTEGER", nullable: true),
                    DateUserNotificationCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateUserNotificationUpdated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateUserNotificationDeleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateUserNotificationMarked = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PostId = table.Column<int>(type: "INTEGER", nullable: true),
                    CommentId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReplyId = table.Column<int>(type: "INTEGER", nullable: true),
                    AttachmentId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReactionId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AvatarUrl", "Biography", "CoverUrl", "CurrentToken", "DateAccountCreated", "DateBirthday", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "RefreshToken", "RefreshTokenExpiryTime", "Role", "Status", "Username" },
                values: new object[,]
                {
                    { 1, "images/users/avatars/luis.jpg", "Hello, I'm Luis Carvalho.", "images/users/covers/luis_cover.jpg", null, new DateTime(2024, 4, 8, 8, 37, 39, 895, DateTimeKind.Utc).AddTicks(4579), new DateTime(1996, 6, 3, 23, 0, 0, 0, DateTimeKind.Utc), "luiscarvalho239@gmail.com", "Luis", "Carvalho", "$2a$12$h38SKrMFQEXulhePXXefkO4/dxFkNy93ZTU4ppPdEao.DcKfeDCdG", "123456789", null, new DateTime(2024, 4, 8, 8, 37, 39, 895, DateTimeKind.Utc).AddTicks(4586), "Administrator", "public", "admin" },
                    { 2, "images/users/avatars/guest.png", "Hello, I'm Guest.", "images/users/covers/guest_cover.jpeg", null, new DateTime(2024, 4, 8, 8, 37, 40, 229, DateTimeKind.Utc).AddTicks(1429), new DateTime(1996, 6, 3, 23, 0, 0, 0, DateTimeKind.Utc), "guest@localhost.loc", "Guest", "Convidado", "$2a$12$SeN.xsyoc4gA00mbrhVyJugG5G.JoveSTl30Lz0U0/MgtPtspPLam", "123456789", null, new DateTime(2024, 4, 8, 8, 37, 40, 229, DateTimeKind.Utc).AddTicks(1435), "Guest", "public", "guest" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_UserId",
                table: "Attachments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_UserId",
                table: "ChatMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_UserId",
                table: "FriendRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FilesData");

            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "Shares");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
