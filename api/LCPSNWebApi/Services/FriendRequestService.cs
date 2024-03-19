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
    public class FriendRequestService : ControllerBase, IFriendRequest
    {
        private readonly IStringLocalizer<SharedResource> _shResLoc;
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public FriendRequestService(DBContext context, IConfiguration configuration, IStringLocalizer<SharedResource> shResLoc)
        {
            _context = context;
            _configuration = configuration;
            _shResLoc = shResLoc;
        }

        public async Task<ActionResult<IEnumerable<FriendRequest>>> GetFriendRequests()
        {
            return await _context.FriendRequests.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<FriendRequest>>> GetFriendRequestsById(int? id)
        {
            var FriendRequests = await _context.FriendRequests.Where(x => x.FriendRequestId == id).ToListAsync();

            if (FriendRequests == null)
            {
                return NotFound();
            }

            return FriendRequests;
        }

        public IActionResult GetFriendRequestsAsEnumList()
        {
            return Ok(typeof(FriendRequest).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList());
        }

        public async Task<IActionResult> PutFriendRequests(int? id, FriendRequest FriendRequests)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            if (id != FriendRequests.FriendRequestId)
            {
                return BadRequest();
            }

            _context.Entry(FriendRequests).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendRequestsExists(id))
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

        public async Task<ActionResult<IEnumerable<FriendRequest>>> PostFriendRequests(FriendRequest FriendRequestsData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            _context.FriendRequests.Add(FriendRequestsData);
            await _context.SaveChangesAsync();

            return await GetFriendRequests();
            // return CreatedAtAction("GetFriendRequestsById", new { id = FriendRequestsData.FriendRequestId }, FriendRequestsData);
        }

        public async Task<IActionResult> DeleteFriendRequests(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            var FriendRequests = await _context.FriendRequests.FindAsync(id);
            if (FriendRequests == null)
            {
                return NotFound();
            }

            _context.FriendRequests.Remove(FriendRequests);
            await _context.SaveChangesAsync();
            await ResetIdSeed(_context.FriendRequests.Count());

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

                var queryable = _context.FriendRequests.AsQueryable();

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

                return Ok(new QueryParamsRes<FriendRequest>()
                {
                    Data = entities,
                    QueryParams = qparams,
                    Count = entities!.Count,
                    TotalCount = _context.FriendRequests.Count()
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
                string queryString = $@"DBCC CHECKIDENT('dbo.FriendRequests', RESEED, @rsid)";

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
                string queryString = $@"SELECT MAX(FriendRequestId) FROM FriendRequests";

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

        private bool FriendRequestsExists(int? id)
        {
            return _context.FriendRequests.Any(e => e.FriendRequestId == id);
        }

        private IQueryable<FriendRequest> DoFilterData(IQueryable<FriendRequest> queryable, QueryParams qryp)
        {
            Expression<Func<FriendRequest, bool>> newexp;

            newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.FriendRequestId! == int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.FriendRequestId! != int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.FriendRequestId! > int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.FriendRequestId! >= int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.FriendRequestId! < int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.FriendRequestId! <= int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.FriendRequestId!.ToString()!.Contains(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.FriendRequestId!.ToString()!.Contains(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.FriendRequestId!.ToString()!.StartsWith(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.FriendRequestId!.ToString()!.EndsWith(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.FriendRequestId!.ToString()!.Length == 0) :
            qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.FriendRequestId!.ToString()!.Length > 0) : (x => x.FriendRequestId! == int.Parse(qryp.Search!));

            return queryable.Where(newexp);
        }
    }
}