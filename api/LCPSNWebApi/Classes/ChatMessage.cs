using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LCPSNWebApi.Library.Resources;

namespace LCPSNWebApi.Classes;
public class ChatMessage
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? ChatMessageId { get; set; }
    
    [Required(ErrorMessageResourceName = "DescRequired", ErrorMessageResourceType = typeof(MyResources))] 
    [DataType(DataType.Text)] 
    public string Description { get; set; } = null!;
    
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.Text)] public bool? IsRead { get; set; } = false;
    [DataType(DataType.Text)] public string? TypeMsg { get; set; } = TypeChatMsgEnum.sent.ToString();
    [DataType(DataType.DateTime)] public DateTime? DateChatMessageCreated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateChatMessageReaded { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateChatMessageUpdated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateChatMessageDeleted { get; set; } = DateTime.UtcNow;
    public string? ConnectionId { get; set; }
    public int? CommentId { get; set; } = 1;
    public int? ReplyId { get; set; } = 1;
    public int? UserId { get; set; } = 1;
    public int? TargetUserId { get; set; } = 1;
    public int? ReactionId { get; set; } = 1;
    public int? ShareId { get; set; } = 1;
    public int? AttachmentId { get; set; } = 1;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TypeChatMsgEnum {
    [Description("sent")] sent,
    [Description("received")] received
}