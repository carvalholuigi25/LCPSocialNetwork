using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static lcpsnapi.Classes.Enums;

namespace lcpsnapi.Classes
{
    public class Animes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? AnimeId { get; set; }
        public string? Title { get; set; }
        public string? Desc { get; set; }
        public string? MainImage { get; set; }
        public string? Cover { get; set; }
        public List<GalleryAnimes>? Gallery { get; set; }
        public List<ReviewAnimes>? Reviews { get; set; }
        public string? AuthorsInfo { get; set; }
        public string? CastInfo { get; set; }
        public string? Company { get; set; }
        public string? Publisher { get; set; }
        public string? Distributor { get; set; }
        public int? CertificationAge { get; set; }
        public int? TotalDuration { get; set; }
        public string? DateStart { get; set; }
        public string? DateEnd { get; set; }
        public double? Rating { get; set; }
        public bool? IsFeatured { get; set; }
        public string? DateCreated { get; set; }
        public int? UsersId { get; set; }
    }

    public class GalleryAnimes 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? GalleryAnimeId { get; set; }
        public string? Title { get; set; }
        public string? Date { get; set; }
        public string? Type { get; set; }
        public string? Src { get; set; }
        public int? AnimeId { get; set; }
        public int? UserId { get; set; }
    }

    public class ReviewAnimes 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ReviewAnimeId { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Date { get; set; }
        public string? Src { get; set; }
        public double? Rating { get; set; }
        public int? AnimeId { get; set; }
        public int? UserId { get; set; }
    }
}