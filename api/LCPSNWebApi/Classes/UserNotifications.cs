using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LCPSNWebApi.Classes;
public class UserNotification 
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? UserNotificationId { get; set; }
    [Required][DataType(DataType.Text)] public string Description { get; set; } = null!;
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.Text)] public bool? IsMarkRead { get; set; } = false;
    [DataType(DataType.Text)] public bool? IsPinned { get; set; } = false;
    [DataType(DataType.DateTime)] public DateTime? DateUserNotificationCreated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateUserNotificationUpdated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateUserNotificationDeleted { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateUserNotificationMarked { get; set; } = DateTime.UtcNow;
    [DataType(DataType.Text)] public int? UserId { get; set; } = 1;
}