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
    public class TVSeriesController : ControllerBase
    {
        private readonly ITVSeries _ITVSeries;

        public TVSeriesController(ITVSeries ITVSeries)
        {
            _ITVSeries = ITVSeries;
        }

        /// <summary>
        /// Get TVSeries
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TVSeries>?>> GetTVSerie()
        {
            return await Task.FromResult(_ITVSeries.GetTVSeries());
        }

        /// <summary>
        /// Get TVSeries by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TVSeries>> GetTVSeries(int id)
        {
            var TVSeries = await Task.FromResult(_ITVSeries.GetTVSeriesDetails(id));
            if (TVSeries == null)
            {
                return NotFound();
            }
            return TVSeries;
        }

        /// <summary>
        /// Get TVSeries length
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<object>> GetTVSerieLength()
        {
            return await Task.FromResult(new { length = _ITVSeries.GetTVSeries()?.Count() });
        }

        /// <summary>
        /// Get TVSeries length by id
        /// </summary>
        [HttpGet("count/{id}")]
        public async Task<ActionResult<object>> GetTVSerieLengthById(int? id = 1)
        {
            return await Task.FromResult(new { length = _ITVSeries.GetTVSeries()?.Where(x => x.TVSerieId == id).Count() });
        }

        /// <summary>
        /// Insert TVSeries
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TVSeries>> InsertTVSeries(TVSeries TVSeries)
        {
            _ITVSeries.AddTVSeries(TVSeries);
            return await Task.FromResult(TVSeries);
        }

        /// <summary>
        /// Update TVSeries by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<TVSeries>> PutTVSeries(int id, TVSeries TVSeries)
        {
            if (id != TVSeries.TVSerieId)
            {
                return BadRequest();
            }
            try
            {
                _ITVSeries.UpdateTVSeries(TVSeries);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TVSeriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(TVSeries);
        }

        /// <summary>
        /// Delete TVSeries by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TVSeries>> DeleteTVSeries(int id)
        {
            var tvserie = _ITVSeries.DeleteTVSeries(id);
            return await Task.FromResult(tvserie);
        }

        /// <summary>
        /// Reset AI TVSeries by id
        /// </summary>
        [HttpPost("ai/{id}")]
        public void ResetAIByTVSerieId(int? id = 0)
        {
            _ITVSeries.ResetAIById("TVSerie", id);
        }

        /// <summary>
        /// Check if tvserie exists by id
        /// </summary>
        private bool TVSeriesExists(int id)
        {
            return _ITVSeries.CheckTVSeries(id);
        }
    }
}
