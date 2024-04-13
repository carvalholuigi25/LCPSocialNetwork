using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LCPSNWebApi.Classes;
public class Feedback
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? FeedbackId { get; set; }
    public string? Title { get; set; } = "";
    public string? Description { get; set; } = "";
    public bool? IsLocked { get; set; } = false;
    public bool? IsFeatured { get; set; } = false;
    public string? TypeFeedback { get; set; } = FeedbackTypeEnum.pending.ToString();
    public string? StatusFeedback { get; set; } = FeedbackStatusEnum.publicT.ToString();
    [DataType(DataType.DateTime)] public DateTime? DateFeedbackCreated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateFeedbackUpdated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateFeedbackDeleted { get; set; } = DateTime.UtcNow;
    public int? Counter { get; set; } = 0;
    public int? UserId { get; set; } = 1;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FeedbackTypeEnum {
    [Description("pending")] pending,
    [Description("approved")] approved,
    [Description("rejected")] rejected,
    [Description("draft")] draft,
    [Description("deleted")] deleted
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FeedbackStatusEnum {
    [Description("public")] publicT,
    [Description("private")] privateT
}