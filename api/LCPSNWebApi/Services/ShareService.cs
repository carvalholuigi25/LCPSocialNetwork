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
  public class ShareService : ControllerBase, IShare
    {
        private readonly IStringLocalizer<MyResources> _localizer;
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public ShareService(DBContext context, IConfiguration configuration, IStringLocalizer<MyResources> localizer)
        {
            _context = context;
            _configuration = configuration;
            _localizer = localizer;
        }

        public async Task<ActionResult<IEnumerable<Share>>> GetShares()
        {
            return await _context.Shares.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Share>>> GetSharesById(int? id)
        {
            var Share = await _context.Shares.Where(x => x.ShareId == id).ToListAsync();

            if (Share == null)
            {
                return NotFound(_localizer.GetString("DataNotFound").Value);
            }

            return Share;
        }

        public IActionResult GetSharesAsEnumList()
        {
            return Ok(typeof(Share).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList());
        }

        public async Task<ActionResult<int>> GetSharesCount()
        {
            return (await _context.Shares.ToListAsync()).Count;
        }

        public async Task<ActionResult<int>> GetSharesCountByPostId(int postId = 1) 
        {
            return await _context.Shares.Where(x => x.PostId == postId).CountAsync();
        }

        public async Task<IActionResult> PutShares(int? id, Share Share)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            if (id != Share.ShareId)
            {
                return BadRequest();
            }

            _context.Entry(Share).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShareExists(id))
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

        public async Task<ActionResult<IEnumerable<Share>>> PostShares(Share ShareData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            _context.Shares.Add(ShareData);
            await _context.SaveChangesAsync();

            return await GetShares();
            // return CreatedAtAction("GetSharesById", new { id = ShareData.ShareId }, ShareData);
        }

        public async Task<IActionResult> DeleteShares(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            var Share = await _context.Shares.FindAsync(id);
            if (Share == null)
            {
                return NotFound(_localizer.GetString("DataNotFound").Value);
            }

            _context.Shares.Remove(Share);
            await _context.SaveChangesAsync();
            await ResetIdSeed(_context.Shares.Count());

            return NoContent();
        }

        public async Task<IActionResult> DeleteAllShares()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            var Shares = await _context.Shares.ToListAsync();
            if (Shares == null)
            {
                return NotFound(_localizer.GetString("DataNotFound").Value);
            }

            _context.Shares.RemoveRange(Shares);
            await _context.SaveChangesAsync();
            await ResetIdSeed(_context.Shares.Count());

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

                var queryable = _context.Shares.AsQueryable();

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

                return Ok(new QueryParamsRes<Share>()
                {
                    Data = entities,
                    QueryParams = qparams,
                    Count = entities!.Count,
                    TotalCount = _context.Shares.Count()
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
                string queryString = $@"DBCC CHECKIDENT('dbo.Shares', RESEED, @rsid)";
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
                string queryString = $@"SELECT MAX(ShareId) FROM Shares";
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

        private bool ShareExists(int? id)
        {
            return _context.Shares.Any(e => e.ShareId == id);
        }

        private IQueryable<Share> DoFilterData(IQueryable<Share> queryable, QueryParams qryp)
        {
            Expression<Func<Share, bool>> newexp;

            newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.ShareId! == int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.ShareId! != int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.ShareId! > int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.ShareId! >= int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.ShareId! < int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.ShareId! <= int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.ShareId!.ToString()!.Contains(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.ShareId!.ToString()!.Contains(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.ShareId!.ToString()!.StartsWith(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.ShareId!.ToString()!.EndsWith(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.ShareId!.ToString()!.Length == 0) :
            qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.ShareId!.ToString()!.Length > 0) : (x => x.ShareId! == int.Parse(qryp.Search!));

            return queryable.Where(newexp);
        }
    }
}
