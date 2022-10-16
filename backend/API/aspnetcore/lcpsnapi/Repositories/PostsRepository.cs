using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class PostsRepository : IPosts
    {
        private readonly MyDBContext _dbContext;

        public PostsRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Posts>? GetPosts()
        {
            try
            {
                return _dbContext.Post?.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Posts GetPostsDetails(int id)
        {
            try
            {
                Posts? Post = _dbContext.Post?.Find(id);
                if (Post != null)
                {
                    return Post;
                }
                else
                {
                    throw new ArgumentException("Cannot get the post");
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddPosts(Posts Post)
        {
            try
            {
                _dbContext.Post?.Add(Post);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePosts(Posts Post)
        {
            try
            {
                _dbContext.Entry(Post).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Posts DeletePosts(int id)
        {
            try
            {
                Posts? Post = _dbContext.Post?.Find(id);

                if (Post != null)
                {
                    _dbContext.Post?.Remove(Post);
                    _dbContext.SaveChanges();
                    return Post;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the post");
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckPosts(int id)
        {
            if (_dbContext.Post == null)
            {
                throw new ArgumentException("The posts cannot be checked!");
            }

            return _dbContext.Post.Any(e => e.PostId == id);
        }
    }
}
