using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LCPSNWebApi.Classes;
public class Attachment
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int AttachmentId { get; set; }
    [Required][DataType(DataType.Text)] public string Title { get; set; } = null!;
    [Required][DataType(DataType.Text)] public string AttachmentUrl { get; set; } = null!;
    [DataType(DataType.Text)] public string? AttachmentType { get; set; }
    [DataType(DataType.Text)] public string? Description { get; set; }
    [DataType(DataType.Text)] public string? Status { get; set; }
    [DataType(DataType.DateTime)] public DateTime? DateAttachmentUploaded { get; set; }
    public bool? IsFeatured { get; set; }
    public int? UserId { get; set; }
    public int? FriendId { get; set; }
}