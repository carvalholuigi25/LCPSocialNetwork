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
    public class CommentController : ControllerBase
    {
        private readonly IComment _Comments;

        public CommentController(IComment Comments)
        {
            _Comments = Comments;
        }

        /// <summary>
        /// This endpoint retrives all Comments.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _Comments.GetComment();
        }

        /// <summary>
        /// This endpoint retrives specific Comment by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsById(int? id)
        {
            return await _Comments.GetCommentById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of Comments for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        public IActionResult GetCommentsAsEnumList()
        {
            return _Comments.GetCommentAsEnumList();
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
        /// This endpoint updates specific Comment by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Comments"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComments(int? id, Comment Comments)
        {
            return await _Comments.PutComment(id, Comments);
        }

        /// <summary>
        /// This endpoint creates specific Comment by body.
        /// </summary>
        /// <param name="Comments"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Comment>>> CreateComments(Comment Comments)
        {
            return await _Comments.CreateComment(Comments);
        }

        /// <summary>
        /// This endpoint deletes a specific Comment by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComments(int? id)
        {
            return await _Comments.DeleteComment(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table Comments.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _Comments.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<IActionResult> SearchComments([FromQuery] QueryParams qryp)
        {
            return await _Comments.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table Comments.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        public async Task<IActionResult> GetLastId()
        {
            return await _Comments.GetLastId();
        }
    }
}