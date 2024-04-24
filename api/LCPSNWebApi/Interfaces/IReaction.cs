using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IReaction
    {
        Task<ActionResult<IEnumerable<Reaction>>> GetReactions();
        Task<ActionResult<IEnumerable<Reaction>>> GetReactionsById(int? id);
        IActionResult GetReactionsAsEnumList();
        Task<ActionResult<int>> GetReactionsCount();
        Task<ActionResult<int>> GetReactionsCountByPostId(int postId = 1);
        Task<IActionResult> PutReactions(int? id, Reaction Reactions);
        Task<ActionResult<IEnumerable<Reaction>>> PostReactions(Reaction ReactionsData);
        Task<IActionResult> DeleteReactions(int? id);
        Task<IActionResult> DeleteAllReactions();
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}