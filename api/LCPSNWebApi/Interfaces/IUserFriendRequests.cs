using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IUserFriendRequest
    {
        Task<ActionResult<IEnumerable<UserFriendRequest>>> GetUserFriendRequests();
        Task<ActionResult<IEnumerable<UserFriendRequest>>> GetUserFriendRequestsById(int? id);
        IActionResult GetUserFriendRequestsAsEnumList();
        Task<IActionResult> PutUserFriendRequests(int? id, UserFriendRequest UserFriendRequests);
        Task<ActionResult<IEnumerable<UserFriendRequest>>> PostUserFriendRequests(UserFriendRequest UserFriendRequestsData);
        Task<IActionResult> DeleteUserFriendRequests(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}