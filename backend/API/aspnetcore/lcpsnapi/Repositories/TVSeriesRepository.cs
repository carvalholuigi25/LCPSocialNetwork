using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using lcpsnapi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class TVSeriesRepository : ITVSeries
    {
        private readonly MyDBContext _dbContext;

        public TVSeriesRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TVSeries>? GetTVSeries()
        {
            try
            {
                return _dbContext.TVSerie?.ToList();
            }
            catch
            {
                throw;
            }
        }

        public TVSeries GetTVSeriesDetails(int id)
        {
            try
            {
                TVSeries? TVSerie = _dbContext.TVSerie?.Find(id);
                if (TVSerie != null)
                {
                    return TVSerie;
                }
                else
                {
                    throw new ArgumentException("Cannot get the tvserie");
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddTVSeries(TVSeries TVSerie)
        {
            try
            {
                _dbContext.TVSerie?.Add(TVSerie);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateTVSeries(TVSeries TVSerie)
        {
            try
            {
                _dbContext.Entry(TVSerie).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public TVSeries DeleteTVSeries(int id)
        {
            try
            {
                TVSeries? TVSerie = _dbContext.TVSerie?.Find(id);

                if (TVSerie != null)
                {
                    _dbContext.TVSerie?.Remove(TVSerie);
                    _dbContext.SaveChanges();
                    ResetAIById("TVSerie", 0);
                    return TVSerie;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the tvserie");
                }
            }
            catch
            {
                throw;
            }
        }

        public void ResetAIById(string? tblname = "TVSerie", int? id = 0) {
            try {
                _dbContext.Database.ExecuteSqlRaw($"DBCC CHECKIDENT('dbo.{tblname}', RESEED, {id})");
            } catch (Exception e) {
                throw new ArgumentException(e.Message);
            }
        }

        public bool CheckTVSeries(int id)
        {
            if (_dbContext.TVSerie == null)
            {
                throw new ArgumentException("The tvseries cannot be checked!");
            }

            return _dbContext.TVSerie.Any(e => e.TVSerieId == id);
        }
    }
}
