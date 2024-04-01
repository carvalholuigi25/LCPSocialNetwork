using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LCPSNWebApi.Library.Resources;

namespace LCPSNWebApi.Classes;
public class FriendRequest
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? FriendRequestId { get; set; }
    
    [Required(ErrorMessageResourceName = "DescRequired", ErrorMessageResourceType = typeof(MyResources))] 
    [DataType(DataType.Text)] 
    public string Description { get; set; } = null!;
    
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.Text)] public FriendRequestTypeEnum? FriendRequestType { get; set; } = FriendRequestTypeEnum.unknown;
    [DataType(DataType.Text)] public bool? IsAccepted { get; set; } = false;
    [DataType(DataType.DateTime)] public DateTime? DateFriendRequestCreated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateFriendRequestAccepted { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DateFriendRequestDeleted { get; set; } = DateTime.UtcNow;
    public int? UserId { get; set; } = 1;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FriendRequestTypeEnum {
   [Description("friend")] friend,
   [Description("unknown")] unknown
}