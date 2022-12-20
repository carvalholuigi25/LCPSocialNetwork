using lcpsnapi.Classes;
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

        public UsersController(IUsers IUsers)
        {
            _IUsers = IUsers;
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
            return await _IUsers.DoUserLog(users);
        }

        /// <summary>
        /// Do user registration
        /// </summary>
        [HttpPost("auth/register")]
        [AllowAnonymous]
        public async Task<ActionResult<UsersToken?>> DoUserReg([FromBody] UsersToken? users, Enums.UserRole? role = Enums.UserRole.user)
        {
            return await _IUsers.DoUserReg(users, role);
        }

        /// <summary>
        /// Check if user exists by id
        /// </summary>
        private bool UsersExists(int id)
        {
            return _IUsers.CheckUsers(id);
        }
    }
}
