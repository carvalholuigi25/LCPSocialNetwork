using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LCPSNWebApi.Classes;
public class Post
{
    [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int? PostId { get; set; }
    [Required][DataType(DataType.Text)] public string Title { get; set; } = null!;
    [Required][DataType(DataType.Text)] public string Description { get; set; } = null!;
    [DataType(DataType.Text)] public string? ImgUrl { get; set; } = "assets/images/bkg.jpeg";
    [DataType(DataType.Text)] public string? Status { get; set; } = "public";
    [DataType(DataType.DateTime)] public DateTime? DatePostCreated { get; set; } = DateTime.UtcNow;
    public string? TypeTxtPost { get; set; } = TypeTxtPostEnum.html.ToString();
    public int? UserId { get; set; } = 1;
    public bool? IsFeatured { get; set; } = false;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TypeTxtPostEnum {
    [Description("HTML")] html,
    [Description("Markdown")] markdown
}