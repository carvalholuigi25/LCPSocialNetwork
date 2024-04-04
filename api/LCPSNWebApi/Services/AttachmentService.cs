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
using LCPSNWebApi.Library.Resources;
using Microsoft.Data.Sqlite;
using MySqlConnector;
using Npgsql;

namespace LCPSNWebApi.Services
{
  public class AttachmentService : ControllerBase, IAttachment
    {
        private readonly IStringLocalizer<MyResources> _localizer;
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public AttachmentService(DBContext context, IConfiguration configuration, IStringLocalizer<MyResources> localizer)
        {
            _context = context;
            _configuration = configuration;
            _localizer = localizer;
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
                return NotFound(string.Format(_localizer.GetString("AttachmentIdNotFound").Value, id));
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
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            if (id != AttachmentData.AttachmentId)
            {
                return BadRequest(string.Format(_localizer.GetString("AttachmentIdNotFound").Value, id));
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
                    return NotFound(_localizer.GetString("DataNotFound").Value);
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
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
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
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            var Attachment = await _context.Attachments.FindAsync(id);
            if (Attachment == null)
            {
                return NotFound(_localizer.GetString("DataNotFound").Value);
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
                    return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
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
                return StatusCode(500, string.Format(_localizer.GetString("DataCatchError").Value, ex.Message));
            }
        }

        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            try
            {
                StringComparison strcomp = StringComparison.OrdinalIgnoreCase;
                string connectionString = GetConnectionStr();
                string queryString = $@"DBCC CHECKIDENT('dbo.Attachments', RESEED, @rsid)";
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
                string queryString = $@"SELECT MAX(AttachmentId) FROM Attachments";
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
