using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendRequestController : ControllerBase
    {
        private readonly IFriendRequest _FriendRequests;

        public FriendRequestController(IFriendRequest FriendRequests)
        {
            _FriendRequests = FriendRequests;
        }

        /// <summary>
        /// This endpoint retrives all FriendRequests.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<FriendRequest>>> GetFriendRequests()
        {
            return await _FriendRequests.GetFriendRequests();
        }

        /// <summary>
        /// This endpoint retrives specific FriendRequest by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<FriendRequest>>> GetFriendRequestsById(int? id)
        {
            return await _FriendRequests.GetFriendRequestsById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of FriendRequests for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public IActionResult GetFriendRequestsAsEnumList()
        {
            return _FriendRequests.GetFriendRequestsAsEnumList();
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
        /// This endpoint updates specific FriendRequest by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FriendRequests"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> PutFriendRequests(int? id, FriendRequest FriendRequests)
        {
            return await _FriendRequests.PutFriendRequests(id, FriendRequests);
        }

        /// <summary>
        /// This endpoint creates specific FriendRequest by body.
        /// </summary>
        /// <param name="FriendRequests"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FriendRequest>>> PostFriendRequests(FriendRequest FriendRequests)
        {
            return await _FriendRequests.PostFriendRequests(FriendRequests);
        }

        /// <summary>
        /// This endpoint deletes a specific FriendRequest by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> DeleteFriendRequests(int? id)
        {
            return await _FriendRequests.DeleteFriendRequests(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table FriendRequests.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _FriendRequests.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> SearchFriendRequests([FromQuery] QueryParams qryp)
        {
            return await _FriendRequests.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table FriendRequests.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> GetLastId()
        {
            return await _FriendRequests.GetLastId();
        }
    }
}