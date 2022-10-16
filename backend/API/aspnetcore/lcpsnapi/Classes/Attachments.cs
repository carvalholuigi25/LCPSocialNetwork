using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static lcpsnapi.Classes.Enums;

namespace lcpsnapi.Classes
{
    public class Attachments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? AttachmentId { get; set; }
        public string? Title { get; set; }
        public string? Cover { get; set; }
        public string? Desc { get; set; }
        public Status? Status { get; set; }
        public Privacy? Privacy { get; set; }
        public bool? IsFeatured { get; set; }
        public string? DateCreated { get; set; }
        public string? DateModified { get; set; }
        public string? DateDeleted { get; set; }
        public int? UsersId { get; set; }
    }
}