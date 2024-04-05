using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessage _ChatMessages;

        public ChatMessageController(IChatMessage ChatMessages)
        {
            _ChatMessages = ChatMessages;
        }

        /// <summary>
        /// This endpoint retrives all ChatMessages.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessages()
        {
            return await _ChatMessages.GetChatMessages();
        }

        /// <summary>
        /// This endpoint retrives specific ChatMessage by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessagesById(int? id)
        {
            return await _ChatMessages.GetChatMessagesById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of ChatMessages for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public IActionResult GetChatMessagesAsEnumList()
        {
            return _ChatMessages.GetChatMessagesAsEnumList();
        }

        /// <summary>
        /// This endpoint retrives all ChatMessages length.
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public async Task<ActionResult<int>> GetChatMessagesCount()
        {
            return await _ChatMessages.GetChatMessagesCount();
        }

        /// <summary>
        /// This endpoint retrives list of enums of filter operation for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("fopenumslist")]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public IActionResult GetFilterOperationEnumList()
        {
            return Ok(Enum.GetNames(typeof(FilterOperatorEnum)));
        }

        /// <summary>
        /// This endpoint updates specific ChatMessage by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ChatMessages"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public async Task<IActionResult> PutChatMessages(int? id, ChatMessage ChatMessages)
        {
            return await _ChatMessages.PutChatMessages(id, ChatMessages);
        }

        /// <summary>
        /// This endpoint creates specific ChatMessage by body.
        /// </summary>
        /// <param name="ChatMessages"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> PostChatMessages(ChatMessage ChatMessages)
        {
            return await _ChatMessages.PostChatMessages(ChatMessages);
        }

        /// <summary>
        /// This endpoint deletes a specific ChatMessage by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public async Task<IActionResult> DeleteChatMessages(int? id)
        {
            return await _ChatMessages.DeleteChatMessages(id);
        }

        /// <summary>
        /// This endpoint deletes all ChatMessages.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("all")]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public async Task<IActionResult> DeleteAllChatMessages()
        {
            return await _ChatMessages.DeleteAllChatMessages();
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table ChatMessages.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _ChatMessages.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public async Task<IActionResult> SearchChatMessages([FromQuery] QueryParams qryp)
        {
            return await _ChatMessages.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table ChatMessages.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public async Task<IActionResult> GetLastId()
        {
            return await _ChatMessages.GetLastId();
        }
    }
}