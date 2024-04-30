using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LCPSNWebApi.Migrations.PostgreSQL
{
    /// <inheritdoc />
    public partial class InitialCreatePostgreSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: true),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: true),
                    TypeFeedback = table.Column<string>(type: "text", nullable: true),
                    StatusFeedback = table.Column<string>(type: "text", nullable: true),
                    DateFeedbackCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateFeedbackUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateFeedbackDeleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Counter = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                });

            migrationBuilder.CreateTable(
                name: "FilesData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageBytes = table.Column<byte[]>(type: "bytea", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false),
                    FileFullPath = table.Column<string>(type: "text", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImgUrl = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    DatePostCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DatePostUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DatePostDeleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TypeTxtPost = table.Column<string>(type: "text", nullable: true),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CommentId = table.Column<int>(type: "integer", nullable: true),
                    ReplyId = table.Column<int>(type: "integer", nullable: true),
                    ShareId = table.Column<int>(type: "integer", nullable: true),
                    ReactionId = table.Column<int>(type: "integer", nullable: true),
                    AttachmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    ReactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReactionType = table.Column<int>(type: "integer", nullable: true),
                    ReactionIcon = table.Column<string>(type: "text", nullable: true),
                    DateReacted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReactionCounter = table.Column<int>(type: "integer", nullable: true),
                    AttachmentId = table.Column<int>(type: "integer", nullable: true),
                    PostId = table.Column<int>(type: "integer", nullable: true),
                    CommentId = table.Column<int>(type: "integer", nullable: true),
                    ReplyId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.ReactionId);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    ReplyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImgUrl = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    DateReplyCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateReplyUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateReplyDeleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: true),
                    ReactionId = table.Column<int>(type: "integer", nullable: true),
                    ShareId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CommentId = table.Column<int>(type: "integer", nullable: true),
                    PostId = table.Column<int>(type: "integer", nullable: true),
                    AttachmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.ReplyId);
                });

            migrationBuilder.CreateTable(
                name: "Shares",
                columns: table => new
                {
                    ShareId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShareCounter = table.Column<int>(type: "integer", nullable: true),
                    DateShared = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AttachmentId = table.Column<int>(type: "integer", nullable: true),
                    PostId = table.Column<int>(type: "integer", nullable: true),
                    CommentId = table.Column<int>(type: "integer", nullable: true),
                    ReplyId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shares", x => x.ShareId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DateBirthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Biography = table.Column<string>(type: "text", nullable: true),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true),
                    CoverUrl = table.Column<string>(type: "text", nullable: true),
                    DateAccountCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CurrentToken = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AttachmentUrl = table.Column<string>(type: "text", nullable: false),
                    AttachmentType = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    DateAttachmentUploaded = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateAttachmentUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateAttachmentDeleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: true),
                    PostId = table.Column<int>(type: "integer", nullable: true),
                    CommentId = table.Column<int>(type: "integer", nullable: true),
                    ReplyId = table.Column<int>(type: "integer", nullable: true),
                    ReactionId = table.Column<int>(type: "integer", nullable: true),
                    ShareId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
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
                    ChatMessageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    IsRead = table.Column<bool>(type: "boolean", nullable: true),
                    TypeMsg = table.Column<string>(type: "text", nullable: true),
                    DateChatMessageCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateChatMessageReaded = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateChatMessageUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateChatMessageDeleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ConnectionId = table.Column<string>(type: "text", nullable: true),
                    CommentId = table.Column<int>(type: "integer", nullable: true),
                    ReplyId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    TargetUserId = table.Column<int>(type: "integer", nullable: true),
                    ReactionId = table.Column<int>(type: "integer", nullable: true),
                    ShareId = table.Column<int>(type: "integer", nullable: true),
                    AttachmentId = table.Column<int>(type: "integer", nullable: true)
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
                    CommentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImgUrl = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    DateCommentCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateCommentUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateCommentDeleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    PostId = table.Column<int>(type: "integer", nullable: true),
                    ReplyId = table.Column<int>(type: "integer", nullable: true),
                    ShareId = table.Column<int>(type: "integer", nullable: true),
                    ReactionId = table.Column<int>(type: "integer", nullable: true),
                    AttachmentId = table.Column<int>(type: "integer", nullable: true)
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
                    FriendRequestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    FriendRequestType = table.Column<int>(type: "integer", nullable: true),
                    IsAccepted = table.Column<bool>(type: "boolean", nullable: true),
                    DateFriendRequestCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateFriendRequestAccepted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateFriendRequestDeleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
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
                    NotificationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    IsMarkRead = table.Column<bool>(type: "boolean", nullable: true),
                    IsPinned = table.Column<bool>(type: "boolean", nullable: true),
                    DateUserNotificationCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateUserNotificationUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateUserNotificationDeleted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateUserNotificationMarked = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PostId = table.Column<int>(type: "integer", nullable: true),
                    CommentId = table.Column<int>(type: "integer", nullable: true),
                    ReplyId = table.Column<int>(type: "integer", nullable: true),
                    AttachmentId = table.Column<int>(type: "integer", nullable: true),
                    ReactionId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
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
                    { 1, "images/users/avatars/luis.jpg", "Hello, I'm Luis Carvalho.", "images/users/covers/luis_cover.jpg", null, new DateTime(2024, 4, 30, 17, 45, 2, 740, DateTimeKind.Utc).AddTicks(5335), new DateTime(1996, 6, 3, 23, 0, 0, 0, DateTimeKind.Utc), "luiscarvalho239@gmail.com", "Luis", "Carvalho", "$2a$12$4hARHhqRVIN4vMOuS9tEHujacy6CGqpHAyq/SJ/gmFPapIofGlYjm", "123456789", null, new DateTime(2024, 4, 30, 17, 45, 2, 740, DateTimeKind.Utc).AddTicks(5342), "Administrator", "public", "admin" },
                    { 2, "images/users/avatars/guest.png", "Hello, I'm Guest.", "images/users/covers/guest_cover.jpeg", null, new DateTime(2024, 4, 30, 17, 45, 3, 70, DateTimeKind.Utc).AddTicks(165), new DateTime(1996, 6, 3, 23, 0, 0, 0, DateTimeKind.Utc), "guest@localhost.loc", "Guest", "Convidado", "$2a$12$6lo7dxHdkKLBbaB.wfCxpeQ5Na7l4bxok8hSN1/9YX6u2aeF7QzQC", "123456789", null, new DateTime(2024, 4, 30, 17, 45, 3, 70, DateTimeKind.Utc).AddTicks(172), "Guest", "public", "guest" }
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
                name: "Feedbacks");

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
