namespace lcpsnapi.Classes
{
    public class Todo
    {
        public int? Id { get; set; } = 1;
        public string? Title { get; set; } = "Item 1";
        public string? Description { get; set; } = "Item1";
        public string? Image { get; set; } = "images/todo/item1.png";
        public bool? IsChecked { get; set; } = false;
        public DateTime? DateT { get; set; } = DateTime.UtcNow;
        public int? UserId { get; set; } = 1;
    }
}
