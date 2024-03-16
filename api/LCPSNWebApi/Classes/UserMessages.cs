using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LCPSNWebApi.Classes;
public class UserMessage
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? UserMessageId { get; set; }
    [Required][DataType(DataType.Text)] public string Description { get; set; } = null!;
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.Text)] public bool? IsRead { get; set; } = false;
    [DataType(DataType.DateTime)] public DateTime? DateUserMessageCreated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateUserMessageReaded { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateUserMessageUpdated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateUserMessageDeleted { get; set; } = DateTime.UtcNow;
    [DataType(DataType.Text)] public int? UserId { get; set; } = 1;
}