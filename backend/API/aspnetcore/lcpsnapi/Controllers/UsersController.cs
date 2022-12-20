using lcpsnapi.Classes;
using lcpsnapi.Functions;
using lcpsnapi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _IUsers;
        private readonly IUsersToken _IUsersToken;

        public UsersController(IUsers IUsers, IUsersToken IUsersToken)
        {
            _IUsers = IUsers;
            _IUsersToken = IUsersToken;
        }

        /// <summary>
        /// Get all users details
        /// </summary>
        [HttpGet]
        [Authorize(Policy = "PolicyAdminOnly")]
        public async Task<ActionResult<IEnumerable<Users>?>> GetUser()
        {
            return await Task.FromResult(_IUsers.GetUsers());
        }

        /// <summary>
        /// Get user details by id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await Task.FromResult(_IUsers.GetUsersDetails(id));
            if (users == null)
            {
                return NotFound();
            }
            return users;
        }

        /// <summary>
        /// Insert new user details
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _IUsers.AddUsers(users);
            return await Task.FromResult(users);
        }

        /// <summary>
        /// Update user details by id
        /// </summary>
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Users>> PutUsers(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }
            try
            {
                _IUsers.UpdateUsers(users);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(users);
        }

        /// <summary>
        /// Delete user details by id
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var user = _IUsers.DeleteUsers(id);
            return await Task.FromResult(user);
        }
        
        /// <summary>
        /// Do user authentication
        /// </summary>
        [HttpPost("auth/login")]
        [AllowAnonymous]
        public async Task<ActionResult<UsersToken?>> DoUserLogin([FromBody] UsersTokenLogin? users)
        {
            #nullable disable
            var usersdata = await Task.FromResult(_IUsers.GetUsers());
            var res = users != null ? usersdata?.Where(x => x.Username == users.Username || x.Email == users.Email).ToList() : null;

            if(res == null) {
                return BadRequest("The user does not exist in our database, please create new one.");
            }

            return new UsersToken()
            {
                UsersTokenId = res[0].UsersTokenId,
                Username = res[0].Username,
                Email = res[0].Email,
                Password = res[0].Password,
                Pin = res[0].Pin,
                Displayname = res[0].Displayname,
                UsersId = res[0].Id,
                DateCreated = Convert.ToDateTime(res[0].DateRegistered),
                Token = MyGenTokens.GenTokenOnly(res[0].Username, res[0].Role.Value, Enums.TokenUnitTime.months, 1)
            };
            #nullable enable
        }

        /// <summary>
        /// Do user registration
        /// </summary>
        [HttpPost("auth/register")]
        [AllowAnonymous]
        public async Task<ActionResult<UsersToken?>> DoUserReg([FromBody] UsersToken? users, Enums.UserRole? role = Enums.UserRole.user)
        {
            if (UsersExistsIfNull(users?.UsersId))
            {
                return BadRequest("The user already exists in our database!");
            }

            _IUsers.AddUsers(new Users()
            {
                Username = users?.Username,
                Password = users?.Password,
                Email = users?.Email,
                Pin = users?.Pin,
                Displayname = users?.Displayname,
                Role = role
            });
            
            _IUsersToken.AddUsersToken(new UsersToken() {
                Username = users?.Username,
                Password = users?.Password,
                Email = users?.Email,
                Pin = users?.Pin,
                Displayname = users?.Displayname,
                Token = null,
                DateExp = new DateTime().ToString(),
                DateCreated = new DateTime(),
                UsersId = users?.UsersId
            });

            return await Task.FromResult(users);
        }

        /// <summary>
        /// Check if user exists by id
        /// </summary>
        private bool UsersExists(int id)
        {
            return _IUsers.CheckUsers(id);
        }

        /// <summary>
        /// Check if user exists by id if it contains null or not as value
        /// </summary>
        private bool UsersExistsIfNull(int? id)
        {
            return _IUsers.CheckUsersIfNull(id);
        }
    }
}
