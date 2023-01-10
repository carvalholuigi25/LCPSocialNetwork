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
    public class ComicBooksController : ControllerBase
    {
        private readonly IComicBooks _IComicBooks;

        public ComicBooksController(IComicBooks IComicBooks)
        {
            _IComicBooks = IComicBooks;
        }

        /// <summary>
        /// Get ComicBooks
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComicBooks>?>> GetComicBook()
        {
            return await Task.FromResult(_IComicBooks.GetComicBooks());
        }

        /// <summary>
        /// Get ComicBooks by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ComicBooks>> GetComicBooks(int id)
        {
            var ComicBooks = await Task.FromResult(_IComicBooks.GetComicBooksDetails(id));
            if (ComicBooks == null)
            {
                return NotFound();
            }
            return ComicBooks;
        }

        /// <summary>
        /// Get ComicBooks length
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<object>> GetComicBookLength()
        {
            return await Task.FromResult(new { length = _IComicBooks.GetComicBooks()?.Count() });
        }

        /// <summary>
        /// Get ComicBooks length by id
        /// </summary>
        [HttpGet("count/{id}")]
        public async Task<ActionResult<object>> GetComicBookLengthById(int? id = 1)
        {
            return await Task.FromResult(new { length = _IComicBooks.GetComicBooks()?.Where(x => x.ComicBookId == id).Count() });
        }

        /// <summary>
        /// Insert ComicBooks
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ComicBooks>> InsertComicBooks(ComicBooks ComicBooks)
        {
            _IComicBooks.AddComicBooks(ComicBooks);
            return await Task.FromResult(ComicBooks);
        }

        /// <summary>
        /// Update ComicBooks by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ComicBooks>> PutComicBooks(int id, ComicBooks ComicBooks)
        {
            if (id != ComicBooks.ComicBookId)
            {
                return BadRequest();
            }
            try
            {
                _IComicBooks.UpdateComicBooks(ComicBooks);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComicBooksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(ComicBooks);
        }

        /// <summary>
        /// Delete ComicBooks by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ComicBooks>> DeleteComicBooks(int id)
        {
            var comicbook = _IComicBooks.DeleteComicBooks(id);
            return await Task.FromResult(comicbook);
        }

        /// <summary>
        /// Reset AI ComicBooks by id
        /// </summary>
        [HttpPost("ai/{id}")]
        public void ResetAIByComicBookId(int? id = 0)
        {
            _IComicBooks.ResetAIById("ComicBook", id);
        }

        /// <summary>
        /// Check if comicbook exists by id
        /// </summary>
        private bool ComicBooksExists(int id)
        {
            return _IComicBooks.CheckComicBooks(id);
        }
    }
}
