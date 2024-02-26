using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LCPSNWebApi.Interfaces
{
    public interface IPost
    {
        Task<ActionResult<IEnumerable<Post>>> GetPost();
        Task<ActionResult<IEnumerable<Post>>> GetPostById(int? id);
        IActionResult GetPostAsEnumList();
        Task<IActionResult> PutPost(int? id, Post PostData);
        Task<ActionResult<Post>> CreatePost(Post PostData);
        Task<IActionResult> DeletePost(int? id);
        Task<IActionResult> ResetIdSeed(int rsid = 1);
        Task<IActionResult> SearchData([FromQuery] QueryParams qryp);
        Task<IActionResult> GetLastId();
    }
}