namespace lcpsnapi.Classes
{
    public class Achievements
    {
        public int? Id { get; set; } = 1;
        public string? Name { get; set; } = "Web Api";
        public string? Description { get; set; } = "Create your WebAPI for your project";
        public string? Icon { get; set; } = "icons/ico_default.png";
        public int? ObjectiveCounter { get; set; } = 1;
        public EAchType? Type { get; set; } = EAchType.Counter;
        public EAchStatus? Status { get; set; } = EAchStatus.Locked;
        public DateTime? DateAchUnlocked { get; set; } = DateTime.UtcNow;
        public int? UserId { get; set; } = 1;

        public enum EAchType
        {
            Counter = 0
        }

        public enum EAchStatus
        {
            Locked = 0,
            Unlocked = 1,
            Secret = 2
        }
    }
}
