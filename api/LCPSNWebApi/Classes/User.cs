using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LCPSNWebApi.Classes;

public class User
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int UserId { get; set; }
    [DataType(DataType.Text)][Required] public string Username { get; set; } = null!;
    [DataType(DataType.Text)][Required] public string Password { get; set; } = null!;
    [DataType(DataType.Text)] public string? Email { get; set; }
    [DataType(DataType.Text)] public string? Role { get; set; } = UserRoles.User.ToString();
    [DataType(DataType.Text)] public string? Status { get; set; }
    [DataType(DataType.Text)] public string? Biography { get; set; }
    [DataType(DataType.Text)] public string? AvatarUrl { get; set; }
    [DataType(DataType.Text)] public string? CoverUrl { get; set; }
    [DataType(DataType.DateTime)] public DateTime? DateAccountCreated { get; set; } = DateTime.UtcNow;
    [DisplayName("CurrentToken")][DataType("text")] public string? CurrentToken { get; set; }
    [DisplayName("RefreshToken")][DataType("text")] public string? RefreshToken { get; set; }
    [DisplayName("RefreshTokenExpiryTime")][DataType(DataType.DateTime)] public DateTime? RefreshTokenExpiryTime { get; set; } = DateTime.UtcNow;
    [JsonIgnore] public Friend? Friends { get; set; }
    [JsonIgnore] public Post? Posts { get; set; }
    [JsonIgnore] public Comment? Comments { get; set; }
}

public class UserAuth
{
    [Required] public string Username { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRoles
{
    [Description("Guest")] Guest,
    [Description("User")] User,
    [Description("Moderator")] Moderator,
    [Description("Administrator")] Administrator,
}