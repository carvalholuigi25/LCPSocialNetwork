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

namespace LCPSNWebApi.Services
{
    public class CommentService : ControllerBase, IComment
    {
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public CommentService(DBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ActionResult<IEnumerable<Comment>>> GetComment()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentById(int? id)
        {
            var Comment = await _context.Comments.Where(x => x.CommentId == id).ToListAsync();

            if (Comment == null)
            {
                return NotFound();
            }

            return Comment;
        }

        public IActionResult GetCommentAsEnumList()
        {
            return Ok(typeof(Comment).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList());
        }

        public async Task<IActionResult> PutComment(int? id, Comment CommentData)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CommentData.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(CommentData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        public async Task<ActionResult<Comment>> CreateComment(Comment CommentData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Comments.Add(CommentData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentById", new { id = CommentData.CommentId }, CommentData);
        }

        public async Task<IActionResult> DeleteComment(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Comment = await _context.Comments.FindAsync(id);
            if (Comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(Comment);
            await _context.SaveChangesAsync();
            await ResetIdSeed(0);

            return NoContent();
        }

        public async Task<IActionResult> SearchData([FromQuery] QueryParams qryp)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var queryable = _context.Comments.AsQueryable();

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

                return Ok(new QueryParamsRes<Comment>()
                {
                    Data = entities,
                    QueryParams = qparams,
                    Count = entities!.Count,
                    TotalCount = _context.Comments.Count()
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            try
            {
                int result;
                string connectionString = _configuration["DBMode"]!.Contains("SQLite", StringComparison.OrdinalIgnoreCase) ? _configuration["ConnectionStrings:SQLite"]! : _configuration["ConnectionStrings:SQLServer"]!;
                string queryString = $@"DBCC CHECKIDENT('dbo.Comments', RESEED, @rsid)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@rsid", rsid);

                    await connection.OpenAsync();
                    result = await command.ExecuteNonQueryAsync();
                }

                return Ok(new { msg = $"Id of table Comments has been reset to {rsid}!", qrycmd = queryString.Replace("@rsid", "" + rsid), res = result, status = 200 });
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, new { msg = $"Internal server error: {ex.Message}" });
            }
        }

        public async Task<IActionResult> GetLastId()
        {
            try
            {
                string msg = "";
                string connectionString = _configuration["DBMode"]!.Contains("SQLite", StringComparison.OrdinalIgnoreCase) ? _configuration["ConnectionStrings:SQLite"]! : _configuration["ConnectionStrings:SQLServer"]!;
                string queryString = $@"SELECT MAX(CommentId) FROM Comments";

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
                return StatusCode(500, new { msg = $"Internal server error: {ex.Message}" });
            }
        }

        private bool CommentExists(int? id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }

        private IQueryable<Comment> DoFilterData(IQueryable<Comment> queryable, QueryParams qryp)
        {
            Expression<Func<Comment, bool>> newexp;

            if (qryp.SortBy!.Contains(nameof(Comment.Title)))
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
            else if (qryp.SortBy!.Contains(nameof(Comment.Description)))
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
                newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.CommentId! == int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.CommentId! != int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.CommentId! > int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.CommentId! >= int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.CommentId! < int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.CommentId! <= int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.CommentId!.ToString()!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.CommentId!.ToString()!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.CommentId!.ToString()!.StartsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.CommentId!.ToString()!.EndsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.CommentId!.ToString()!.Length == 0) :
                qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.CommentId!.ToString()!.Length > 0) : (x => x.CommentId! == int.Parse(qryp.Search!));
            }

            return queryable.Where(newexp);
        }
    }
}