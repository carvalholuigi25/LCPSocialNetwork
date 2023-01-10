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
    public class GamesController : ControllerBase
    {
        private readonly IGames _IGames;

        public GamesController(IGames IGames)
        {
            _IGames = IGames;
        }

        /// <summary>
        /// Get Games
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Games>?>> GetGame()
        {
            return await Task.FromResult(_IGames.GetGames());
        }

        /// <summary>
        /// Get Games by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Games>> GetGames(int id)
        {
            var Games = await Task.FromResult(_IGames.GetGamesDetails(id));
            if (Games == null)
            {
                return NotFound();
            }
            return Games;
        }

        /// <summary>
        /// Get Games length
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<object>> GetGameLength()
        {
            return await Task.FromResult(new { length = _IGames.GetGames()?.Count() });
        }

        /// <summary>
        /// Get Games length by id
        /// </summary>
        [HttpGet("count/{id}")]
        public async Task<ActionResult<object>> GetGameLengthById(int? id = 1)
        {
            return await Task.FromResult(new { length = _IGames.GetGames()?.Where(x => x.GameId == id).Count() });
        }

        /// <summary>
        /// Insert Games
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Games>> InsertGames(Games Games)
        {
            _IGames.AddGames(Games);
            return await Task.FromResult(Games);
        }

        /// <summary>
        /// Update Games by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Games>> PutGames(int id, Games Games)
        {
            if (id != Games.GameId)
            {
                return BadRequest();
            }
            try
            {
                _IGames.UpdateGames(Games);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(Games);
        }

        /// <summary>
        /// Delete Games by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Games>> DeleteGames(int id)
        {
            var game = _IGames.DeleteGames(id);
            return await Task.FromResult(game);
        }

        /// <summary>
        /// Reset AI Games by id
        /// </summary>
        [HttpPost("ai/{id}")]
        public void ResetAIByGameId(int? id = 0)
        {
            _IGames.ResetAIById("Game", id);
        }

        /// <summary>
        /// Check if game exists by id
        /// </summary>
        private bool GamesExists(int id)
        {
            return _IGames.CheckGames(id);
        }
    }
}
