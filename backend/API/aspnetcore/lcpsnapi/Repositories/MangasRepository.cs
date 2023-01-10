using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using lcpsnapi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class MangasRepository : IMangas
    {
        private readonly MyDBContext _dbContext;

        public MangasRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Mangas>? GetMangas()
        {
            try
            {
                return _dbContext.Manga?.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Mangas GetMangasDetails(int id)
        {
            try
            {
                Mangas? Manga = _dbContext.Manga?.Find(id);
                if (Manga != null)
                {
                    return Manga;
                }
                else
                {
                    throw new ArgumentException("Cannot get the manga");
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddMangas(Mangas Manga)
        {
            try
            {
                _dbContext.Manga?.Add(Manga);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateMangas(Mangas Manga)
        {
            try
            {
                _dbContext.Entry(Manga).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Mangas DeleteMangas(int id)
        {
            try
            {
                Mangas? Manga = _dbContext.Manga?.Find(id);

                if (Manga != null)
                {
                    _dbContext.Manga?.Remove(Manga);
                    _dbContext.SaveChanges();
                    ResetAIById("Manga", 0);
                    return Manga;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the manga");
                }
            }
            catch
            {
                throw;
            }
        }

        public void ResetAIById(string? tblname = "Manga", int? id = 0) {
            try {
                _dbContext.Database.ExecuteSqlRaw($"DBCC CHECKIDENT('dbo.{tblname}', RESEED, {id})");
            } catch (Exception e) {
                throw new ArgumentException(e.Message);
            }
        }

        public bool CheckMangas(int id)
        {
            if (_dbContext.Manga == null)
            {
                throw new ArgumentException("The mangas cannot be checked!");
            }

            return _dbContext.Manga.Any(e => e.MangaId == id);
        }
    }
}
