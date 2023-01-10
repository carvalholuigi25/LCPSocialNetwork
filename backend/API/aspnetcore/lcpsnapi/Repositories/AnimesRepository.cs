using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using lcpsnapi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class AnimesRepository : IAnimes
    {
        private readonly MyDBContext _dbContext;

        public AnimesRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Animes>? GetAnimes()
        {
            try
            {
                return _dbContext.Anime?.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Animes GetAnimesDetails(int id)
        {
            try
            {
                Animes? Anime = _dbContext.Anime?.Find(id);
                if (Anime != null)
                {
                    return Anime;
                }
                else
                {
                    throw new ArgumentException("Cannot get the anime");
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddAnimes(Animes Anime)
        {
            try
            {
                _dbContext.Anime?.Add(Anime);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateAnimes(Animes Anime)
        {
            try
            {
                _dbContext.Entry(Anime).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Animes DeleteAnimes(int id)
        {
            try
            {
                Animes? Anime = _dbContext.Anime?.Find(id);

                if (Anime != null)
                {
                    _dbContext.Anime?.Remove(Anime);
                    _dbContext.SaveChanges();
                    ResetAIById("Anime", 0);
                    return Anime;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the anime");
                }
            }
            catch
            {
                throw;
            }
        }

        public void ResetAIById(string? tblname = "Anime", int? id = 0) {
            try {
                _dbContext.Database.ExecuteSqlRaw($"DBCC CHECKIDENT('dbo.{tblname}', RESEED, {id})");
            } catch (Exception e) {
                throw new ArgumentException(e.Message);
            }
        }

        public bool CheckAnimes(int id)
        {
            if (_dbContext.Anime == null)
            {
                throw new ArgumentException("The animes cannot be checked!");
            }

            return _dbContext.Anime.Any(e => e.AnimeId == id);
        }
    }
}
