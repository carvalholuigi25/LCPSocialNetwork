using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IAttachment
    {
        Task<ActionResult<IEnumerable<Attachment>>> GetAttachment();
        Task<ActionResult<IEnumerable<Attachment>>> GetAttachmentById(int? id);
        IActionResult GetAttachmentAsEnumList();
        Task<IActionResult> PutAttachment(int? id, Attachment AttachmentData);
        Task<ActionResult<IEnumerable<Attachment>>> CreateAttachment(Attachment AttachmentData);
        Task<IActionResult> DeleteAttachment(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}