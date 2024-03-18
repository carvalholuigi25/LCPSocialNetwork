using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionController : ControllerBase
    {
        private readonly IReaction _Reactions;

        public ReactionController(IReaction Reactions)
        {
            _Reactions = Reactions;
        }

        /// <summary>
        /// This endpoint retrives all Reactions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<Reaction>>> GetReactions()
        {
            return await _Reactions.GetReactions();
        }

        /// <summary>
        /// This endpoint retrives specific Reaction by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<Reaction>>> GetReactionsById(int? id)
        {
            return await _Reactions.GetReactionsById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of Reactions for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public IActionResult GetReactionsAsEnumList()
        {
            return _Reactions.GetReactionsAsEnumList();
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
        /// This endpoint updates specific Reaction by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Reactions"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> PutReactions(int? id, Reaction Reactions)
        {
            return await _Reactions.PutReactions(id, Reactions);
        }

        /// <summary>
        /// This endpoint creates specific Reaction by body.
        /// </summary>
        /// <param name="Reactions"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Reaction>>> PostReactions(Reaction Reactions)
        {
            return await _Reactions.PostReactions(Reactions);
        }

        /// <summary>
        /// This endpoint deletes a specific Reaction by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> DeleteReactions(int? id)
        {
            return await _Reactions.DeleteReactions(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table Reactions.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _Reactions.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> SearchReactions([FromQuery] QueryParams qryp)
        {
            return await _Reactions.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table Reactions.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> GetLastId()
        {
            return await _Reactions.GetLastId();
        }
    }
}