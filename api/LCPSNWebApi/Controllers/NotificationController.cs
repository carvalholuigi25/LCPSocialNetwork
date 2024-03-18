using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotification _Notifications;

        public NotificationController(INotification Notifications)
        {
            _Notifications = Notifications;
        }

        /// <summary>
        /// This endpoint retrives all Notifications.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            return await _Notifications.GetNotifications();
        }

        /// <summary>
        /// This endpoint retrives specific Notification by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsById(int? id)
        {
            return await _Notifications.GetNotificationsById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of Notifications for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public IActionResult GetNotificationsAsEnumList()
        {
            return _Notifications.GetNotificationsAsEnumList();
        }

        /// <summary>
        /// This endpoint retrives list of enums of filter operation for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("fopenumslist")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public IActionResult GetFilterOperationEnumList()
        {
            return Ok(Enum.GetNames(typeof(FilterOperatorEnum)));
        }

        /// <summary>
        /// This endpoint updates specific Notification by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Notifications"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> PutNotifications(int? id, Notification Notifications)
        {
            return await _Notifications.PutNotifications(id, Notifications);
        }

        /// <summary>
        /// This endpoint creates specific Notification by body.
        /// </summary>
        /// <param name="Notifications"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Notification>>> PostNotifications(Notification Notifications)
        {
            return await _Notifications.PostNotifications(Notifications);
        }

        /// <summary>
        /// This endpoint deletes a specific Notification by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> DeleteNotifications(int? id)
        {
            return await _Notifications.DeleteNotifications(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table Notifications.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _Notifications.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> SearchNotifications([FromQuery] QueryParams qryp)
        {
            return await _Notifications.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table Notifications.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> GetLastId()
        {
            return await _Notifications.GetLastId();
        }
    }
}