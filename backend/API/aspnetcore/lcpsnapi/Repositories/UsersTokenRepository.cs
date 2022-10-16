using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class UsersTokenRepository : IUsersToken
    {
        private readonly MyDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public UsersTokenRepository(MyDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public List<UsersToken>? GetUsersToken()
        {
            try
            {
                return _dbContext.UserToken?.ToList();
            }
            catch
            {
                throw;
            }
        }

        public UsersToken GetUsersTokenDetails(int id)
        {
            try
            {
                UsersToken? userToken = _dbContext.UserToken?.Find(id);
                if (userToken != null)
                {
                    return userToken;
                }
                else
                {
                    throw new ArgumentException("Cannot get the token of user");
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddUsersToken(UsersToken userToken)
        {
            try
            {
                _dbContext.UserToken?.Add(userToken);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUsersToken(UsersToken userToken)
        {
            try
            {
                _dbContext.Entry(userToken).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public UsersToken DeleteUsersToken(int? id = 1)
        {
            try
            {
                UsersToken? userToken = _dbContext.UserToken?.Find(id);

                if (userToken != null)
                {
                    _dbContext.UserToken?.Remove(userToken);
                    _dbContext.SaveChanges();
                    ResetAI();
                    return userToken;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the token of user");
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckUsersToken(int id)
        {
            if(_dbContext.UserToken == null)
            {
                throw new ArgumentException("The token of user has not been found...");
            }

            return _dbContext.UserToken.Any(e => e.UsersTokenId == id);
        }

        public string ResetAI(int id = 0)
        {
            SqlConnection? conn = null;

            try
            {
                conn = new SqlConnection(_configuration.GetConnectionString("MyDBConn"));
                conn.Open();

                SqlCommand cmd = new SqlCommand("DBCC CHECKIDENT ('UserToken', RESEED, "+id+")", conn);
                int rowsAffected = cmd.ExecuteNonQuery();

                if(rowsAffected > 0)
                {
                    return "Updated the auto increment in table dbo.UserToken of value: " + id;
                }
                else
                {
                    return "Unable to update the auto increment in table dbo.UserToken.";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
