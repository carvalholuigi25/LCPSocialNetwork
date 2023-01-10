using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using lcpsnapi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class GamesRepository : IGames
    {
        private readonly MyDBContext _dbContext;

        public GamesRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Games>? GetGames()
        {
            try
            {
                return _dbContext.Game?.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Games GetGamesDetails(int id)
        {
            try
            {
                Games? Game = _dbContext.Game?.Find(id);
                if (Game != null)
                {
                    return Game;
                }
                else
                {
                    throw new ArgumentException("Cannot get the game");
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddGames(Games Game)
        {
            try
            {
                _dbContext.Game?.Add(Game);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateGames(Games Game)
        {
            try
            {
                _dbContext.Entry(Game).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Games DeleteGames(int id)
        {
            try
            {
                Games? Game = _dbContext.Game?.Find(id);

                if (Game != null)
                {
                    _dbContext.Game?.Remove(Game);
                    _dbContext.SaveChanges();
                    ResetAIById("Game", 0);
                    return Game;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the game");
                }
            }
            catch
            {
                throw;
            }
        }

        public void ResetAIById(string? tblname = "Game", int? id = 0) {
            try {
                _dbContext.Database.ExecuteSqlRaw($"DBCC CHECKIDENT('dbo.{tblname}', RESEED, {id})");
            } catch (Exception e) {
                throw new ArgumentException(e.Message);
            }
        }

        public bool CheckGames(int id)
        {
            if (_dbContext.Game == null)
            {
                throw new ArgumentException("The games cannot be checked!");
            }

            return _dbContext.Game.Any(e => e.GameId == id);
        }
    }
}
