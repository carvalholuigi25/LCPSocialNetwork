using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LCPSNWebApi.Classes;
public class Share
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? ShareId { get; set; }
    public int? ShareCounter { get; set; } = 0;
    [DataType(DataType.DateTime)] public DateTime? DateShared { get; set; } = DateTime.UtcNow;
    public int? AttachmentId { get; set; } = 1;
    public int? PostId { get; set; } = 1;
    public int? CommentId { get; set; } = 1;
    public int? ReplyId { get; set; } = 1;
    public int? UserId { get; set; } = 1;
}