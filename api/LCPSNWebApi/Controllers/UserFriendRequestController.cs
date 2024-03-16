using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFriendRequestController : ControllerBase
    {
        private readonly IUserFriendRequest _UserFriendRequests;

        public UserFriendRequestController(IUserFriendRequest UserFriendRequests)
        {
            _UserFriendRequests = UserFriendRequests;
        }

        /// <summary>
        /// This endpoint retrives all UserFriendRequests.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<UserFriendRequest>>> GetUserFriendRequests()
        {
            return await _UserFriendRequests.GetUserFriendRequests();
        }

        /// <summary>
        /// This endpoint retrives specific UserFriendRequest by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<UserFriendRequest>>> GetUserFriendRequestsById(int? id)
        {
            return await _UserFriendRequests.GetUserFriendRequestsById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of UserFriendRequests for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public IActionResult GetUserFriendRequestsAsEnumList()
        {
            return _UserFriendRequests.GetUserFriendRequestsAsEnumList();
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
        /// This endpoint updates specific UserFriendRequest by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UserFriendRequests"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> PutUserFriendRequests(int? id, UserFriendRequest UserFriendRequests)
        {
            return await _UserFriendRequests.PutUserFriendRequests(id, UserFriendRequests);
        }

        /// <summary>
        /// This endpoint creates specific UserFriendRequest by body.
        /// </summary>
        /// <param name="UserFriendRequests"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserFriendRequest>>> PostUserFriendRequests(UserFriendRequest UserFriendRequests)
        {
            return await _UserFriendRequests.PostUserFriendRequests(UserFriendRequests);
        }

        /// <summary>
        /// This endpoint deletes a specific UserFriendRequest by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> DeleteUserFriendRequests(int? id)
        {
            return await _UserFriendRequests.DeleteUserFriendRequests(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table UserFriendRequests.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _UserFriendRequests.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> SearchUserFriendRequests([FromQuery] QueryParams qryp)
        {
            return await _UserFriendRequests.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table UserFriendRequests.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> GetLastId()
        {
            return await _UserFriendRequests.GetLastId();
        }
    }
}