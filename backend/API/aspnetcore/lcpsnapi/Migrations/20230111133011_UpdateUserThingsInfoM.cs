using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lcpsnapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserThingsInfoM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "TVSerie",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Movie",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Manga",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Game",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "ComicBook",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Anime",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Pin" },
                values: new object[] { "$2a$11$5h59HPYYimrBHqify7OUv.sunTLyHvL.Nec.6ayJuNFnLUcNE5rqu", "$2a$11$jQrGRRJWu/fMhzmgTGyBoeS5DcL5YBiuxkQ7q3Fwxkc1TKGekvrWG" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Pin" },
                values: new object[] { "$2a$11$8hvYLSUzahAZCyXHzGv7CeIGdJ1djanynxmCiDBB8GzU25Z6gWuaG", "$2a$11$LcKHm9zdAxGXDbRKG6F06.bQ1rCIb5Xj7274JmksEqk.EnKlpfT7G" });

            migrationBuilder.UpdateData(
                table: "UserToken",
                keyColumn: "UsersTokenId",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateExp", "Password", "Pin", "Token" },
                values: new object[] { new DateTime(2023, 1, 11, 13, 30, 10, 710, DateTimeKind.Utc).AddTicks(1591), "11/02/2023 13:30:10", "$2a$11$FoPw54ytpTaeVboP1cCti.Rt9LxPBiLDK9Rw2p0LKrNdpMNsoKJN.", "$2a$11$y8PY1shvC6PJVKoCcPHujeoLsCx9REgvsxSLr2dGvAXz25KM4Bc5C", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJsb2NhbGhvc3QiLCJqdGkiOiJhYTNlZjVjZS00NmFmLTQ1NGEtYTE2ZC1iMTlkN2NhY2FlMWEiLCJpYXQiOiIxMS8wMS8yMDIzIDEzOjMwOjEwIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Imx1aWdpY2FyOTYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJzdXBlcmFkbWluIiwiZXhwIjoxNjc2MTIyMjEwLCJpc3MiOiJsb2NhbGhvc3QiLCJhdWQiOiJsb2NhbGhvc3QifQ.UIpddIh8-gd3k58cWsLfLAH__rEZXCeKzMb3V1BRWVQ" });

            migrationBuilder.UpdateData(
                table: "UserToken",
                keyColumn: "UsersTokenId",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateExp", "Password", "Pin", "Token" },
                values: new object[] { new DateTime(2023, 1, 11, 13, 30, 11, 51, DateTimeKind.Utc).AddTicks(7735), "11/02/2023 13:30:11", "$2a$11$Eia52k9RGydKyC2.cojdo.5z4pB6hwuwgJb3zKodYokhGMQjbYgam", "$2a$11$TKcd4e4xSKHsUSKRHmeKaO2qhwxA.fUpJ74l3ukBRC5b7J6MQM9lu", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJsb2NhbGhvc3QiLCJqdGkiOiJmMmUxMWRmNi0wZDUwLTQ4YzEtYTgwNy1hNGRmZGFiMjcwNTIiLCJpYXQiOiIxMS8wMS8yMDIzIDEzOjMwOjExIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Imd1ZXN0IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiZ3Vlc3QiLCJleHAiOjE2NzYxMjIyMTEsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6ImxvY2FsaG9zdCJ9.GoBtJ5NR4nPktXlhiUTWBlPDwnjAY3EwKqVLzDmvlyk" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "TVSerie");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "ComicBook");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Anime");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Pin" },
                values: new object[] { "$2a$11$7Ct14dw.JARdAsylPouL2emvuByd5D07cVLNVIeXl1Y4RaTcy7Qd.", "$2a$11$xjNAys54902WKzCr54DATuqIaBDyQT/ZlR5SnqXXWi0ms2FHQDOw2" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Pin" },
                values: new object[] { "$2a$11$gKMBubFrzLqMiWWaQnSLwu5HbQbpGcSVnmihegAVoaBRSQtFVXEf2", "$2a$11$p469JklXpZlqwQZvefCvv.zxwsIMHrEvSWTIvnijr6hRGYulmqSei" });

            migrationBuilder.UpdateData(
                table: "UserToken",
                keyColumn: "UsersTokenId",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateExp", "Password", "Pin", "Token" },
                values: new object[] { new DateTime(2023, 1, 10, 17, 38, 33, 785, DateTimeKind.Utc).AddTicks(5739), "10/02/2023 17:38:33", "$2a$11$zyIJFx65.nAhVVST6CTkGeisY1NCi2KzAE6oZ.wcjDJXLOKjQKWlm", "$2a$11$bpcYy3RFcwDK1xvWAdSe3eMPAEsEGqFrfaGWlxG0TMpxzezdq08Ia", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJsb2NhbGhvc3QiLCJqdGkiOiJiNzYyMGFhMS02NWYyLTRmOTQtYWU3My00YjFkYWExOWIwNWUiLCJpYXQiOiIxMC8wMS8yMDIzIDE3OjM4OjMzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Imx1aWdpY2FyOTYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJzdXBlcmFkbWluIiwiZXhwIjoxNjc2MDUwNzEzLCJpc3MiOiJsb2NhbGhvc3QiLCJhdWQiOiJsb2NhbGhvc3QifQ.BRgbWInoVw3bi3QNUCOcIZyl9P7ufjbDK5I-BbmOX1I" });

            migrationBuilder.UpdateData(
                table: "UserToken",
                keyColumn: "UsersTokenId",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateExp", "Password", "Pin", "Token" },
                values: new object[] { new DateTime(2023, 1, 10, 17, 38, 34, 123, DateTimeKind.Utc).AddTicks(5073), "10/02/2023 17:38:34", "$2a$11$zjULEeVMq.63QubXKN/1EOOKXpjqemDCrEs6XuqyT5bbARQGuVCL.", "$2a$11$9H2loqa26DUOAE66Ao4QA.DgBFYA0KAcDV7Z1rTgHgkvcflG9hYFq", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJsb2NhbGhvc3QiLCJqdGkiOiI1YjJkZDNmYS1lNzZlLTQzZmYtOWEwOC1jMGE1MDgxYWNiNWQiLCJpYXQiOiIxMC8wMS8yMDIzIDE3OjM4OjM0IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Imd1ZXN0IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiZ3Vlc3QiLCJleHAiOjE2NzYwNTA3MTQsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6ImxvY2FsaG9zdCJ9.eLwKP1Ml1ooBu2LwmjD77dAzTbhx06j3Gan_w9H52G0" });
        }
    }
}
