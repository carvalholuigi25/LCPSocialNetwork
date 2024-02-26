using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IUser
    {
        Task<ActionResult<IEnumerable<User>>> GetUsers();
        Task<ActionResult<IEnumerable<User>>> GetUsersById(int? id);
        IActionResult GetUsersAsEnumList();
        Task<IActionResult> PutUsers(int? id, User Users);
        Task<ActionResult<User>> PostUsers(User UsersData);
        Task<IActionResult> DeleteUsers(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}