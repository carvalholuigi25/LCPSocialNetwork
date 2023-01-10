using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lcpsnapi.Classes;
using lcpsnapi.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace lcpsnapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    // [AuthMyRoles]
    public class PostsController : ControllerBase
    {
        private readonly IPosts _IPosts;

        public PostsController(IPosts IPosts)
        {
            _IPosts = IPosts;
        }

        /// <summary>
        /// Get posts
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posts>?>> GetPost()
        {
            return await Task.FromResult(_IPosts.GetPosts());
        }

        /// <summary>
        /// Get posts by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Posts>> GetPosts(int id)
        {
            var posts = await Task.FromResult(_IPosts.GetPostsDetails(id));
            if (posts == null)
            {
                return NotFound();
            }
            return posts;
        }

        /// <summary>
        /// Get posts length
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<object>> GetPostLength()
        {
            return await Task.FromResult(new { length = _IPosts.GetPosts()?.Count() });
        }

        /// <summary>
        /// Get posts length by id
        /// </summary>
        [HttpGet("count/{id}")]
        public async Task<ActionResult<object>> GetPostLengthById(int? id = 1)
        {
            return await Task.FromResult(new { length = _IPosts.GetPosts()?.Where(x => x.PostId == id).Count() });
        }

        /// <summary>
        /// Insert posts
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Posts>> PostPosts(Posts posts)
        {
            _IPosts.AddPosts(posts);
            return await Task.FromResult(posts);
        }

        /// <summary>
        /// Update posts by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Posts>> PutPosts(int id, Posts posts)
        {
            if (id != posts.PostId)
            {
                return BadRequest();
            }
            try
            {
                _IPosts.UpdatePosts(posts);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(posts);
        }

        /// <summary>
        /// Delete posts by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Posts>> DeletePosts(int id)
        {
            var post = _IPosts.DeletePosts(id);
            return await Task.FromResult(post);
        }

        /// <summary>
        /// Reset AI posts by id
        /// </summary>
        [HttpPost("ai/{id}")]
        public void ResetAIByPostId(int? id = 0)
        {
            _IPosts.ResetAIById("Post", id);
        }

        /// <summary>
        /// Check if post exists by id
        /// </summary>
        private bool PostsExists(int id)
        {
            return _IPosts.CheckPosts(id);
        }
    }
}
