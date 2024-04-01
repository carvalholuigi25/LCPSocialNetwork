using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LCPSNWebApi.Library.Resources;

namespace LCPSNWebApi.Classes;
public class Comment
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? CommentId { get; set; }

    [Required(ErrorMessageResourceName = "TitleRequired", ErrorMessageResourceType = typeof(MyResources))] 
    [DataType(DataType.Text)] 
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessageResourceName = "DescRequired", ErrorMessageResourceType = typeof(MyResources))] 
    [DataType(DataType.Text)] 
    public string Description { get; set; } = null!;
    
    [DataType(DataType.Text)] public string? ImgUrl { get; set; } = "assets/images/bkg.jpeg";
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.DateTime)] public DateTime? DateCommentCreated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateCommentUpdated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateCommentDeleted { get; set; } = DateTime.UtcNow;
    public bool? IsFeatured { get; set; } = false;
    public int? UserId { get; set; } = 1;
    public int? PostId { get; set; } = 1;
    public int? ReplyId { get; set; } = 1;
    public int? ShareId { get; set; } = 1;
    public int? ReactionId { get; set; } = 1;
    public int? AttachmentId { get; set; } = 1;
}