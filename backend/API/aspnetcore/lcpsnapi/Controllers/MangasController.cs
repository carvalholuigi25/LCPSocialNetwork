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
    public class MangasController : ControllerBase
    {
        private readonly IMangas _IMangas;

        public MangasController(IMangas IMangas)
        {
            _IMangas = IMangas;
        }

        /// <summary>
        /// Get Mangas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mangas>?>> GetManga()
        {
            return await Task.FromResult(_IMangas.GetMangas());
        }

        /// <summary>
        /// Get Mangas by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Mangas>> GetMangas(int id)
        {
            var Mangas = await Task.FromResult(_IMangas.GetMangasDetails(id));
            if (Mangas == null)
            {
                return NotFound();
            }
            return Mangas;
        }

        /// <summary>
        /// Get Mangas length
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<object>> GetMangaLength()
        {
            return await Task.FromResult(new { length = _IMangas.GetMangas()?.Count() });
        }

        /// <summary>
        /// Get Mangas length by id
        /// </summary>
        [HttpGet("count/{id}")]
        public async Task<ActionResult<object>> GetMangaLengthById(int? id = 1)
        {
            return await Task.FromResult(new { length = _IMangas.GetMangas()?.Where(x => x.MangaId == id).Count() });
        }

        /// <summary>
        /// Insert Mangas
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Mangas>> InsertMangas(Mangas Mangas)
        {
            _IMangas.AddMangas(Mangas);
            return await Task.FromResult(Mangas);
        }

        /// <summary>
        /// Update Mangas by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Mangas>> PutMangas(int id, Mangas Mangas)
        {
            if (id != Mangas.MangaId)
            {
                return BadRequest();
            }
            try
            {
                _IMangas.UpdateMangas(Mangas);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MangasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(Mangas);
        }

        /// <summary>
        /// Delete Mangas by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Mangas>> DeleteMangas(int id)
        {
            var manga = _IMangas.DeleteMangas(id);
            return await Task.FromResult(manga);
        }

        /// <summary>
        /// Reset AI Mangas by id
        /// </summary>
        [HttpPost("ai/{id}")]
        public void ResetAIByMangaId(int? id = 0)
        {
            _IMangas.ResetAIById("Manga", id);
        }

        /// <summary>
        /// Check if manga exists by id
        /// </summary>
        private bool MangasExists(int id)
        {
            return _IMangas.CheckMangas(id);
        }
    }
}
