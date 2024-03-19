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
    public class ShareService : ControllerBase, IShare
    {
        private readonly IStringLocalizer<SharedResource> _shResLoc;
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public ShareService(DBContext context, IConfiguration configuration, IStringLocalizer<SharedResource> shResLoc)
        {
            _context = context;
            _configuration = configuration;
            _shResLoc = shResLoc;
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
                return NotFound();
            }

            return Share;
        }

        public IActionResult GetSharesAsEnumList()
        {
            return Ok(typeof(Share).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList());
        }

        public async Task<IActionResult> PutShares(int? id, Share Share)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
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
                    return NotFound();
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
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
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
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            var Share = await _context.Shares.FindAsync(id);
            if (Share == null)
            {
                return NotFound();
            }

            _context.Shares.Remove(Share);
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
                    return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
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
                return StatusCode(500, string.Format(_shResLoc.GetString("DataCatchError").Value, ex.Message));
            }
        }

        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            try
            {
                int result;
                string connectionString = _configuration["DBMode"]!.Contains("SQLite", StringComparison.OrdinalIgnoreCase) ? _configuration["ConnectionStrings:SQLite"]! : _configuration["ConnectionStrings:SQLServer"]!;
                string queryString = $@"DBCC CHECKIDENT('dbo.Share', RESEED, @rsid)";

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
                string queryString = $@"SELECT MAX(ShareId) FROM Share";

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