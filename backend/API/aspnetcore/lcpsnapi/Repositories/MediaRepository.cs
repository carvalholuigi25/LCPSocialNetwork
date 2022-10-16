using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class MediaRepository : IMedia
    {
        private readonly MyDBContext _dbContext;

        public MediaRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Media>? GetFiles()
        {
            try
            {
                return _dbContext.Media?.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Media GetFilesDetails(int id)
        {
            try
            {
                Media? Media = _dbContext.Media?.Find(id);
                if (Media != null)
                {
                    return Media;
                }
                else
                {
                    throw new ArgumentException("Cannot get the file");
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddFiles(Media media)
        {
            try
            {
                _dbContext.Media?.Add(media);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateFiles(Media media)
        {
            try
            {
                _dbContext.Entry(media).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Media DeleteFiles(int id)
        {
            try
            {
                Media? media = _dbContext.Media?.Find(id);

                if (media != null)
                {
                    _dbContext.Media?.Remove(media);
                    _dbContext.SaveChanges();
                    return media;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the file");
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckFiles(int id)
        {
            if (_dbContext.Media == null)
            {
                throw new ArgumentException("The files cannot be checked!");
            }

            return _dbContext.Media.Any(e => e.MediaId == id);
        }
    }
}
