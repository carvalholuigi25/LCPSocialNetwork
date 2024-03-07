using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LCPSNWebApi.Classes;

public class Friend
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? FriendId { get; set; }
    [DataType(DataType.Text)][Required] public string Username { get; set; } = null!;
    [DataType(DataType.Text)][Required] public string Password { get; set; } = null!;
    [DataType(DataType.Text)] public string? Email { get; set; }
    [DataType(DataType.Text)] public string? Role { get; set; } = UserRoles.User.ToString();
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.Text)] public string? Biography { get; set; }
    [DataType(DataType.Text)] public string? AvatarUrl { get; set; }
    [DataType(DataType.Text)] public string? CoverUrl { get; set; }
    [DataType(DataType.DateTime)] public DateTime? DateAccountCreated { get; set; } = DateTime.UtcNow;
}