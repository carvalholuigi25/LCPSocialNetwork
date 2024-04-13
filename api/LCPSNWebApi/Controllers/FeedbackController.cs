using Microsoft.AspNetCore.Mvc;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Authorization;

namespace LCPSNWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,Moderator,User")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedback _Feedbacks;

        public FeedbackController(IFeedback Feedbacks)
        {
            _Feedbacks = Feedbacks;
        }

        /// <summary>
        /// This endpoint retrives all Feedbacks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            return await _Feedbacks.GetFeedback();
        }

        /// <summary>
        /// This endpoint retrives specific Feedback by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacksById(int? id)
        {
            return await _Feedbacks.GetFeedbackById(id);
        }

        /// <summary>
        /// This endpoint retrives list of enums of Feedbacks for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("enumslist")]
        public IActionResult GetFeedbacksAsEnumList()
        {
            return _Feedbacks.GetFeedbackAsEnumList();
        }

        /// <summary>
        /// This endpoint retrives list of enums of filter operation for search feature
        /// </summary>
        /// <returns></returns>
        [HttpGet("fopenumslist")]
        public IActionResult GetFilterOperationEnumList()
        {
            return Ok(Enum.GetNames(typeof(FilterOperatorEnum)));
        }

        /// <summary>
        /// This endpoint updates specific Feedback by id and body.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Feedbacks"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedbacks(int? id, Feedback Feedbacks)
        {
            return await _Feedbacks.PutFeedback(id, Feedbacks);
        }

        /// <summary>
        /// This endpoint creates specific Feedback by body.
        /// </summary>
        /// <param name="Feedbacks"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Feedback>>> CreateFeedbacks(Feedback Feedbacks)
        {
            return await _Feedbacks.CreateFeedback(Feedbacks);
        }

        /// <summary>
        /// This endpoint deletes a specific Feedback by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbacks(int? id)
        {
            return await _Feedbacks.DeleteFeedback(id);
        }

        /// <summary>
        /// This endpoint resets a auto increment seed by id for table Feedbacks.
        /// </summary>
        /// <param name="rsid"></param>
        /// <returns></returns>
        [HttpPost("{rsid}")]
        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            return await _Feedbacks.ResetIdSeed(rsid);
        }

        /// <summary>
        /// This endpoint does the full filter operation for data.
        /// </summary>
        /// <param name="qryp"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<IActionResult> SearchFeedbacks([FromQuery] QueryParams qryp)
        {
            return await _Feedbacks.SearchData(qryp);
        }

        /// <summary>
        /// This endpoint will return the last id for table Feedbacks.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lastid")]
        public async Task<IActionResult> GetLastId()
        {
            return await _Feedbacks.GetLastId();
        }
    }
}