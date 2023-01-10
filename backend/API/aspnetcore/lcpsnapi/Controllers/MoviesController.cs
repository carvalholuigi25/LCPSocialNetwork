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
    public class MoviesController : ControllerBase
    {
        private readonly IMovies _IMovies;

        public MoviesController(IMovies IMovies)
        {
            _IMovies = IMovies;
        }

        /// <summary>
        /// Get Movies
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>?>> GetMovie()
        {
            return await Task.FromResult(_IMovies.GetMovies());
        }

        /// <summary>
        /// Get Movies by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Movies>> GetMovies(int id)
        {
            var Movies = await Task.FromResult(_IMovies.GetMoviesDetails(id));
            if (Movies == null)
            {
                return NotFound();
            }
            return Movies;
        }

        /// <summary>
        /// Get Movies length
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<object>> GetMovieLength()
        {
            return await Task.FromResult(new { length = _IMovies.GetMovies()?.Count() });
        }

        /// <summary>
        /// Get Movies length by id
        /// </summary>
        [HttpGet("count/{id}")]
        public async Task<ActionResult<object>> GetMovieLengthById(int? id = 1)
        {
            return await Task.FromResult(new { length = _IMovies.GetMovies()?.Where(x => x.MovieId == id).Count() });
        }

        /// <summary>
        /// Insert Movies
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Movies>> InsertMovies(Movies Movies)
        {
            _IMovies.AddMovies(Movies);
            return await Task.FromResult(Movies);
        }

        /// <summary>
        /// Update Movies by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Movies>> PutMovies(int id, Movies Movies)
        {
            if (id != Movies.MovieId)
            {
                return BadRequest();
            }
            try
            {
                _IMovies.UpdateMovies(Movies);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(Movies);
        }

        /// <summary>
        /// Delete Movies by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movies>> DeleteMovies(int id)
        {
            var movie = _IMovies.DeleteMovies(id);
            return await Task.FromResult(movie);
        }

        /// <summary>
        /// Reset AI Movies by id
        /// </summary>
        [HttpPost("ai/{id}")]
        public void ResetAIByMovieId(int? id = 0)
        {
            _IMovies.ResetAIById("Movie", id);
        }

        /// <summary>
        /// Check if movie exists by id
        /// </summary>
        private bool MoviesExists(int id)
        {
            return _IMovies.CheckMovies(id);
        }
    }
}
