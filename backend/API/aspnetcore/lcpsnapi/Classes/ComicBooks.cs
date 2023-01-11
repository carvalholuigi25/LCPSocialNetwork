using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static lcpsnapi.Classes.Enums;

namespace lcpsnapi.Classes
{
    public class ComicBooks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ComicBookId { get; set; }
        public string? Title { get; set; }
        public string? Desc { get; set; }
        public string? MainImage { get; set; }
        public string? Cover { get; set; }
        public List<GalleryComicBooks>? Gallery { get; set; }
        public List<ReviewComicBooks>? Reviews { get; set; }
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
        public bool? IsFavorite { get; set; }
        public string? DateCreated { get; set; }
        public int? UsersId { get; set; }
    }

    public class GalleryComicBooks 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? GalleryComicBookId { get; set; }
        public string? Title { get; set; }
        public string? Date { get; set; }
        public string? Type { get; set; }
        public string? Src { get; set; }
        public int? ComicBookId { get; set; }
        public int? UserId { get; set; }
    }

    public class ReviewComicBooks 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ReviewComicBookId { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Date { get; set; }
        public string? Src { get; set; }
        public double? Rating { get; set; }
        public int? ComicBookId { get; set; }
        public int? UserId { get; set; }
    }
}