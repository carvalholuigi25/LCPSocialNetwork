using LCPSNWebApi.Extensions;
using LCPSNWebApi.Context;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Linq.Expressions;
using LCPSNWebApi.Classes.Filter;
using Microsoft.Extensions.Localization;
using LCPSNLibrary.Resources;

namespace LCPSNWebApi.Services
{
  public class PostService : ControllerBase, IPost
    {
        private readonly IStringLocalizer<MyResources> _shResLoc;
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public PostService(DBContext context, IConfiguration configuration, IStringLocalizer<MyResources> shResLoc)
        {
            _context = context;
            _configuration = configuration;
            _shResLoc = shResLoc;
        }

        public async Task<ActionResult<IEnumerable<Post>>> GetPost()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Post>>> GetPostById(int? id)
        {
            var Posts = await _context.Posts.Where(x => x.PostId == id).ToListAsync();

            if (Posts == null)
            {
                return NotFound();
            }

            return Posts;
        }

        public async Task<ActionResult<IEnumerable<Post>>> GetPostByUserId(int? id)
        {
            var Users = await _context.Posts.Where(x => x.UserId == id).ToListAsync();

            if (Users == null)
            {
                return NotFound();
            }

            return Users;
        }

        public IActionResult GetPostAsEnumList()
        {
            return Ok(typeof(Post).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList());
        }

        public async Task<IActionResult> PutPost(int? id, Post Posts)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            if (id != Posts.PostId)
            {
                return BadRequest();
            }

            _context.Entry(Posts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        public async Task<ActionResult<IEnumerable<Post>>> CreatePost(Post PostsData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            _context.Posts.Add(PostsData);
            await _context.SaveChangesAsync();

            return await GetPost();
            // return CreatedAtAction("GetPostById", new { id = PostsData.PostId }, PostsData);
        }

        public async Task<IActionResult> DeletePost(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            var Posts = await _context.Posts.FindAsync(id);
            if (Posts == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(Posts);
            await _context.SaveChangesAsync();
            await ResetIdSeed(_context.Posts.Count());

            return NoContent();
        }

        public async Task<IActionResult> SearchData([FromQuery] QueryParams qryp)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
                }

                var queryable = _context.Posts.AsQueryable();

                // Apply filtering
                if (!string.IsNullOrEmpty(qryp.Search))
                {
                    queryable = DoFilterData(queryable, qryp);
                }

                // Apply sorting
                if (!string.IsNullOrEmpty(qryp.SortBy))
                {
                    queryable = qryp.SortOrder!.ToString()!.Contains("DESC", StringComparison.OrdinalIgnoreCase) ? queryable.OrderBy(qryp.SortBy, true) : queryable.OrderBy(qryp.SortBy, false);
                }

                var entities = await queryable
                    .Skip((qryp.Page >= 1 ? (qryp.Page - 1) : qryp.Page) * qryp.PageSize)
                    .Take(qryp.PageSize)
                    .ToListAsync();

                var qparams = new QueryParams()
                {
                    PageSize = qryp.PageSize,
                    Page = qryp.Page,
                    SortOrder = qryp.SortOrder,
                    SortBy = qryp.SortBy,
                    Search = qryp.Search,
                    Operator = qryp.Operator
                };

                return Ok(new QueryParamsRes<Post>()
                {
                    Data = entities,
                    QueryParams = qparams,
                    Count = entities!.Count,
                    TotalCount = _context.Posts.Count()
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, string.Format(_shResLoc.GetString("DataCatchError").Value, ex.Message));
            }
        }

        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            try
            {
                int result;
                string connectionString = _configuration["DBMode"]!.Contains("SQLite", StringComparison.OrdinalIgnoreCase) ? _configuration["ConnectionStrings:SQLite"]! : _configuration["ConnectionStrings:SQLServer"]!;
                string queryString = $@"DBCC CHECKIDENT('dbo.Posts', RESEED, @rsid)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@rsid", rsid);

                    await connection.OpenAsync();
                    result = await command.ExecuteNonQueryAsync();
                }

                return Ok(new { msg = string.Format(_shResLoc.GetString("IdTblReset").Value, rsid), qrycmd = queryString.Replace("@rsid", "" + rsid), res = result, status = 200 });
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, string.Format(_shResLoc.GetString("DataCatchError").Value, ex.Message));
            }
        }

        public async Task<IActionResult> GetLastId()
        {
            try
            {
                string msg = "";
                string connectionString = _configuration["DBMode"]!.Contains("SQLite", StringComparison.OrdinalIgnoreCase) ? _configuration["ConnectionStrings:SQLite"]! : _configuration["ConnectionStrings:SQLServer"]!;
                string queryString = $@"SELECT MAX(PostId) FROM Posts";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        msg = "Id: " + reader.GetFieldValue<int>(0);
                    }
                }

                return Ok(new { msg, qrycmd = queryString, status = 200 });
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, string.Format(_shResLoc.GetString("DataCatchError").Value, ex.Message));
            }
        }

        private bool PostsExists(int? id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }

        private IQueryable<Post> DoFilterData(IQueryable<Post> queryable, QueryParams qryp)
        {
            Expression<Func<Post, bool>> newexp;

            if (qryp.SortBy!.Contains(nameof(Post.Title)))
            {
                newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.Title! == qryp.Search!) : 
                qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.Title! != qryp.Search!) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.Title!.Length > qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.Title!.Length >= qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.Title!.Length < qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.Title!.Length < qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.Title!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.Title!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.Title!.StartsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.Title!.EndsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.Title!.Length == 0) :
                qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.Title!.Length > 0) : (x => x.Title!.Contains(qryp.Search!));
            }
            else if (qryp.SortBy!.Contains(nameof(Post.Description)))
            {
                newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.Description! == qryp.Search!) :
                qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.Description! != qryp.Search!) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.Description!.Length > qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.Description!.Length >= qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.Description!.Length < qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.Description!.Length < qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.Description!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.Description!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.Description!.StartsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.Description!.EndsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.Description!.Length == 0) :
                qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.Description!.Length > 0) : (x => x.Description!.Contains(qryp.Search!));
            }
            else
            {
                newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.PostId! == int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.PostId! != int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.PostId! > int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.PostId! >= int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.PostId! < int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.PostId! <= int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.PostId!.ToString()!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.PostId!.ToString()!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.PostId!.ToString()!.StartsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.PostId!.ToString()!.EndsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.PostId!.ToString()!.Length == 0) :
                qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.PostId!.ToString()!.Length > 0) : (x => x.PostId! == int.Parse(qryp.Search!));
            }

            return queryable.Where(newexp);
        }
    }
}
