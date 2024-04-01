using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LCPSNWebApi.Library.Resources;

namespace LCPSNWebApi.Classes;
public class Notification 
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? NotificationId { get; set; }
    
    [Required(ErrorMessageResourceName = "DescRequired", ErrorMessageResourceType = typeof(MyResources))] 
    [DataType(DataType.Text)] 
    public string Description { get; set; } = null!;
    
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.Text)] public bool? IsMarkRead { get; set; } = false;
    [DataType(DataType.Text)] public bool? IsPinned { get; set; } = false;
    [DataType(DataType.DateTime)] public DateTime? DateUserNotificationCreated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateUserNotificationUpdated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateUserNotificationDeleted { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateUserNotificationMarked { get; set; } = DateTime.UtcNow;
    public int? PostId { get; set; } = 1;
    public int? CommentId { get; set; } = 1;
    public int? ReplyId { get; set; } = 1;
    public int? AttachmentId { get; set; } = 1;
    public int? ReactionId { get; set; } = 1;
    public int? UserId { get; set; } = 1;
}