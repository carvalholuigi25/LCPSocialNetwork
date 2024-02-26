using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,Moderator,User")]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachment _Attachments;

        public AttachmentController(IAttachment Attachments)
        {
            _Attachments = Attachments;
        }

        /// <summary>
        /// This endpoint retrives all Attachments.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attachment>>> GetAttachments()
        {
            return await _Attachments.GetAttachment();
        }

        /// <summary>
        /// This endpoint retrives specific Attachment by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Attachment>>> GetAttachmentsById(int? id)
        {
            return await _Attachments.GetAttachmentById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of Attachments for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        public IActionResult GetAttachmentsAsEnumList()
        {
            return _Attachments.GetAttachmentAsEnumList();
        }

        /// <summary>
        /// This endpoint retrives list of enums of filter operation for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("fopenumslist")]
        public IActionResult GetFilterOperationEnumList()
        {
            return Ok(Enum.GetNames(typeof(FilterOperatorEnum)));
        }

        /// <summary>
        /// This endpoint updates specific Attachment by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Attachments"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttachments(int? id, Attachment Attachments)
        {
            return await _Attachments.PutAttachment(id, Attachments);
        }

        /// <summary>
        /// This endpoint creates specific Attachment by body.
        /// </summary>
        /// <param name="Attachments"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Attachment>> CreateAttachments(Attachment Attachments)
        {
            return await _Attachments.CreateAttachment(Attachments);
        }

        /// <summary>
        /// This endpoint deletes a specific Attachment by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachments(int? id)
        {
            return await _Attachments.DeleteAttachment(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table Attachments.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _Attachments.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<IActionResult> SearchAttachments([FromQuery] QueryParams qryp)
        {
            return await _Attachments.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table Attachments.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        public async Task<IActionResult> GetLastId()
        {
            return await _Attachments.GetLastId();
        }
    }
}