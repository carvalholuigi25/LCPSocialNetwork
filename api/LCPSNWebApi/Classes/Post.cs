using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LCPSNWebApi.Library.Resources;

namespace LCPSNWebApi.Classes;
public class Post
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? PostId { get; set; }
    
    [Required(ErrorMessageResourceName = "TitleRequired", ErrorMessageResourceType = typeof(MyResources))] 
    [DataType(DataType.Text)] 
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessageResourceName = "DescRequired", ErrorMessageResourceType = typeof(MyResources))] 
    [DataType(DataType.Text)] 
    public string Description { get; set; } = null!;
    
    [DataType(DataType.Text)] public string? ImgUrl { get; set; } = "assets/images/bkg.jpeg";
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.DateTime)] public DateTime? DatePostCreated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DatePostUpdated { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DatePostDeleted { get; set; } = DateTime.UtcNow;
    public string? TypeTxtPost { get; set; } = TypeTxtPostEnum.html.ToString();
    public bool? IsFeatured { get; set; } = false;
    public int? UserId { get; set; } = 1;
    public int? CommentId { get; set; } = 1;
    public int? ReplyId { get; set; } = 1;
    public int? ShareId { get; set; } = 1;
    public int? ReactionId { get; set; } = 1;
    public int? AttachmentId { get; set; } = 1;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TypeTxtPostEnum {
    [Description("HTML")] html,
    [Description("Markdown")] markdown
}