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
    public class AttachmentService : ControllerBase, IAttachment
    {
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public AttachmentService(DBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ActionResult<IEnumerable<Attachment>>> GetAttachment()
        {
            return await _context.Attachments.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Attachment>>> GetAttachmentById(int? id)
        {
            var Attachment = await _context.Attachments.Where(x => x.AttachmentId == id).ToListAsync();

            if (Attachment == null)
            {
                return NotFound();
            }

            return Attachment;
        }

        public IActionResult GetAttachmentAsEnumList()
        {
            return Ok(typeof(Attachment).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList());
        }

        public async Task<IActionResult> PutAttachment(int? id, Attachment AttachmentData)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != AttachmentData.AttachmentId)
            {
                return BadRequest();
            }

            _context.Entry(AttachmentData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttachmentExists(id))
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

        public async Task<ActionResult<IEnumerable<Attachment>>> CreateAttachment(Attachment AttachmentData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Attachments.Add(AttachmentData);
            await _context.SaveChangesAsync();

            return await GetAttachment();
            // return CreatedAtAction("GetAttachmentById", new { id = AttachmentData.AttachmentId }, AttachmentData);
        }

        public async Task<IActionResult> DeleteAttachment(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Attachment = await _context.Attachments.FindAsync(id);
            if (Attachment == null)
            {
                return NotFound();
            }

            _context.Attachments.Remove(Attachment);
            await _context.SaveChangesAsync();
            await ResetIdSeed(_context.Attachments.Count());

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

                var queryable = _context.Attachments.AsQueryable();

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

                return Ok(new QueryParamsRes<Attachment>()
                {
                    Data = entities,
                    QueryParams = qparams,
                    Count = entities!.Count,
                    TotalCount = _context.Attachments.Count()
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
                string queryString = $@"DBCC CHECKIDENT('dbo.Attachments', RESEED, @rsid)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@rsid", rsid);

                    await connection.OpenAsync();
                    result = await command.ExecuteNonQueryAsync();
                }

                return Ok(new { msg = $"Id of table Attachments has been reset to {rsid}!", qrycmd = queryString.Replace("@rsid", "" + rsid), res = result, status = 200 });
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
                string queryString = $@"SELECT MAX(AttachmentId) FROM Attachments";

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

        private bool AttachmentExists(int? id)
        {
            return _context.Attachments.Any(e => e.AttachmentId == id);
        }

        private IQueryable<Attachment> DoFilterData(IQueryable<Attachment> queryable, QueryParams qryp)
        {
            Expression<Func<Attachment, bool>> newexp;

            if (qryp.SortBy!.Contains(nameof(Attachment.Title)))
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
            else if (qryp.SortBy!.Contains(nameof(Attachment.Description)))
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
                newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.AttachmentId! == int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.AttachmentId! != int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.AttachmentId! > int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.AttachmentId! >= int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.AttachmentId! < int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.AttachmentId! <= int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.AttachmentId!.ToString()!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.AttachmentId!.ToString()!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.AttachmentId!.ToString()!.StartsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.AttachmentId!.ToString()!.EndsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.AttachmentId!.ToString()!.Length == 0) :
                qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.AttachmentId!.ToString()!.Length > 0) : (x => x.AttachmentId! == int.Parse(qryp.Search!));
            }

            return queryable.Where(newexp);
        }
    }
}