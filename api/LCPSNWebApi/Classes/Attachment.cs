using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LCPSNWebApi.Library.Resources;

namespace LCPSNWebApi.Classes;
public class Attachment
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? AttachmentId { get; set; }
    
    [Required(ErrorMessageResourceName = "TitleRequired", ErrorMessageResourceType = typeof(MyResources))] 
    [DataType(DataType.Text)] 
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessageResourceName = "DescRequired", ErrorMessageResourceType = typeof(MyResources))] 
    [DataType(DataType.Text)] 
    public string Description { get; set; } = null!;

    [Required][DataType(DataType.Text)] public string AttachmentUrl { get; set; } = null!;
    [DataType(DataType.Text)] public string? AttachmentType { get; set; }
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.DateTime)] public DateTime? DateAttachmentUploaded { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateAttachmentUpdated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateAttachmentDeleted { get; set; } = DateTime.UtcNow;
    public bool? IsFeatured { get; set; } = false;
    public int? PostId { get; set; } = 1;
    public int? CommentId { get; set; } = 1;
    public int? ReplyId { get; set; } = 1;
    public int? ReactionId { get; set; } = 1;
    public int? ShareId { get; set; } = 1;
    public int? UserId { get; set; } = 1;
}