using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _users;

        public UserController(IUser users)
        {
            _users = users;
        }

        /// <summary>
        /// This endpoint retrives all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _users.GetUsers();
        }

        /// <summary>
        /// This endpoint retrives specific user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersById(int? id)
        {
            return await _users.GetUsersById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of users for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public IActionResult GetUsersAsEnumList()
        {
            return _users.GetUsersAsEnumList();
        }

        /// <summary>
        /// This endpoint retrives list of enums of filter operation for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("fopenumslist")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public IActionResult GetFilterOperationEnumList()
        {
            return Ok(Enum.GetNames(typeof(FilterOperatorEnum)));
        }

        /// <summary>
        /// This endpoint updates specific user by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Users"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> PutUsers(int? id, User Users)
        {
            return await _users.PutUsers(id, Users);
        }

        /// <summary>
        /// This endpoint creates specific user by body.
        /// </summary>
        /// <param name="Users"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<User>>> PostUsers(User Users)
        {
            return await _users.PostUsers(Users);
        }

        /// <summary>
        /// This endpoint deletes a specific user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> DeleteUsers(int? id)
        {
            return await _users.DeleteUsers(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table users.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _users.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> SearchUsers([FromQuery] QueryParams qryp)
        {
            return await _users.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table users.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> GetLastId()
        {
            return await _users.GetLastId();
        }
    }
}