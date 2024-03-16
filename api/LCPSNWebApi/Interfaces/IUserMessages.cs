using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IUserMessages
    {
        Task<ActionResult<IEnumerable<UserMessage>>> GetUserMessages();
        Task<ActionResult<IEnumerable<UserMessage>>> GetUserMessagesById(int? id);
        IActionResult GetUserMessagesAsEnumList();
        Task<IActionResult> PutUserMessages(int? id, UserMessage UserMessages);
        Task<ActionResult<IEnumerable<UserMessage>>> PostUserMessages(UserMessage UserMessagesData);
        Task<IActionResult> DeleteUserMessages(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}