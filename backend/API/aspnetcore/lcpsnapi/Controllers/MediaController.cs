using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lcpsnapi.Classes;
using lcpsnapi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using lcpsnapi.Extensions;

namespace lcpsnapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthMyRoles]
    public class MediaController : ControllerBase
    {
        private readonly IMedia _IMedia;

        public MediaController(IMedia IMedia)
        {
            _IMedia = IMedia;
        }

        /// <summary>
        /// Get all media files
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Media>?>> GetMedia()
        {
            return await Task.FromResult(_IMedia.GetFiles());
        }

        /// <summary>
        /// Get all media files by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Media>> GetMediaDetails(int id)
        {
            var files = await Task.FromResult(_IMedia.GetFilesDetails(id));
            if (files == null)
            {
                return NotFound();
            }
            return files;
        }

        /// <summary>
        /// Insert or upload new media file
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Media>> PostMedia(Media files)
        {
            _IMedia.AddFiles(files);
            return await Task.FromResult(files);
        }

        /// <summary>
        /// Update the current media file by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Media>> PutMedia(int id, Media files)
        {
            if (id != files.MediaId)
            {
                return BadRequest();
            }
            try
            {
                _IMedia.UpdateFiles(files);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(files);
        }

        /// <summary>
        /// Delete the current media file by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Media>> DeleteMedia(int id)
        {
            var user = _IMedia.DeleteFiles(id);
            return await Task.FromResult(user);
        }

        /// <summary>
        /// Check if current media file exists by id
        /// </summary>
        private bool MediaExists(int id)
        {
            return _IMedia.CheckFiles(id);
        }
    }
}
