using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IChatMessage
    {
        Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessages();
        Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessagesById(int? id);
        IActionResult GetChatMessagesAsEnumList();
        Task<IActionResult> PutChatMessages(int? id, ChatMessage Messages);
        Task<ActionResult<IEnumerable<ChatMessage>>> PostChatMessages(ChatMessage MessagesData);
        Task<IActionResult> DeleteChatMessages(int? id);
        Task<IActionResult> DeleteAllChatMessages();
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}