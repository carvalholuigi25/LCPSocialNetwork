using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static lcpsnapi.Classes.Enums;

namespace lcpsnapi.Classes
{
    public class TVSeries
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? TVSerieId { get; set; }
        public string? Title { get; set; }
        public string? Desc { get; set; }
        public string? MainImage { get; set; }
        public string? Cover { get; set; }
        public List<GalleryTVSeries>? Gallery { get; set; }
        public List<ReviewTVSeries>? Reviews { get; set; }
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

    public class GalleryTVSeries 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? GalleryTVSerieId { get; set; }
        public string? Title { get; set; }
        public string? Date { get; set; }
        public string? Type { get; set; }
        public string? Src { get; set; }
        public int? TVSerieId { get; set; }
        public int? UserId { get; set; }
    }

    public class ReviewTVSeries 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ReviewTVSerieId { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Date { get; set; }
        public string? Src { get; set; }
        public double? Rating { get; set; }
        public int? TVSerieId { get; set; }
        public int? UserId { get; set; }
    }
}