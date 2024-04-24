using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly IShare _Shares;

        public ShareController(IShare Shares)
        {
            _Shares = Shares;
        }

        /// <summary>
        /// This endpoint retrives all Shares.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<Share>>> GetShares()
        {
            return await _Shares.GetShares();
        }

        /// <summary>
        /// This endpoint retrives specific Share by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<ActionResult<IEnumerable<Share>>> GetSharesById(int? id)
        {
            return await _Shares.GetSharesById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of Shares for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public IActionResult GetSharesAsEnumList()
        {
            return _Shares.GetSharesAsEnumList();
        }

        /// <summary>
        /// This endpoint retrives all Shares length.
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        [Authorize(Roles = "Administrator,Moderator,User,Guest")]
        public async Task<ActionResult<int>> GetSharesCount()
        {
            return await _Shares.GetSharesCount();
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
        /// This endpoint updates specific Share by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Shares"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> PutShares(int? id, Share Shares)
        {
            return await _Shares.PutShares(id, Shares);
        }

        /// <summary>
        /// This endpoint creates specific Share by body.
        /// </summary>
        /// <param name="Shares"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Share>>> PostShares(Share Shares)
        {
            return await _Shares.PostShares(Shares);
        }

        /// <summary>
        /// This endpoint deletes a specific Share by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> DeleteShares(int? id)
        {
            return await _Shares.DeleteShares(id);
        }

        /// <summary>
        /// This endpoint deletes all Shares.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllShares()
        {
            return await _Shares.DeleteAllShares();
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table Shares.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _Shares.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> SearchShares([FromQuery] QueryParams qryp)
        {
            return await _Shares.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table Shares.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        [Authorize(Roles = "Administrator,Moderator,User")]
        public async Task<IActionResult> GetLastId()
        {
            return await _Shares.GetLastId();
        }
    }
}