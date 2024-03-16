using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LCPSNWebApi.Classes;
public class UserFriendRequest
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? UserFriendRequestId { get; set; }
    [Required][DataType(DataType.Text)] public string? Description { get; set; }
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.Text)] public bool? IsAccepted { get; set; } = false;
    [DataType(DataType.DateTime)] public DateTime? DateUserFriendRequestCreated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateUserFriendRequestAccepted { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateUserFriendRequestDeleted { get; set; } = DateTime.UtcNow;
    [DataType(DataType.Text)] public int? UserId { get; set; } = 1;
}