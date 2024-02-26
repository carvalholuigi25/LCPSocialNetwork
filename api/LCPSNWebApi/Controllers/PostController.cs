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
    public class PostController : ControllerBase
    {
        private readonly IPost _posts;

        public PostController(IPost posts)
        {
            _posts = posts;
        }

        /// <summary>
        /// This endpoint retrives all posts.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return await _posts.GetPost();
        }

        /// <summary>
        /// This endpoint retrives specific post by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsById(int? id)
        {
            return await _posts.GetPostById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of posts for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        public IActionResult GetPostsAsEnumList()
        {
            return _posts.GetPostAsEnumList();
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
        /// This endpoint updates specific post by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="posts"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosts(int? id, Post posts)
        {
            return await _posts.PutPost(id, posts);
        }

        /// <summary>
        /// This endpoint creates specific post by body.
        /// </summary>
        /// <param name="posts"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePosts(Post posts)
        {
            return await _posts.CreatePost(posts);
        }

        /// <summary>
        /// This endpoint deletes a specific post by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosts(int? id)
        {
            return await _posts.DeletePost(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table posts.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _posts.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<IActionResult> SearchPosts([FromQuery] QueryParams qryp)
        {
            return await _posts.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table posts.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        public async Task<IActionResult> GetLastId()
        {
            return await _posts.GetLastId();
        }
    }
}