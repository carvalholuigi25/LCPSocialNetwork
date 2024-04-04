using LCPSNWebApi.Context;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Filter;
using LCPSNWebApi.Extensions;
using LCPSNWebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Reflection;
using System.Linq.Expressions;
using BC = BCrypt.Net.BCrypt;
using LCPSNWebApi.Library.Resources;
using Microsoft.Data.Sqlite;
using MySqlConnector;
using Npgsql;

namespace LCPSNWebApi.Services
{
  public class UserService : ControllerBase, IUser
    {
        private readonly IStringLocalizer<MyResources> _localizer;
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public UserService(DBContext context, IConfiguration configuration, IStringLocalizer<MyResources> localizer)
        {
            _context = context;
            _configuration = configuration;
            _localizer = localizer;
        }

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<User>>> GetUsersById(int? id)
        {
            var Users = await _context.Users.Where(x => x.UserId == id).ToListAsync();

            if (Users == null)
            {
                return NotFound(_localizer.GetString("DataNotFound").Value);
            }

            return Users;
        }

        public IActionResult GetUsersAsEnumList()
        {
            return Ok(typeof(User).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList());
        }

        public async Task<IActionResult> PutUsers(int? id, User Users)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            if (id != Users.UserId)
            {
                return BadRequest();
            }

            if(!string.IsNullOrEmpty(Users.Username)) 
            {
                if(_context.Users.Any(e => e.Username == Users.Username)) {
                    return BadRequest(string.Format(_localizer.GetString("UsernameTaken").Value, Users.Username));
                }

                CheckIfRoleIsForbiddenInUserName();
            }

            if(!string.IsNullOrEmpty(Users.Email) &&_context.Users.Any(e => e.Email == Users.Email)) 
            {
                return BadRequest(string.Format(_localizer.GetString("EmailTaken").Value, Users.Email));
            }

            if (!string.IsNullOrEmpty(Users.Password))
            {
                Users.Password = BC.HashPassword(Users.Password, BC.GenerateSalt(12), false, BCrypt.Net.HashType.SHA256);
            }

            _context.Entry(Users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        public async Task<ActionResult<IEnumerable<User>>> PostUsers(User UsersData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            if(!string.IsNullOrEmpty(UsersData.Username)) 
            {
                if(_context.Users.Any(e => e.Username == UsersData.Username)) {
                    return BadRequest(string.Format(_localizer.GetString("UsernameTaken").Value, UsersData.Username));
                }

                CheckIfRoleIsForbiddenInUserName();
            }

            if(!string.IsNullOrEmpty(UsersData.Email) &&_context.Users.Any(e => e.Email == UsersData.Email)) 
            {
                return BadRequest(string.Format(_localizer.GetString("EmailTaken").Value, UsersData.Email));
            }

            UsersData.Password = BC.HashPassword(UsersData.Password, BC.GenerateSalt(12), false, BCrypt.Net.HashType.SHA256);
            _context.Users.Add(UsersData);
            await _context.SaveChangesAsync();

            return await GetUsers();
            // return CreatedAtAction("GetUsersById", new { id = UsersData.UserId }, UsersData);
        }

        public async Task<IActionResult> DeleteUsers(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(string.Format(_localizer.GetString("ModelInvalid").Value, ModelState));
            }

            var Users = await _context.Users.FindAsync(id);
            if (Users == null)
            {
                return NotFound(_localizer.GetString("DataNotFound").Value);
            }

            _context.Users.Remove(Users);
            await _context.SaveChangesAsync();
            await ResetIdSeed(_context.Users.Count());

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

                var queryable = _context.Users.AsQueryable();

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

                return Ok(new QueryParamsRes<User>()
                {
                    Data = entities,
                    QueryParams = qparams,
                    Count = entities!.Count,
                    TotalCount = _context.Users.Count()
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, string.Format(_localizer.GetString("DataCatchError").Value, $"{ex.Message}"));
            }
        }

        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            try
            {
                StringComparison strcomp = StringComparison.OrdinalIgnoreCase;
                string connectionString = GetConnectionStr();
                string queryString = $@"DBCC CHECKIDENT('dbo.Users', RESEED, @rsid)";
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
                string queryString = $@"SELECT MAX(UserId) FROM Users";
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

        private string GetDBMode() {
            return _configuration["DBMode"]!.ToString();
        }

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

        private bool UsersExists(int? id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        private IQueryable<User> DoFilterData(IQueryable<User> queryable, QueryParams qryp)
        {
            Expression<Func<User, bool>> newexp;

            if (qryp.SortBy!.Contains("Username", StringComparison.InvariantCultureIgnoreCase))
            {
                newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.Username! == qryp.Search!) : 
                qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.Username! != qryp.Search!) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.Username!.Length > qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.Username!.Length >= qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.Username!.Length < qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.Username!.Length < qryp.Search!.Length) :
                qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.Username!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.Username!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.Username!.StartsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.Username!.EndsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.Username!.Length == 0) :
                qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.Username!.Length > 0) : (x => x.Username!.Contains(qryp.Search!));
            }
            else
            {
                newexp = qryp.Operator!.Value == FilterOperatorEnum.Equals ? (x => x.UserId! == int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.DoesntEqual ? (x => x.UserId! != int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThan ? (x => x.UserId! > int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.GreaterThanOrEqual ? (x => x.UserId! >= int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThan ? (x => x.UserId! < int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.LessThanOrEqual ? (x => x.UserId! <= int.Parse(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.Contains ? (x => x.UserId!.ToString()!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.NotContains ? (x => !x.UserId!.ToString()!.Contains(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.StartsWith ? (x => x.UserId!.ToString()!.StartsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.EndsWith ? (x => x.UserId!.ToString()!.EndsWith(qryp.Search!)) :
                qryp.Operator!.Value == FilterOperatorEnum.IsEmpty ? (x => x.UserId!.ToString()!.Length == 0) :
                qryp.Operator!.Value == FilterOperatorEnum.IsNotEmpty ? (x => x.UserId!.ToString()!.Length > 0) : (x => x.UserId! == int.Parse(qryp.Search!));
            }

            return queryable.Where(newexp);
        }

        private BadRequestObjectResult CheckIfRoleIsForbiddenInUserName() {
            string[] forbiddenRoleNamesList = ["Administrator", "Moderator"];
            string msg = "";

            if(forbiddenRoleNamesList.Length > 0) {
                for(var c = 0; c < forbiddenRoleNamesList.Length; c++) {
                    if(_context.Users.Any(e => e.Username.Contains(forbiddenRoleNamesList[c]).ToString().Length > 1)) {
                        msg = string.Format(_localizer.GetString("ChkRoleNameInUserName").Value, $"{forbiddenRoleNamesList[c]}");
                    }
                }
            }

            return BadRequest(msg);
        }
    }
}
