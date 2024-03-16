using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessageController : ControllerBase
    {
        private readonly IUserMessages _UserMessages;

        public UserMessageController(IUserMessages UserMessages)
        {
            _UserMessages = UserMessages;
        }

        /// <summary>
        /// This endpoint retrives all UserMessages.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<UserMessage>>> GetUserMessages()
        {
            return await _UserMessages.GetUserMessages();
        }

        /// <summary>
        /// This endpoint retrives specific UserMessage by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<UserMessage>>> GetUserMessagesById(int? id)
        {
            return await _UserMessages.GetUserMessagesById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of UserMessages for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public IActionResult GetUserMessagesAsEnumList()
        {
            return _UserMessages.GetUserMessagesAsEnumList();
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
        /// This endpoint updates specific UserMessage by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UserMessages"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> PutUserMessages(int? id, UserMessage UserMessages)
        {
            return await _UserMessages.PutUserMessages(id, UserMessages);
        }

        /// <summary>
        /// This endpoint creates specific UserMessage by body.
        /// </summary>
        /// <param name="UserMessages"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserMessage>>> PostUserMessages(UserMessage UserMessages)
        {
            return await _UserMessages.PostUserMessages(UserMessages);
        }

        /// <summary>
        /// This endpoint deletes a specific UserMessage by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> DeleteUserMessages(int? id)
        {
            return await _UserMessages.DeleteUserMessages(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table UserMessages.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _UserMessages.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> SearchUserMessages([FromQuery] QueryParams qryp)
        {
            return await _UserMessages.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table UserMessages.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> GetLastId()
        {
            return await _UserMessages.GetLastId();
        }
    }
}