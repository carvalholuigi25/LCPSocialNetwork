using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IFriend
    {
        Task<ActionResult<IEnumerable<Friend>>> GetFriends();
        Task<ActionResult<IEnumerable<Friend>>> GetFriendsById(int? id);
        IActionResult GetFriendsAsEnumList();
        Task<IActionResult> PutFriends(int? id, Friend Friends);
        Task<ActionResult<IEnumerable<Friend>>> PostFriends(Friend FriendsData);
        Task<IActionResult> DeleteFriends(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}