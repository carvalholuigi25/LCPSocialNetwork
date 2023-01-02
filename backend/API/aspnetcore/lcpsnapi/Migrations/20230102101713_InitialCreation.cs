using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace lcpsnapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjectiveCounter = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    DateAchUnlocked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Privacy = table.Column<int>(type: "int", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    DateUploaded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.MediaId);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shortdesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Privacy = table.Column<int>(type: "int", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true),
                    ReactsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                });

            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsChecked = table.Column<bool>(type: "bit", nullable: true),
                    DateT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UsersTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Displayname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateExp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.UsersTokenId);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Privacy = table.Column<int>(type: "int", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDeleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true),
                    PostsPostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_Attachment_Post_PostsPostId",
                        column: x => x.PostsPostId,
                        principalTable: "Post",
                        principalColumn: "PostId");
                });

            migrationBuilder.CreateTable(
                name: "Info",
                columns: table => new
                {
                    InfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalReactions = table.Column<int>(type: "int", nullable: true),
                    TotalComments = table.Column<int>(type: "int", nullable: true),
                    TotalReplies = table.Column<int>(type: "int", nullable: true),
                    TotalShares = table.Column<int>(type: "int", nullable: true),
                    LatestPostPostId = table.Column<int>(type: "int", nullable: true),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workorprofession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSchoolFinished = table.Column<bool>(type: "bit", nullable: true),
                    IsUniversityFinished = table.Column<bool>(type: "bit", nullable: true),
                    Privacy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info", x => x.InfoId);
                    table.ForeignKey(
                        name: "FK_Info_Post_LatestPostPostId",
                        column: x => x.LatestPostPostId,
                        principalTable: "Post",
                        principalColumn: "PostId");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Displayname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: true),
                    TypeFriend = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PrivacyStatus = table.Column<int>(type: "int", nullable: true),
                    DateBirthday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRegistered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfoId = table.Column<int>(type: "int", nullable: true),
                    UsersTokenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Info_InfoId",
                        column: x => x.InfoId,
                        principalTable: "Info",
                        principalColumn: "InfoId");
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    FriendsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: true),
                    TypeFriend = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PrivacyStatus = table.Column<int>(type: "int", nullable: true),
                    DateBirthday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRegistered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFriends = table.Column<int>(type: "int", nullable: true),
                    UsersTokenId = table.Column<int>(type: "int", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.FriendsId);
                    table.ForeignKey(
                        name: "FK_Friends_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Country", "Cover", "DateBirthday", "DateRegistered", "Displayname", "Email", "FirstName", "Image", "InfoId", "LastName", "Password", "Pin", "PrivacyStatus", "Role", "Status", "TypeFriend", "Username", "UsersTokenId" },
                values: new object[,]
                {
                    { 1, "Portugal", "/assets/images/users/c_luigi.png", "1996-06-04T00:00:00", "2022-08-19T10:30:00", "Luis Carvalho", "luiscarvalho239@gmail.com", "Luis", "/assets/images/users/luigi.png", null, "Carvalho", "$2a$11$CshvIuxyoupMpxbFS5QckecbULI1iETxEt86jCzb/5KTdesOfVHgK", "$2a$11$N2PIbdB0naM9hDZQgdwOvuoxlZ6XJoccG/tXF2UOlvh1c2mB9R8Xe", 0, 0, 1, 0, "luigicar96", null },
                    { 2, "Italy", "/assets/images/users/c_guest.png", "1995-05-03T00:00:00", "2022-08-31T15:50:00", "Guest Convidado", "guest@localhost.loc", "Guest", "/assets/images/users/guest.png", null, "Convidado", "$2a$11$woMdHR0LZtlrPhA8EDNALOss8uBx2PvIvogfEYJZdGACtjBvlGYba", "$2a$11$A0cy/lwir7gTKTzdSumEbeaL.Y67UZgADKagaJ0UOvoLeKz0Z8y92", 1, 5, 0, 0, "guest", null }
                });

            migrationBuilder.InsertData(
                table: "UserToken",
                columns: new[] { "UsersTokenId", "Cover", "DateCreated", "DateExp", "Displayname", "Email", "Image", "Password", "Pin", "Role", "Token", "Username", "UsersId" },
                values: new object[,]
                {
                    { 1, "/assets/images/users/c_luigi.png", new DateTime(2023, 1, 2, 10, 17, 13, 74, DateTimeKind.Utc).AddTicks(669), "02/02/2023 10:17:13", "Luis Carvalho", "luiscarvalho239@gmail.com", "/assets/images/users/luigi.png", "$2a$11$sdcqIJIZL5KY9t9zKvB1Lefx2NTBfxMcwmRSUfrsVtEorsOB9pnAy", "$2a$11$sRLrLVYyLn6VUkpKxBXgNObQi.zR41F7ptuDY3ybE3O4wRSnJqNbS", "superadmin", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJsb2NhbGhvc3QiLCJqdGkiOiJmNjgyZTA1ZC04NWQyLTQzNjAtYTM0OC0yZDIxMzg2ZDM2ZDAiLCJpYXQiOiIwMi8wMS8yMDIzIDEwOjE3OjEzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Imx1aWdpY2FyOTYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJzdXBlcmFkbWluIiwiZXhwIjoxNjc1MzMzMDMzLCJpc3MiOiJsb2NhbGhvc3QiLCJhdWQiOiJsb2NhbGhvc3QifQ.UNReiu7AgAIbVi18rlzVIbf3oVVnr7aGxbGJCf8j6cs", "luigicar96", 1 },
                    { 2, "/assets/images/users/c_guest.png", new DateTime(2023, 1, 2, 10, 17, 13, 476, DateTimeKind.Utc).AddTicks(6103), "02/02/2023 10:17:13", "Guest Convidado", "guest@localhost.loc", "/assets/images/users/guest.png", "$2a$11$FKw0zlz7WjRasGvNa98vD.f8IKyGxzTUFFy760IEha3IAnHB0jWzq", "$2a$11$gVgaMqk7r.xLeEoQYKrZReNQYZE6RmYvmQW.t1hbuExXAtzuZ6egS", "guest", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJsb2NhbGhvc3QiLCJqdGkiOiI0ZTY5NjBjYy03MmQ1LTQ0MTYtOWExMy1hY2ExYzBjMjgwZDIiLCJpYXQiOiIwMi8wMS8yMDIzIDEwOjE3OjEzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Imd1ZXN0IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiZ3Vlc3QiLCJleHAiOjE2NzUzMzMwMzMsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6ImxvY2FsaG9zdCJ9.zYf2UZuet_kbvno9JBcVt7ExuCyIHsJ2ITo4gg7nB_E", "guest", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_PostsPostId",
                table: "Attachment",
                column: "PostsPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UsersId",
                table: "Friends",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Info_LatestPostPostId",
                table: "Info",
                column: "LatestPostPostId");

            migrationBuilder.CreateIndex(
                name: "IX_User_InfoId",
                table: "User",
                column: "InfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Todo");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Info");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
