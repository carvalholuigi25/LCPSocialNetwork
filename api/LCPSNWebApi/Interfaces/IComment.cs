using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IComment
    {
        Task<ActionResult<IEnumerable<Comment>>> GetComment();
        Task<ActionResult<IEnumerable<Comment>>> GetCommentById(int? id);
        Task<ActionResult<IEnumerable<Comment>>> GetCommentByPostId(int? postId);
        IActionResult GetCommentAsEnumList();
        Task<ActionResult<int>> GetCommentCount();
        Task<ActionResult<int>> GetCommentCountByPostId(int postId = 1);
        Task<IActionResult> PutComment(int? id, Comment CommentData);
        Task<ActionResult<IEnumerable<Comment>>> CreateComment(Comment CommentData);
        Task<IActionResult> DeleteComment(int? id);
        Task<IActionResult> DeleteAllComments();
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}