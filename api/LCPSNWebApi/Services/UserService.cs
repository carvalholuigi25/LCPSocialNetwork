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
using BC = BCrypt.Net.BCrypt;

namespace LCPSNWebApi.Services
{
    public class UserService : ControllerBase, IUser
    {
        private readonly DBContext _context;
        private IConfiguration _configuration;

        public UserService(DBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
                return NotFound();
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
                return BadRequest(ModelState);
            }

            if (id != Users.UserId)
            {
                return BadRequest();
            }

            if(!string.IsNullOrEmpty(Users.Username)) 
            {
                if(_context.Users.Any(e => e.Username == Users.Username)) {
                    return BadRequest("This username is already taken!");
                }

                CheckIfRoleIsForbiddenInUserName();
            }

            if(!string.IsNullOrEmpty(Users.Email) &&_context.Users.Any(e => e.Email == Users.Email)) 
            {
                return BadRequest("This email is already taken!");
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
                    return NotFound();
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
                return BadRequest(ModelState);
            }

            if(!string.IsNullOrEmpty(UsersData.Username)) 
            {
                if(_context.Users.Any(e => e.Username == UsersData.Username)) {
                    return BadRequest("This username is already taken!");
                }

                CheckIfRoleIsForbiddenInUserName();
            }

            if(!string.IsNullOrEmpty(UsersData.Email) &&_context.Users.Any(e => e.Email == UsersData.Email)) 
            {
                return BadRequest("This email is already taken!");
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
                return BadRequest(ModelState);
            }

            var Users = await _context.Users.FindAsync(id);
            if (Users == null)
            {
                return NotFound();
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
                    return BadRequest(ModelState);
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
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        public async Task<IActionResult> ResetIdSeed(int rsid = 1)
        {
            try
            {
                int result;
                string connectionString = _configuration["DBMode"]!.Contains("SQLite", StringComparison.OrdinalIgnoreCase) ? _configuration["ConnectionStrings:SQLite"]! : _configuration["ConnectionStrings:SQLServer"]!;
                string queryString = $@"DBCC CHECKIDENT('dbo.Users', RESEED, @rsid)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@rsid", rsid);

                    await connection.OpenAsync();
                    result = await command.ExecuteNonQueryAsync();
                }

                return Ok(new { msg = $"Id of table Users has been reset to {rsid}!", qrycmd = queryString.Replace("@rsid", "" + rsid), res = result, status = 200 });
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
                string queryString = $@"SELECT MAX(UserId) FROM users";

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
                        msg = $"This username shouldnt contain of this name: {forbiddenRoleNamesList[c]} because its forbidden!";
                    }
                }
            }

            return BadRequest(msg);
        }
    }
}