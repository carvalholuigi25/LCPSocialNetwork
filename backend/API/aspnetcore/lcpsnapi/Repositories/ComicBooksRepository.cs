using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using lcpsnapi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class ComicBooksRepository : IComicBooks
    {
        private readonly MyDBContext _dbContext;

        public ComicBooksRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ComicBooks>? GetComicBooks()
        {
            try
            {
                return _dbContext.ComicBook?.ToList();
            }
            catch
            {
                throw;
            }
        }

        public ComicBooks GetComicBooksDetails(int id)
        {
            try
            {
                ComicBooks? ComicBook = _dbContext.ComicBook?.Find(id);
                if (ComicBook != null)
                {
                    return ComicBook;
                }
                else
                {
                    throw new ArgumentException("Cannot get the comicbook");
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddComicBooks(ComicBooks ComicBook)
        {
            try
            {
                _dbContext.ComicBook?.Add(ComicBook);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateComicBooks(ComicBooks ComicBook)
        {
            try
            {
                _dbContext.Entry(ComicBook).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public ComicBooks DeleteComicBooks(int id)
        {
            try
            {
                ComicBooks? ComicBook = _dbContext.ComicBook?.Find(id);

                if (ComicBook != null)
                {
                    _dbContext.ComicBook?.Remove(ComicBook);
                    _dbContext.SaveChanges();
                    ResetAIById("ComicBook", 0);
                    return ComicBook;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the comicbook");
                }
            }
            catch
            {
                throw;
            }
        }

        public void ResetAIById(string? tblname = "ComicBook", int? id = 0) {
            try {
                _dbContext.Database.ExecuteSqlRaw($"DBCC CHECKIDENT('dbo.{tblname}', RESEED, {id})");
            } catch (Exception e) {
                throw new ArgumentException(e.Message);
            }
        }

        public bool CheckComicBooks(int id)
        {
            if (_dbContext.ComicBook == null)
            {
                throw new ArgumentException("The comicbooks cannot be checked!");
            }

            return _dbContext.ComicBook.Any(e => e.ComicBookId == id);
        }
    }
}
