using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IFriendRequest
    {
        Task<ActionResult<IEnumerable<FriendRequest>>> GetFriendRequests();
        Task<ActionResult<IEnumerable<FriendRequest>>> GetFriendRequestsById(int? id);
        IActionResult GetFriendRequestsAsEnumList();
        Task<ActionResult<int>> GetFriendRequestsCount();
        Task<IActionResult> PutFriendRequests(int? id, FriendRequest FriendRequests);
        Task<ActionResult<IEnumerable<FriendRequest>>> PostFriendRequests(FriendRequest FriendRequestsData);
        Task<IActionResult> DeleteFriendRequests(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}