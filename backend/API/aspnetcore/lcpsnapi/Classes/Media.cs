using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static lcpsnapi.Classes.Enums;

namespace lcpsnapi.Classes
{
    public class Media
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? MediaId { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? Type { get; set; }
        public string? Category { get; set; }
        public string? Cover { get; set; }
        public string? Desc { get; set; }
        public Status? Status { get; set; }
        public Privacy? Privacy { get; set; }
        public bool? IsFeatured { get; set; }
        public string? DateUploaded { get; set; }
        public string? DateModified { get; set; }
        public string? DateDeleted { get; set; }
        public int? UsersId { get; set; }
    }
}