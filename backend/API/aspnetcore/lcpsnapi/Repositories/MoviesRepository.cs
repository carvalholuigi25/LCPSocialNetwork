using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using lcpsnapi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class MoviesRepository : IMovies
    {
        private readonly MyDBContext _dbContext;

        public MoviesRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Movies>? GetMovies()
        {
            try
            {
                return _dbContext.Movie?.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Movies GetMoviesDetails(int id)
        {
            try
            {
                Movies? Movie = _dbContext.Movie?.Find(id);
                if (Movie != null)
                {
                    return Movie;
                }
                else
                {
                    throw new ArgumentException("Cannot get the movie");
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddMovies(Movies Movie)
        {
            try
            {
                _dbContext.Movie?.Add(Movie);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateMovies(Movies Movie)
        {
            try
            {
                _dbContext.Entry(Movie).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Movies DeleteMovies(int id)
        {
            try
            {
                Movies? Movie = _dbContext.Movie?.Find(id);

                if (Movie != null)
                {
                    _dbContext.Movie?.Remove(Movie);
                    _dbContext.SaveChanges();
                    ResetAIById("Movie", 0);
                    return Movie;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the movie");
                }
            }
            catch
            {
                throw;
            }
        }

        public void ResetAIById(string? tblname = "Movie", int? id = 0) {
            try {
                _dbContext.Database.ExecuteSqlRaw($"DBCC CHECKIDENT('dbo.{tblname}', RESEED, {id})");
            } catch (Exception e) {
                throw new ArgumentException(e.Message);
            }
        }

        public bool CheckMovies(int id)
        {
            if (_dbContext.Movie == null)
            {
                throw new ArgumentException("The movies cannot be checked!");
            }

            return _dbContext.Movie.Any(e => e.MovieId == id);
        }
    }
}
