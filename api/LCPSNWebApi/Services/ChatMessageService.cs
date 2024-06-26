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
using LCPSNWebApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LCPSNWebApi.Services
{
  public class ChatMessageService : ControllerBase, IChatMessage
    {
        private readonly IStringLocalizer<MyResources> _localizer;
        private readonly DBContext _context;
        private IConfiguration _configuration;
         private readonly IChatHub _chatHub;

        public ChatMessageService(DBContext context, IConfiguration configuration, IStringLocalizer<MyResources> localizer, IChatHub chatHub)
        {
            _context = context;
            _configuration = configuration;
            _localizer = localizer;
            _chatHub = chatHub;
        }

        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessages()
        {
            return await _context.ChatMessages.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChatMessagesById(int? id)
        {
            var ChatMessages = await _context.ChatMessages.Where(x => x.ChatMessageId == id).ToListAsync();

            if (ChatMessages == null)
            {
                return NotFound(_localizer.GetString("DataNotFound").Value);
            }

            return ChatMessages;
        }

        public IActionResult GetChatMessagesAsEnumList()
        {
            return Ok(typeof(ChatMessage).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList());
        }

        public async Task<ActionResult<int>> GetChatMessagesCount()
        {
            return (await _context.ChatMessages.ToListAsync()).Count;
        }

        public async Task<IActionResult> PutChatMessages(int? id, ChatMessage ChatMessages)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            if (id != ChatMessages.ChatMessageId)
            {
                return BadRequest();
            }

            _context.Entry(ChatMessages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await _chatHub.SendMessage();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatMessagesExists(id))
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

        public async Task<ActionResult<IEnumerable<ChatMessage>>> PostChatMessages(ChatMessage ChatMessagesData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            _context.ChatMessages.Add(ChatMessagesData);
            await _context.SaveChangesAsync();
            await _chatHub.SendMessage();

            return await GetChatMessages();
            // return CreatedAtAction("GetChatMessagesById", new { id = ChatMessagesData.ChatMessageId }, ChatMessagesData);
        }

        public async Task<IActionResult> DeleteChatMessages(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            var ChatMessages = await _context.ChatMessages.FindAsync(id);
            if (ChatMessages == null)
            {
                return NotFound(_localizer.GetString("DataNotFound").Value);
            }

            _context.ChatMessages.Remove(ChatMessages);
            await _context.SaveChangesAsync();
            await ResetIdSeed(_context.ChatMessages.Count());
            await _chatHub.SendMessage();

            return NoContent();
        }

        public async Task<IActionResult> DeleteAllChatMessages()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            var ChatMessages = await _context.ChatMessages.ToListAsync();
            if (ChatMessages == null)
            {
                return NotFound(_localizer.GetString("DataNotFound").Value);
            }

            _context.ChatMessages.RemoveRange(ChatMessages);
            await _context.SaveChangesAsync();
            await ResetIdSeed(_context.ChatMessages.Count());
            await _chatHub.SendMessage();

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

                var queryable = _context.ChatMessages.AsQueryable();

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

                return Ok(new QueryParamsRes<ChatMessage>()
                {
                    Data = entities,
                    QueryParams = qparams,
                    Count = entities!.Count,
                    TotalCount = _context.ChatMessages.Count()
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
                string queryString = $@"DBCC CHECKIDENT('dbo.ChatMessages', RESEED, @rsid)";
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
                string queryString = $@"SELECT MAX(ChatMessageId) FROM ChatMessages";
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

        private bool ChatMessagesExists(int? id)
        {
            return _context.ChatMessages.Any(e => e.ChatMessageId == id);
        }

        private IQueryable<ChatMessage> DoFilterData(IQueryable<ChatMessage> queryable, QueryParams qryp)
        {
            Expression<Func<ChatMessage, bool>> newexp;

            newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.ChatMessageId! == int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.ChatMessageId! != int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.ChatMessageId! > int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.ChatMessageId! >= int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.ChatMessageId! < int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.ChatMessageId! <= int.Parse(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.ChatMessageId!.ToString()!.Contains(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.ChatMessageId!.ToString()!.Contains(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.ChatMessageId!.ToString()!.StartsWith(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.ChatMessageId!.ToString()!.EndsWith(qryp.Search!)) :
            qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.ChatMessageId!.ToString()!.Length == 0) :
            qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.ChatMessageId!.ToString()!.Length > 0) : (x => x.ChatMessageId! == int.Parse(qryp.Search!));

            return queryable.Where(newexp);
        }
    }
}
