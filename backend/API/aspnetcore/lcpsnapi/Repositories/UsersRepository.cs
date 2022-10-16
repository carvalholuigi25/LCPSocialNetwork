using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class UsersRepository : IUsers
    {
        private readonly MyDBContext _dbContext;

        public UsersRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Users>? GetUsers()
        {
            try
            {
                return _dbContext.User?.ToList();
            }
            catch
            {
                throw new Exception("Failed to retrieve users data");
            }
        }

        public Users GetUsersDetails(int id)
        {
            try
            {
                Users? user = _dbContext.User?.Find(id);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new ArgumentException("Cannot get the user");
                }
            }
            catch
            {
                throw new Exception("Failed to retrieve users data by id");
            }
        }

        public void AddUsers(Users user)
        {
            try
            {
                _dbContext.User?.Add(user);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw new Exception("Failed to add new users data");
            }
        }

        public void UpdateUsers(Users user)
        {
            try
            {
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw new Exception("Failed to update current users data");
            }
        }

        public Users DeleteUsers(int id)
        {
            try
            {
                Users? user = _dbContext.User?.Find(id);

                if (user != null)
                {
                    _dbContext.User?.Remove(user);
                    _dbContext.SaveChanges();
                    return user;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the user");
                }
            }
            catch
            {
                throw new Exception("Failed to delete current users data by id");
            }
        }

        public bool CheckUsers(int id)
        {
            if(_dbContext.User == null)
            {
                throw new ArgumentException("User has not been found...");
            }

            return _dbContext.User.Any(e => e.Id == id);
        }
    }
}
