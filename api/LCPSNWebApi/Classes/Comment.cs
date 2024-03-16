using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LCPSNWebApi.Classes;
public class Comment
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? CommentId { get; set; }
    [Required][DataType(DataType.Text)] public string Title { get; set; } = null!;
    [Required][DataType(DataType.Text)] public string Description { get; set; } = null!;
    [DataType(DataType.Text)] public string? ImgUrl { get; set; } = "assets/images/bkg.jpeg";
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.DateTime)] public DateTime? DatePostCreated { get; set; } = DateTime.UtcNow;
    public bool? IsFeatured { get; set; } = false;
    public int? UserId { get; set; } = 1;
    public int? AttachmentId { get; set; } = 1;
}