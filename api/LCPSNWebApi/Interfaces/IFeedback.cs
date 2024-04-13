using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IFeedback
    {
        Task<ActionResult<IEnumerable<Feedback>>> GetFeedback();
        Task<ActionResult<IEnumerable<Feedback>>> GetFeedbackById(int? id);
        IActionResult GetFeedbackAsEnumList();
        Task<IActionResult> PutFeedback(int? id, Feedback FeedbackData);
        Task<ActionResult<IEnumerable<Feedback>>> CreateFeedback(Feedback FeedbackData);
        Task<IActionResult> DeleteFeedback(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}