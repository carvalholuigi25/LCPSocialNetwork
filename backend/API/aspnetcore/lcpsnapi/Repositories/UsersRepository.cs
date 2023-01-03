using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Functions;
using lcpsnapi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace lcpsnapi.Repositories
{
    public class UsersRepository : ControllerBase, IUsers
    {
        private readonly MyDBContext _dbContext;
        private readonly IUsersToken _IUsersToken;
        private readonly int Salt = 11;

        public UsersRepository(MyDBContext dbContext, IUsersToken iUsersToken)
        {
            _dbContext = dbContext;
            _IUsersToken = iUsersToken;
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
                user.Password = BC.HashPassword(user.Password, Salt);
                user.Pin = BC.HashPassword(user.Pin, Salt);
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
                user.Password = BC.HashPassword(user.Password, Salt);
                user.Pin = BC.HashPassword(user.Pin, Salt);
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

        public async Task<ActionResult<UsersToken?>> DoUserLog([FromBody] UsersTokenLogin? users)
        {
            #nullable disable
            var usersdata = await Task.FromResult(GetUsers());
            var res = users != null ? usersdata?.FirstOrDefault(x => x.Username == users.Username || x.Email == users.Email) : null;

            if (res == null)
            {
                return BadRequest("The user does not exist in our database, please create new one.");
            }

            if(!CheckIfPasswordIsValid(users.Password, res.Password))
            {
                return BadRequest("The password does not match to his user's password.");
            }

            return new UsersToken()
            {
                UsersTokenId = res.UsersTokenId ?? res.Id,
                Username = res.Username,
                Email = res.Email,
                Password = BC.HashPassword(res.Password, 11),
                Pin = res.Pin,
                Displayname = res.Displayname,
                Cover = res.Cover,
                Image = res.Image,
                Role = res.Role.ToString(),
                UsersId = res.Id,
                DateCreated = Convert.ToDateTime(res.DateRegistered),
                DateExp = MyGenTokens.GenDateExpOnly(Enums.TokenUnitTime.months, 1).ToString("yyyy-MM-ddTHH:mm:ss"),
                Token = MyGenTokens.GenTokenOnly(res.Username, res.Role.Value, Enums.TokenUnitTime.months, 1)
            };
            #nullable enable
        }

        public async Task<ActionResult<UsersToken?>> DoUserReg([FromBody] UsersToken? users, Enums.UserRole? role = Enums.UserRole.user)
        {
            if (CheckUsersIfNull(users?.UsersId))
            {
                return BadRequest("The user already exists in our database!");
            }

            AddUsers(new Users()
            {
                Username = users?.Username,
                Email = users?.Email,
                Password = BC.HashPassword(users?.Password, 11),
                Pin = BC.HashPassword(users?.Pin, 11),
                Displayname = users?.Displayname,
                Image = users?.Image,
                Cover = users?.Cover,
                Role = role
            });

            _IUsersToken.AddUsersToken(new UsersToken()
            {
                Username = users?.Username,
                Email = users?.Email,
                Password = BC.HashPassword(users?.Password, 11),
                Pin = BC.HashPassword(users?.Pin, 11),
                Displayname = users?.Displayname,
                Image = users?.Image,
                Cover = users?.Cover,
                Token = null,
                DateExp = new DateTime().ToString(),
                DateCreated = new DateTime(),
                UsersId = users?.UsersId
            });

            return await Task.FromResult(users);
        }

        public bool CheckIfPasswordIsValid(string password, string hashedpassword)
        {
            return BC.Verify(password, hashedpassword);
        }

        public bool CheckUsers(int id)
        {
            if(_dbContext.User == null)
            {
                throw new ArgumentException("User has not been found...");
            }

            return _dbContext.User.Any(e => e.Id == id);
        }

        public bool CheckUsersIfNull(int? id)
        {
            if (_dbContext.User == null)
            {
                throw new ArgumentException("User has not been found...");
            }

            return _dbContext.User.Any(e => e.Id == id);
        }
    }
}
