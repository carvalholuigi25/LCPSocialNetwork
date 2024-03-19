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
    public class ReactionService : ControllerBase, IReaction
    {
        private readonly IStringLocalizer<SharedResource> _shResLoc;
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public ReactionService(DBContext context, IConfiguration configuration, IStringLocalizer<SharedResource> shResLoc)
        {
            _context = context;
            _configuration = configuration;
            _shResLoc = shResLoc;
        }

        public async Task<ActionResult<IEnumerable<Reaction>>> GetReactions()
        {
            return await _context.Reactions.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Reaction>>> GetReactionsById(int? id)
        {
            var Reactions = await _context.Reactions.Where(x => x.ReactionId == id).ToListAsync();

            if (Reactions == null)
            {
                return NotFound();
            }

            return Reactions;
        }

        public IActionResult GetReactionsAsEnumList()
        {
            return Ok(typeof(Reaction).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList());
        }

        public async Task<IActionResult> PutReactions(int? id, Reaction Reactions)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            if (id != Reactions.ReactionId)
            {
                return BadRequest();
            }

            _context.Entry(Reactions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReactionsExists(id))
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

        public async Task<ActionResult<IEnumerable<Reaction>>> PostReactions(Reaction ReactionsData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            _context.Reactions.Add(ReactionsData);
            await _context.SaveChangesAsync();

            return await GetReactions();
            // return CreatedAtAction("GetReactionsById", new { id = ReactionsData.ReactionId }, ReactionsData);
        }

        public async Task<IActionResult> DeleteReactions(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_shResLoc.GetString("ModelInvalid").Value, ModelState));
            }

            var Reactions = await _context.Reactions.FindAsync(id);
            if (Reactions == null)
            {
                return NotFound();
            }

            _context.Reactions.Remove(Reactions);
            await _context.SaveChangesAsync();
            await ResetIdSeed(_context.Reactions.Count());

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

                var queryable = _context.Reactions.AsQueryable();

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

                return Ok(new QueryParamsRes<Reaction>()
                {
                    Data = entities,
                    QueryParams = qparams,
                    Count = entities!.Count,
                    TotalCount = _context.Reactions.Count()
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
                string queryString = $@"DBCC CHECKIDENT('dbo.Reactions', RESEED, @rsid)";

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
                string queryString = $@"SELECT MAX(ReactionId) FROM Reactions";

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

        private bool ReactionsExists(int? id)
        {
            return _context.Reactions.Any(e => e.ReactionId == id);
        }

        private IQueryable<Reaction> DoFilterData(IQueryable<Reaction> queryable, QueryParams qryp)
        {
            Expression<Func<Reaction, bool>> newexp;

            newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.ReactionId! == int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.ReactionId! != int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.ReactionId! > int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.ReactionId! >= int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.ReactionId! < int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.ReactionId! <= int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.ReactionId!.ToString()!.Contains(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.ReactionId!.ToString()!.Contains(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.ReactionId!.ToString()!.StartsWith(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.ReactionId!.ToString()!.EndsWith(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.ReactionId!.ToString()!.Length == 0) :
            qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.ReactionId!.ToString()!.Length > 0) : (x => x.ReactionId! == int.Parse(qryp.Search!));

            return queryable.Where(newexp);
        }
    }
}