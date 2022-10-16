using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
        public class AchievementsRepository : IAchievements
        {
            private readonly MyDBContext _dbContext;

            public AchievementsRepository(MyDBContext dbContext)
            {
                _dbContext = dbContext;
            }

            public List<Achievements>? GetAchievements()
            {
                try
                {
                    return _dbContext.Achievements?.ToList();
                }
                catch
                {
                    throw;
                }
            }

            public Achievements GetAchievementsDetails(int id)
            {
                try
                {
                    Achievements? item = _dbContext.Achievements?.Find(id);
                    if (item != null)
                    {
                        return item;
                    }
                    else
                    {
                        throw new ArgumentException("Cannot get the achievements");
                    }
                }
                catch
                {
                    throw;
                }
            }

            public void AddAchievements(Achievements item)
            {
                try
                {
                    _dbContext.Achievements?.Add(item);
                    _dbContext.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }

            public void UpdateAchievements(Achievements item)
            {
                try
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }

            public Achievements DeleteAchievements(int id)
            {
                try
                {
                    Achievements? item = _dbContext.Achievements?.Find(id);

                    if (item != null)
                    {
                        _dbContext.Achievements?.Remove(item);
                        _dbContext.SaveChanges();
                        return item;
                    }
                    else
                    {
                        throw new ArgumentException("Cannot delete the achievement");
                    }
                }
                catch
                {
                    throw;
                }
            }

            public bool CheckAchievementsItem(int id)
            {
                if (_dbContext.Achievements == null)
                {
                    throw new ArgumentException("The achievements cannot be checked!");
                }

                return _dbContext.Achievements.Any(e => e.Id == id);
            }
        }
}
