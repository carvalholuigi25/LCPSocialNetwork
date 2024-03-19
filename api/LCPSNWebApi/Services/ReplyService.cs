using LCPSNWebApi.Extensions;
using LCPSNWebApi.Context;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Linq.Expressions;
using LCPSNWebApi.Resource;
using Microsoft.Extensions.Localization;

namespace LCPSNWebApi.Services
{
    public class ReplyService : ControllerBase, IReply
    {
        private readonly IStringLocalizer<SharedResource> _shResLoc;
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public ReplyService(DBContext context, IConfiguration configuration, IStringLocalizer<SharedResource> shResLoc)
        {
            _context = context;
            _configuration = configuration;
            _shResLoc = shResLoc;
        }

        public async Task<ActionResult<IEnumerable<Reply>>> GetReply()
        {
            return await _context.Replies.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Reply>>> GetReplyById(int? id)
        {
            var Reply = await _context.Replies.Where(x => x.ReplyId == id).ToListAsync();

            if (Reply == null)
            {
                return NotFound();
            }

            return Reply;
        }

        public async Task<ActionResult<IEnumerable<Reply>>> GetReplyByUserId(int? id)
        {
            var Reply = await _context.Replies.Where(x => x.UserId == id).ToListAsync();

            if (Reply == null)
            {
                return NotFound();
            }

            return Reply;
        }

        public IActionResult GetReplyAsEnumList()
        {
            return Ok(typeof(Reply).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList());
        }

        public async Task<IActionResult> PutReply(int? id, Reply Reply)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            if (id != Reply.ReplyId)
            {
                return BadRequest();
            }

            _context.Entry(Reply).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplyExists(id))
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

        public async Task<ActionResult<IEnumerable<Reply>>> CreateReply(Reply ReplyData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            _context.Replies.Add(ReplyData);
            await _context.SaveChangesAsync();

            return await GetReply();
            // return CreatedAtAction("GetReplyById", new { id = ReplyData.ReplyId }, ReplyData);
        }

        public async Task<IActionResult> DeleteReply(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            var Reply = await _context.Replies.FindAsync(id);
            if (Reply == null)
            {
                return NotFound();
            }

            _context.Replies.Remove(Reply);
            await _context.SaveChangesAsync();
            await ResetIdSeed(_context.Replies.Count());

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

                var queryable = _context.Replies.AsQueryable();

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

                return Ok(new QueryParamsRes<Reply>()
                {
                    Data = entities,
                    QueryParams = qparams,
                    Count = entities!.Count,
                    TotalCount = _context.Replies.Count()
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
                string queryString = $@"DBCC CHECKIDENT('dbo.Reply', RESEED, @rsid)";

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
                string queryString = $@"SELECT MAX(ReplyId) FROM Reply";

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

        private bool ReplyExists(int? id)
        {
            return _context.Replies.Any(e => e.ReplyId == id);
        }

        private IQueryable<Reply> DoFilterData(IQueryable<Reply> queryable, QueryParams qryp)
        {
            Expression<Func<Reply, bool>> newexp;

            newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.ReplyId! == int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.ReplyId! != int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.ReplyId! > int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.ReplyId! >= int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.ReplyId! < int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.ReplyId! <= int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.ReplyId!.ToString()!.Contains(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.ReplyId!.ToString()!.Contains(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.ReplyId!.ToString()!.StartsWith(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.ReplyId!.ToString()!.EndsWith(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.ReplyId!.ToString()!.Length == 0) :
            qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.ReplyId!.ToString()!.Length > 0) : (x => x.ReplyId! == int.Parse(qryp.Search!));

            return queryable.Where(newexp);
        }
    }
}