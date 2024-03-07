using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,Moderator,User")]
    public class FriendController : ControllerBase
    {
        private readonly IFriend _Friends;

        public FriendController(IFriend Friends)
        {
            _Friends = Friends;
        }

        /// <summary>
        /// This endpoint retrives all Friends.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> GetFriends()
        {
            return await _Friends.GetFriends();
        }

        /// <summary>
        /// This endpoint retrives specific Friend by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Friend>>> GetFriendsById(int? id)
        {
            return await _Friends.GetFriendsById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of Friends for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        public IActionResult GetFriendsAsEnumList()
        {
            return _Friends.GetFriendsAsEnumList();
        }

        /// <summary>
        /// This endpoint retrives list of enums of filter operation for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("fopenumslist")]
        public IActionResult GetFilterOperationEnumList()
        {
            return Ok(Enum.GetNames(typeof(FilterOperatorEnum)));
        }

        /// <summary>
        /// This endpoint updates specific Friend by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Friends"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriends(int? id, Friend Friends)
        {
            return await _Friends.PutFriends(id, Friends);
        }

        /// <summary>
        /// This endpoint creates specific Friend by body.
        /// </summary>
        /// <param name="Friends"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Friend>>> PostFriends(Friend Friends)
        {
            return await _Friends.PostFriends(Friends);
        }

        /// <summary>
        /// This endpoint deletes a specific Friend by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriends(int? id)
        {
            return await _Friends.DeleteFriends(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table Friends.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _Friends.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<IActionResult> SearchFriends([FromQuery] QueryParams qryp)
        {
            return await _Friends.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table Friends.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        public async Task<IActionResult> GetLastId()
        {
            return await _Friends.GetLastId();
        }
    }
}