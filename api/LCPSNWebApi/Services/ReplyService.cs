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
using Microsoft.Extensions.Localization;
using LCPSNWebApi.Library.Resources;
using Microsoft.Data.Sqlite;
using MySqlConnector;
using Npgsql;

namespace LCPSNWebApi.Services
{
  public class ReplyService : ControllerBase, IReply
    {
        private readonly IStringLocalizer<MyResources> _localizer;
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public ReplyService(DBContext context, IConfiguration configuration, IStringLocalizer<MyResources> localizer)
        {
            _context = context;
            _configuration = configuration;
            _localizer = localizer;
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
                return NotFound(_localizer.GetString("DataNotFound").Value);
            }

            return Reply;
        }

        public async Task<ActionResult<IEnumerable<Reply>>> GetReplyByUserId(int? id)
        {
            var Reply = await _context.Replies.Where(x => x.UserId == id).ToListAsync();

            if (Reply == null)
            {
                return NotFound(_localizer.GetString("DataNotFound").Value);
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
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
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
                    return NotFound(_localizer.GetString("DataNotFound").Value);
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
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
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
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            var Reply = await _context.Replies.FindAsync(id);
            if (Reply == null)
            {
                return NotFound(_localizer.GetString("DataNotFound").Value);
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
                    return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
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
                return StatusCode(500, string.Format(_localizer.GetString("DataCatchError").Value, ex.Message));
            }
        }

        

        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            try
            {
                StringComparison strcomp = StringComparison.OrdinalIgnoreCase;
                string connectionString = GetConnectionStr();
                string queryString = $@"DBCC CHECKIDENT('dbo.Replies', RESEED, @rsid)";
                int result;

                using (var connection = GetConnectionType(connectionString, strcomp)) 
                {
                    var command = GetConnectionCommand(queryString, connection, strcomp);
                    command.Parameters.AddWithValue("@rsid", rsid);

                    await connection.OpenAsync();
                    result = await command.ExecuteNonQueryAsync();
                }

                return Ok(new { msg = string.Format(_localizer.GetString("IdTblReset"), rsid), qrycmd = queryString.Replace("@rsid", "" + rsid), res = result, status = 200 });
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, string.Format(_localizer.GetString("DataCatchError").Value, $"{ex.Message}"));
            }
        }

        public async Task<IActionResult> GetLastId()
        {
            try
            {
                StringComparison strcomp = StringComparison.OrdinalIgnoreCase;
                string msg = "";
                string connectionString = GetConnectionStr();
                string queryString = $@"SELECT MAX(ReplyId) FROM Replies";
                var id = 0;

                using (var connection = GetConnectionType(connectionString, strcomp))
                {
                    var command = GetConnectionCommand(queryString, connection, strcomp);

                    await connection.OpenAsync();
                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        id = reader.GetFieldValue<int>(0);
                        msg = "Id: " + id;
                    }
                }

                return Ok(new { msg, data = id, qrycmd = queryString, status = 200 });
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, string.Format(_localizer.GetString("DataCatchError").Value, $"{ex.Message}"));
            }
        }

        private string GetDBMode() => _configuration["DBMode"]!.ToString();

        private string GetConnectionStr() {
            return GetDBMode()!.Contains("SQLite", StringComparison.OrdinalIgnoreCase) ? _configuration["ConnectionStrings:SQLite"]! : 
            GetDBMode()!.Contains("MySQL", StringComparison.OrdinalIgnoreCase) ?  _configuration["ConnectionStrings:MySQL"]! :  
            GetDBMode()!.Contains("PostgreSQL", StringComparison.OrdinalIgnoreCase) ?  _configuration["ConnectionStrings:PostgreSQL"]! : _configuration["ConnectionStrings:SQLServer"]!;
        }

        private dynamic GetConnectionType(string connectionString, StringComparison strcomp = StringComparison.OrdinalIgnoreCase) {
            return GetDBMode().Contains("SQLite", strcomp) ? new SqliteConnection(connectionString) : 
            GetDBMode().Contains("MySQL", strcomp) ? new MySqlConnection(connectionString) : 
            GetDBMode().Contains("PostgreSQL", strcomp) ? new NpgsqlConnection(connectionString) : 
            new SqlConnection(connectionString);
        }

        private dynamic GetConnectionCommand(string queryString, dynamic connection, StringComparison strcomp = StringComparison.OrdinalIgnoreCase) {
            return GetDBMode().Contains("SQLite", strcomp) ? new SqliteCommand(queryString, connection) : 
            GetDBMode().Contains("MySQL", strcomp) ? new MySqlCommand(queryString, connection) : 
            GetDBMode().Contains("PostgreSQL", strcomp) ? new NpgsqlCommand(queryString, connection) : 
            new SqlCommand(queryString, connection);
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
