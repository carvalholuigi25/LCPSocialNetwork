using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IReply
    {
        Task<ActionResult<IEnumerable<Reply>>> GetReply();
        Task<ActionResult<IEnumerable<Reply>>> GetReplyById(int? id);
        Task<ActionResult<IEnumerable<Reply>>> GetReplyByUserId(int? userId);
        IActionResult GetReplyAsEnumList();
        Task<IActionResult> PutReply(int? id, Reply ReplyData);
        Task<ActionResult<IEnumerable<Reply>>> CreateReply(Reply ReplyData);
        Task<IActionResult> DeleteReply(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}