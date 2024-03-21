using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LCPSNLibrary.Resources;

namespace LCPSNWebApi.Classes;

public class User
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? UserId { get; set; }
    
    [DataType(DataType.Text)]
    [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(MyResources))] 
    public string Username { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(MyResources))]
    [StringLength(1000, MinimumLength = 1, ErrorMessageResourceName = "PasswordLengthReq", ErrorMessageResourceType = typeof(MyResources))] 
    public string Password { get; set; } = null!;

    [DataType(DataType.Text)] 
    [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(MyResources))]
    [EmailAddress(ErrorMessageResourceName = "EmailInvalid", ErrorMessageResourceType = typeof(MyResources))]
    public string Email { get; set; } = null!;
    
    [DataType(DataType.Text)] 
    [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(MyResources))]
    public string FirstName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Required(ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(MyResources))]
    public string LastName { get; set; } = null!;

    [DataType(DataType.DateTime)]
    [Required(ErrorMessageResourceName = "DateBirthdayRequired", ErrorMessageResourceType = typeof(MyResources))]
    public DateTime DateBirthday { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.Text)] public string? Role { get; set; } = UserRoles.User.ToString();
    [DataType(DataType.Text)] public string? Status { get; set; }
    [DataType(DataType.Text)] public string? Biography { get; set; }
    [DataType(DataType.Text)] public string? AvatarUrl { get; set; }
    [DataType(DataType.Text)] public string? CoverUrl { get; set; }
    [DataType(DataType.DateTime)] public DateTime? DateAccountCreated { get; set; } = DateTime.UtcNow;
    [DisplayName("CurrentToken")][DataType("text")] public string? CurrentToken { get; set; }
    [DisplayName("RefreshToken")][DataType("text")] public string? RefreshToken { get; set; }
    [DisplayName("RefreshTokenExpiryTime")][DataType(DataType.DateTime)] public DateTime? RefreshTokenExpiryTime { get; set; } = DateTime.UtcNow;
    public ICollection<Attachment>? Attachments { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<FriendRequest>? FriendRequests { get; set; }
    public ICollection<ChatMessage>? ChatMessages { get; set; }
    public ICollection<Notification>? Notifications { get; set; }
}

public class UserAuth
{
    [DataType(DataType.Text)]
    [Required(ErrorMessageResourceName = "UsernameRequired", ErrorMessageResourceType = typeof(MyResources))] 
    public string Username { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(MyResources))]
    [StringLength(1000, MinimumLength = 1, ErrorMessageResourceName = "PasswordLengthReq", ErrorMessageResourceType = typeof(MyResources))] 
    public string Password { get; set; } = null!;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRoles
{
    [Description("Guest")] Guest,
    [Description("User")] User,
    [Description("Moderator")] Moderator,
    [Description("Administrator")] Administrator
}