using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface IAchievements
    {
        public List<Achievements>? GetAchievements();
        public Achievements GetAchievementsDetails(int id);
        public void AddAchievements(Achievements item);
        public void UpdateAchievements(Achievements item);
        public Achievements DeleteAchievements(int id);
        public bool CheckAchievementsItem(int id);
    }
}
