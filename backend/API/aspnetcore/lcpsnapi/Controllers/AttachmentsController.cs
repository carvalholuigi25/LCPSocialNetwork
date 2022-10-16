using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using lcpsnapi.Classes;
using lcpsnapi.Interfaces;
using lcpsnapi.Extensions;
using static lcpsnapi.Classes.Enums;

namespace lcpsnapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthMyRoles]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachments _IAttachments;

        public AttachmentsController(IAttachments IAttachments)
        {
            _IAttachments = IAttachments;
        }

        /// <summary>
        /// Get all attachments files
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attachments>?>> GetAttachment()
        {
            return await Task.FromResult(_IAttachments.GetAttachments());
        }

        /// <summary>
        /// Get all attachments files by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Attachments>> GetAttachments(int id)
        {
            var attachments = await Task.FromResult(_IAttachments.GetAttachmentsDetails(id));
            if (attachments == null)
            {
                return NotFound();
            }
            return attachments;
        }

        /// <summary>
        /// Insert or upload the current attachment file
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Attachments>> PostAttachments(Attachments attachments)
        {
            _IAttachments.AddAttachments(attachments);
            return await Task.FromResult(attachments);
        }

        /// <summary>
        /// Update the current attachment file by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Attachments>> PutAttachments(int id, Attachments attachments)
        {
            if (id != attachments.AttachmentId)
            {
                return BadRequest();
            }
            try
            {
                _IAttachments.UpdateAttachments(attachments);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttachmentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(attachments);
        }

        /// <summary>
        /// Delete the current attachment file by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Attachments>> DeleteAttachments(int id)
        {
            var user = _IAttachments.DeleteAttachments(id);
            return await Task.FromResult(user);
        }

        /// <summary>
        /// Check if attachment file exists by id
        /// </summary>
        private bool AttachmentsExists(int id)
        {
            return _IAttachments.CheckAttachments(id);
        }
    }
}
