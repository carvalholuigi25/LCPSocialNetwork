using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IUserNotifications
    {
        Task<ActionResult<IEnumerable<UserNotification>>> GetUserNotifications();
        Task<ActionResult<IEnumerable<UserNotification>>> GetUserNotificationsById(int? id);
        IActionResult GetUserNotificationsAsEnumList();
        Task<IActionResult> PutUserNotifications(int? id, UserNotification UserNotifications);
        Task<ActionResult<IEnumerable<UserNotification>>> PostUserNotifications(UserNotification UserNotificationsData);
        Task<IActionResult> DeleteUserNotifications(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}