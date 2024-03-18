using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly IReply _Replies;

        public ReplyController(IReply Replies)
        {
            _Replies = Replies;
        }

        /// <summary>
        /// This endpoint retrives all Replies.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<Reply>>> GetReplies()
        {
            return await _Replies.GetReply();
        }

        /// <summary>
        /// This endpoint retrives specific Reply by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<Reply>>> GetRepliesById(int? id)
        {
            return await _Replies.GetReplyById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of Replies for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public IActionResult GetRepliesAsEnumList()
        {
            return _Replies.GetReplyAsEnumList();
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
        /// This endpoint updates specific Reply by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Replies"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> PutReplies(int? id, Reply Replies)
        {
            return await _Replies.PutReply(id, Replies);
        }

        /// <summary>
        /// This endpoint creates specific Reply by body.
        /// </summary>
        /// <param name="Replies"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Reply>>> PostReplies(Reply Replies)
        {
            return await _Replies.CreateReply(Replies);
        }

        /// <summary>
        /// This endpoint deletes a specific Reply by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> DeleteReplies(int? id)
        {
            return await _Replies.DeleteReply(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table Replies.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _Replies.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> SearchReplies([FromQuery] QueryParams qryp)
        {
            return await _Replies.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table Replies.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> GetLastId()
        {
            return await _Replies.GetLastId();
        }
    }
}