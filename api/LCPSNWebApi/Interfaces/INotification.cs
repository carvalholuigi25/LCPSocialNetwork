using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface INotification
    {
        Task<ActionResult<IEnumerable<Notification>>> GetNotifications();
        Task<ActionResult<IEnumerable<Notification>>> GetNotificationsById(int? id);
        IActionResult GetNotificationsAsEnumList();
        Task<IActionResult> PutNotifications(int? id, Notification Notifications);
        Task<ActionResult<IEnumerable<Notification>>> PostNotifications(Notification NotificationsData);
        Task<IActionResult> DeleteNotifications(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}