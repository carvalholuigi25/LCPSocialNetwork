using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lcpsnapi.Classes;
using lcpsnapi.Interfaces;
using lcpsnapi.Extensions;

namespace lcpsnapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthMyRoles]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievements _IAchievements;

        public AchievementsController(IAchievements IAchievements)
        {
            _IAchievements = IAchievements;
        }

        /// <summary>
        /// Get all achievements items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Achievements>?>> GetAchievement()
        {
            return await Task.FromResult(_IAchievements.GetAchievements());
        }

        /// <summary>
        /// Get all achievements items by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Achievements>> GetAchievements(int id)
        {
            var achievements = await Task.FromResult(_IAchievements.GetAchievementsDetails(id));
            if (achievements == null)
            {
                return NotFound();
            }
            return achievements;
        }

        /// <summary>
        /// Insert the current achievement item
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Achievements>> PostAchievements(Achievements achievements)
        {
            _IAchievements.AddAchievements(achievements);
            return await Task.FromResult(achievements);
        }

        /// <summary>
        /// Update the current achievement item by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Achievements>> PutAchievements(int id, Achievements achievements)
        {
            if (id != achievements.Id)
            {
                return BadRequest();
            }
            try
            {
                _IAchievements.UpdateAchievements(achievements);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchievementsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(achievements);
        }

        /// <summary>
        /// Delete the current achievement item by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Achievements>> DeleteAchievements(int id)
        {
            var user = _IAchievements.DeleteAchievements(id);
            return await Task.FromResult(user);
        }

        /// <summary>
        /// Check if achievement item exists by id
        /// </summary>
        private bool AchievementsExists(int id)
        {
            return _IAchievements.CheckAchievementsItem(id);
        }
    }
}
