using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNotificationController : ControllerBase
    {
        private readonly IUserNotifications _UserNotifications;

        public UserNotificationController(IUserNotifications UserNotifications)
        {
            _UserNotifications = UserNotifications;
        }

        /// <summary>
        /// This endpoint retrives all UserNotifications.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<UserNotification>>> GetUserNotifications()
        {
            return await _UserNotifications.GetUserNotifications();
        }

        /// <summary>
        /// This endpoint retrives specific UserNotification by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<UserNotification>>> GetUserNotificationsById(int? id)
        {
            return await _UserNotifications.GetUserNotificationsById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of UserNotifications for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public IActionResult GetUserNotificationsAsEnumList()
        {
            return _UserNotifications.GetUserNotificationsAsEnumList();
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
        /// This endpoint updates specific UserNotification by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UserNotifications"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> PutUserNotifications(int? id, UserNotification UserNotifications)
        {
            return await _UserNotifications.PutUserNotifications(id, UserNotifications);
        }

        /// <summary>
        /// This endpoint creates specific UserNotification by body.
        /// </summary>
        /// <param name="UserNotifications"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserNotification>>> PostUserNotifications(UserNotification UserNotifications)
        {
            return await _UserNotifications.PostUserNotifications(UserNotifications);
        }

        /// <summary>
        /// This endpoint deletes a specific UserNotification by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> DeleteUserNotifications(int? id)
        {
            return await _UserNotifications.DeleteUserNotifications(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table UserNotifications.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _UserNotifications.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> SearchUserNotifications([FromQuery] QueryParams qryp)
        {
            return await _UserNotifications.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table UserNotifications.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> GetLastId()
        {
            return await _UserNotifications.GetLastId();
        }
    }
}