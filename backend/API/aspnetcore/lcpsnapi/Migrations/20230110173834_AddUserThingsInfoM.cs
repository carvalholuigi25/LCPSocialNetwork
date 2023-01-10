using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lcpsnapi.Migrations
{
    /// <inheritdoc />
    public partial class AddUserThingsInfoM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anime",
                columns: table => new
                {
                    AnimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorsInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CastInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distributor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificationAge = table.Column<int>(type: "int", nullable: true),
                    TotalDuration = table.Column<int>(type: "int", nullable: true),
                    DateStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anime", x => x.AnimeId);
                });

            migrationBuilder.CreateTable(
                name: "ComicBook",
                columns: table => new
                {
                    ComicBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorsInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CastInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distributor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificationAge = table.Column<int>(type: "int", nullable: true),
                    TotalDuration = table.Column<int>(type: "int", nullable: true),
                    DateStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicBook", x => x.ComicBookId);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorsInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CastInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distributor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificationAge = table.Column<int>(type: "int", nullable: true),
                    TotalDuration = table.Column<int>(type: "int", nullable: true),
                    DateStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Manga",
                columns: table => new
                {
                    MangaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorsInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CastInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distributor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificationAge = table.Column<int>(type: "int", nullable: true),
                    TotalDuration = table.Column<int>(type: "int", nullable: true),
                    DateStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manga", x => x.MangaId);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorsInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CastInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distributor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificationAge = table.Column<int>(type: "int", nullable: true),
                    TotalDuration = table.Column<int>(type: "int", nullable: true),
                    DateStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "TVSerie",
                columns: table => new
                {
                    TVSerieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorsInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CastInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distributor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificationAge = table.Column<int>(type: "int", nullable: true),
                    TotalDuration = table.Column<int>(type: "int", nullable: true),
                    DateStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVSerie", x => x.TVSerieId);
                });

            migrationBuilder.CreateTable(
                name: "GalleryAnimes",
                columns: table => new
                {
                    GalleryAnimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimeId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AnimesAnimeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryAnimes", x => x.GalleryAnimeId);
                    table.ForeignKey(
                        name: "FK_GalleryAnimes_Anime_AnimesAnimeId",
                        column: x => x.AnimesAnimeId,
                        principalTable: "Anime",
                        principalColumn: "AnimeId");
                });

            migrationBuilder.CreateTable(
                name: "ReviewAnimes",
                columns: table => new
                {
                    ReviewAnimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    AnimeId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AnimesAnimeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewAnimes", x => x.ReviewAnimeId);
                    table.ForeignKey(
                        name: "FK_ReviewAnimes_Anime_AnimesAnimeId",
                        column: x => x.AnimesAnimeId,
                        principalTable: "Anime",
                        principalColumn: "AnimeId");
                });

            migrationBuilder.CreateTable(
                name: "GalleryComicBooks",
                columns: table => new
                {
                    GalleryComicBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComicBookId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ComicBooksComicBookId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryComicBooks", x => x.GalleryComicBookId);
                    table.ForeignKey(
                        name: "FK_GalleryComicBooks_ComicBook_ComicBooksComicBookId",
                        column: x => x.ComicBooksComicBookId,
                        principalTable: "ComicBook",
                        principalColumn: "ComicBookId");
                });

            migrationBuilder.CreateTable(
                name: "ReviewComicBooks",
                columns: table => new
                {
                    ReviewComicBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    ComicBookId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ComicBooksComicBookId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewComicBooks", x => x.ReviewComicBookId);
                    table.ForeignKey(
                        name: "FK_ReviewComicBooks_ComicBook_ComicBooksComicBookId",
                        column: x => x.ComicBooksComicBookId,
                        principalTable: "ComicBook",
                        principalColumn: "ComicBookId");
                });

            migrationBuilder.CreateTable(
                name: "GalleryGames",
                columns: table => new
                {
                    GalleryGameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    GamesGameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryGames", x => x.GalleryGameId);
                    table.ForeignKey(
                        name: "FK_GalleryGames_Game_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Game",
                        principalColumn: "GameId");
                });

            migrationBuilder.CreateTable(
                name: "ReviewGames",
                columns: table => new
                {
                    ReviewGameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    GamesGameId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewGames", x => x.ReviewGameId);
                    table.ForeignKey(
                        name: "FK_ReviewGames_Game_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Game",
                        principalColumn: "GameId");
                });

            migrationBuilder.CreateTable(
                name: "GalleryMangas",
                columns: table => new
                {
                    GalleryMangaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    MangasMangaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryMangas", x => x.GalleryMangaId);
                    table.ForeignKey(
                        name: "FK_GalleryMangas_Manga_MangasMangaId",
                        column: x => x.MangasMangaId,
                        principalTable: "Manga",
                        principalColumn: "MangaId");
                });

            migrationBuilder.CreateTable(
                name: "ReviewMangas",
                columns: table => new
                {
                    ReviewMangaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    MangaId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    MangasMangaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewMangas", x => x.ReviewMangaId);
                    table.ForeignKey(
                        name: "FK_ReviewMangas_Manga_MangasMangaId",
                        column: x => x.MangasMangaId,
                        principalTable: "Manga",
                        principalColumn: "MangaId");
                });

            migrationBuilder.CreateTable(
                name: "GalleryMovies",
                columns: table => new
                {
                    GalleryMovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    MoviesMovieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryMovies", x => x.GalleryMovieId);
                    table.ForeignKey(
                        name: "FK_GalleryMovies_Movie_MoviesMovieId",
                        column: x => x.MoviesMovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId");
                });

            migrationBuilder.CreateTable(
                name: "ReviewMovies",
                columns: table => new
                {
                    ReviewMovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    MoviesMovieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewMovies", x => x.ReviewMovieId);
                    table.ForeignKey(
                        name: "FK_ReviewMovies_Movie_MoviesMovieId",
                        column: x => x.MoviesMovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId");
                });

            migrationBuilder.CreateTable(
                name: "GalleryTVSeries",
                columns: table => new
                {
                    GalleryTVSerieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TVSerieId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TVSeriesTVSerieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryTVSeries", x => x.GalleryTVSerieId);
                    table.ForeignKey(
                        name: "FK_GalleryTVSeries_TVSerie_TVSeriesTVSerieId",
                        column: x => x.TVSeriesTVSerieId,
                        principalTable: "TVSerie",
                        principalColumn: "TVSerieId");
                });

            migrationBuilder.CreateTable(
                name: "ReviewTVSeries",
                columns: table => new
                {
                    ReviewTVSerieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    TVSerieId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TVSeriesTVSerieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewTVSeries", x => x.ReviewTVSerieId);
                    table.ForeignKey(
                        name: "FK_ReviewTVSeries_TVSerie_TVSeriesTVSerieId",
                        column: x => x.TVSeriesTVSerieId,
                        principalTable: "TVSerie",
                        principalColumn: "TVSerieId");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_GalleryAnimes_AnimesAnimeId",
                table: "GalleryAnimes",
                column: "AnimesAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryComicBooks_ComicBooksComicBookId",
                table: "GalleryComicBooks",
                column: "ComicBooksComicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryGames_GamesGameId",
                table: "GalleryGames",
                column: "GamesGameId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryMangas_MangasMangaId",
                table: "GalleryMangas",
                column: "MangasMangaId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryMovies_MoviesMovieId",
                table: "GalleryMovies",
                column: "MoviesMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryTVSeries_TVSeriesTVSerieId",
                table: "GalleryTVSeries",
                column: "TVSeriesTVSerieId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewAnimes_AnimesAnimeId",
                table: "ReviewAnimes",
                column: "AnimesAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComicBooks_ComicBooksComicBookId",
                table: "ReviewComicBooks",
                column: "ComicBooksComicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewGames_GamesGameId",
                table: "ReviewGames",
                column: "GamesGameId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewMangas_MangasMangaId",
                table: "ReviewMangas",
                column: "MangasMangaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewMovies_MoviesMovieId",
                table: "ReviewMovies",
                column: "MoviesMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewTVSeries_TVSeriesTVSerieId",
                table: "ReviewTVSeries",
                column: "TVSeriesTVSerieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryAnimes");

            migrationBuilder.DropTable(
                name: "GalleryComicBooks");

            migrationBuilder.DropTable(
                name: "GalleryGames");

            migrationBuilder.DropTable(
                name: "GalleryMangas");

            migrationBuilder.DropTable(
                name: "GalleryMovies");

            migrationBuilder.DropTable(
                name: "GalleryTVSeries");

            migrationBuilder.DropTable(
                name: "ReviewAnimes");

            migrationBuilder.DropTable(
                name: "ReviewComicBooks");

            migrationBuilder.DropTable(
                name: "ReviewGames");

            migrationBuilder.DropTable(
                name: "ReviewMangas");

            migrationBuilder.DropTable(
                name: "ReviewMovies");

            migrationBuilder.DropTable(
                name: "ReviewTVSeries");

            migrationBuilder.DropTable(
                name: "Anime");

            migrationBuilder.DropTable(
                name: "ComicBook");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Manga");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "TVSerie");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Pin" },
                values: new object[] { "$2a$11$CshvIuxyoupMpxbFS5QckecbULI1iETxEt86jCzb/5KTdesOfVHgK", "$2a$11$N2PIbdB0naM9hDZQgdwOvuoxlZ6XJoccG/tXF2UOlvh1c2mB9R8Xe" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Pin" },
                values: new object[] { "$2a$11$woMdHR0LZtlrPhA8EDNALOss8uBx2PvIvogfEYJZdGACtjBvlGYba", "$2a$11$A0cy/lwir7gTKTzdSumEbeaL.Y67UZgADKagaJ0UOvoLeKz0Z8y92" });

            migrationBuilder.UpdateData(
                table: "UserToken",
                keyColumn: "UsersTokenId",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateExp", "Password", "Pin", "Token" },
                values: new object[] { new DateTime(2023, 1, 2, 10, 17, 13, 74, DateTimeKind.Utc).AddTicks(669), "02/02/2023 10:17:13", "$2a$11$sdcqIJIZL5KY9t9zKvB1Lefx2NTBfxMcwmRSUfrsVtEorsOB9pnAy", "$2a$11$sRLrLVYyLn6VUkpKxBXgNObQi.zR41F7ptuDY3ybE3O4wRSnJqNbS", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJsb2NhbGhvc3QiLCJqdGkiOiJmNjgyZTA1ZC04NWQyLTQzNjAtYTM0OC0yZDIxMzg2ZDM2ZDAiLCJpYXQiOiIwMi8wMS8yMDIzIDEwOjE3OjEzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Imx1aWdpY2FyOTYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJzdXBlcmFkbWluIiwiZXhwIjoxNjc1MzMzMDMzLCJpc3MiOiJsb2NhbGhvc3QiLCJhdWQiOiJsb2NhbGhvc3QifQ.UNReiu7AgAIbVi18rlzVIbf3oVVnr7aGxbGJCf8j6cs" });

            migrationBuilder.UpdateData(
                table: "UserToken",
                keyColumn: "UsersTokenId",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateExp", "Password", "Pin", "Token" },
                values: new object[] { new DateTime(2023, 1, 2, 10, 17, 13, 476, DateTimeKind.Utc).AddTicks(6103), "02/02/2023 10:17:13", "$2a$11$FKw0zlz7WjRasGvNa98vD.f8IKyGxzTUFFy760IEha3IAnHB0jWzq", "$2a$11$gVgaMqk7r.xLeEoQYKrZReNQYZE6RmYvmQW.t1hbuExXAtzuZ6egS", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJsb2NhbGhvc3QiLCJqdGkiOiI0ZTY5NjBjYy03MmQ1LTQ0MTYtOWExMy1hY2ExYzBjMjgwZDIiLCJpYXQiOiIwMi8wMS8yMDIzIDEwOjE3OjEzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6Imd1ZXN0IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiZ3Vlc3QiLCJleHAiOjE2NzUzMzMwMzMsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6ImxvY2FsaG9zdCJ9.zYf2UZuet_kbvno9JBcVt7ExuCyIHsJ2ITo4gg7nB_E" });
        }
    }
}
