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
    public class AnimesController : ControllerBase
    {
        private readonly IAnimes _IAnimes;

        public AnimesController(IAnimes IAnimes)
        {
            _IAnimes = IAnimes;
        }

        /// <summary>
        /// Get Animes
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animes>?>> GetAnime()
        {
            return await Task.FromResult(_IAnimes.GetAnimes());
        }

        /// <summary>
        /// Get Animes by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Animes>> GetAnimes(int id)
        {
            var Animes = await Task.FromResult(_IAnimes.GetAnimesDetails(id));
            if (Animes == null)
            {
                return NotFound();
            }
            return Animes;
        }

        /// <summary>
        /// Get Animes length
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<object>> GetAnimeLength()
        {
            return await Task.FromResult(new { length = _IAnimes.GetAnimes()?.Count() });
        }

        /// <summary>
        /// Get Animes length by id
        /// </summary>
        [HttpGet("count/{id}")]
        public async Task<ActionResult<object>> GetAnimeLengthById(int? id = 1)
        {
            return await Task.FromResult(new { length = _IAnimes.GetAnimes()?.Where(x => x.AnimeId == id).Count() });
        }

        /// <summary>
        /// Insert Animes
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Animes>> InsertAnimes(Animes Animes)
        {
            _IAnimes.AddAnimes(Animes);
            return await Task.FromResult(Animes);
        }

        /// <summary>
        /// Update Animes by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Animes>> PutAnimes(int id, Animes Animes)
        {
            if (id != Animes.AnimeId)
            {
                return BadRequest();
            }
            try
            {
                _IAnimes.UpdateAnimes(Animes);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(Animes);
        }

        /// <summary>
        /// Delete Animes by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Animes>> DeleteAnimes(int id)
        {
            var anime = _IAnimes.DeleteAnimes(id);
            return await Task.FromResult(anime);
        }

        /// <summary>
        /// Reset AI Animes by id
        /// </summary>
        [HttpPost("ai/{id}")]
        public void ResetAIByAnimeId(int? id = 0)
        {
            _IAnimes.ResetAIById("Anime", id);
        }

        /// <summary>
        /// Check if anime exists by id
        /// </summary>
        private bool AnimesExists(int id)
        {
            return _IAnimes.CheckAnimes(id);
        }
    }
}
