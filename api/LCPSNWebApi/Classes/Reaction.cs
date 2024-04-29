using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LCPSNWebApi.Classes;
public class Reaction
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? ReactionId { get; set; }
    public ReactionTypeEnum? ReactionType { get; set; } = ReactionTypeEnum.like;
    [DataType(DataType.DateTime)] public DateTime? DateReacted { get; set; } = DateTime.UtcNow;
    public int? ReactionCounter { get; set; } = 0;
    public int? AttachmentId { get; set; } = 1;
    public int? PostId { get; set; } = 1;
    public int? CommentId { get; set; } = 1;
    public int? ReplyId { get; set; } = 1;
    public int? UserId { get; set; } = 1;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReactionTypeEnum {
   [Description("like")] like,
   [Description("dislike")] dislike,
   [Description("love")] love,
   [Description("courage")] courage,
   [Description("laugh")] laugh,
   [Description("surprised")] surprised,
   [Description("sad")] sad,
   [Description("angry")] angry,
   [Description("custom")] custom
}