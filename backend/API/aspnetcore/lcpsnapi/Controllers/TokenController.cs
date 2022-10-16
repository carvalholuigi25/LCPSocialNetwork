using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using static lcpsnapi.Classes.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace lcpsnapi.Controllers
{
    [Route("api/token")]
    [ApiController]
    [AllowAnonymous]
    // [AuthMyRoles]
    public class TokenController : ControllerBase
    {
        #nullable disable

        public IConfiguration _configuration;
        public MyDBContext _context;
        private readonly IUsersToken _IUsersToken;

        public TokenController(IConfiguration config, MyDBContext context, IUsersToken iUsersToken)
        {
            _configuration = config;
            _context = context;
            _IUsersToken = iUsersToken;
        }

        /// <summary>
        /// Get all users tokens
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersToken>>> GetUserTokens()
        {
            return await Task.FromResult(_IUsersToken.GetUsersToken());
        }

        /// <summary>
        /// Checks if token is valid by token generated from user login
        /// </summary>
        [HttpGet("{token}")]
        public async Task<ActionResult<UsersTokenInfo>> CheckIfTokenIsValid(string token)
        {
            return await CheckIfItemTokenIsValid(token);
        }

        /// <summary>
        /// Insert the user token by unit time and his value
        /// </summary>
        [HttpPost("{unitTime}/{unitTimeVal}")]
        public async Task<ActionResult<UsersTokenInfo>> Post(UsersTokenLogin _userData, TokenUnitTime unitTime = TokenUnitTime.weeks, int unitTimeVal = 1)
        {
            return await GenerateToken(_userData, unitTime, unitTimeVal);
        }

        /// <summary>
        /// Delete user token by id
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<UsersToken>> DeleteUserToken(int id)
        {
            var usertoken = _IUsersToken.DeleteUsersToken(id);
            return await Task.FromResult(usertoken);
        } 
        
        /// <summary>
        /// Reset auto increment of user token by id
        /// </summary>
        [HttpPost("{id}")]
        [Authorize]
        public async Task<string> ResetAIUserToken(int id = 0)
        {
           return await Task.FromResult(_IUsersToken.ResetAI(id));
        }

        private DateTime GenDateExpOnly(TokenUnitTime unitTime = TokenUnitTime.months, int unitTimeVal = 1)
        {
            return unitTime.ToString() switch
            {
                "miliseconds" => DateTime.UtcNow.AddMilliseconds(unitTimeVal),
                "seconds" => DateTime.UtcNow.AddSeconds(unitTimeVal),
                "minutes" => DateTime.UtcNow.AddMinutes(unitTimeVal),
                "hours" => DateTime.UtcNow.AddHours(unitTimeVal),
                "days" => DateTime.UtcNow.AddDays(unitTimeVal),
                "weeks" => DateTime.UtcNow.AddDays(unitTimeVal * 7),
                "months" => DateTime.UtcNow.AddMonths(unitTimeVal),
                "years" => DateTime.UtcNow.AddYears(unitTimeVal),
                _ => DateTime.UtcNow.AddDays(unitTimeVal)
            };
        }

        private string GenTokenOnly(string username, UserRole role = UserRole.user, TokenUnitTime unitTime = TokenUnitTime.weeks, int unitTimeVal = 1)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", "" + Guid.NewGuid()),
                new Claim("Username", "" + username),
                new Claim("Displayname", "" + username),
                new Claim("Email", "" + username + "@localhost.loc"),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var dateExp = GenDateExpOnly(unitTime, unitTimeVal);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: dateExp,
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UsersTokenInfo> GenerateToken(UsersTokenLogin _userData, TokenUnitTime unitTime = TokenUnitTime.weeks, int unitTimeVal = 1)
        {
            if (_userData != null && _userData.Username != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Username, _userData.Password);

                if (user != null)
                {
                    var enableinfoclaims = true;
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", "" + user.Id),
                        new Claim("Displayname", "" + user.Displayname),
                        new Claim("Username", "" + user.Username),
                        new Claim("Email", "" + user.Email),
                        new Claim(ClaimTypes.Role, "" + user.Role)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var dateExp = unitTime.ToString() switch
                    {
                        "miliseconds" => DateTime.UtcNow.AddMilliseconds(unitTimeVal),
                        "seconds" => DateTime.UtcNow.AddSeconds(unitTimeVal),
                        "minutes" => DateTime.UtcNow.AddMinutes(unitTimeVal),
                        "hours" => DateTime.UtcNow.AddHours(unitTimeVal),
                        "days" => DateTime.UtcNow.AddDays(unitTimeVal),
                        "weeks" => DateTime.UtcNow.AddDays(unitTimeVal * 7),
                        "months" => DateTime.UtcNow.AddMonths(unitTimeVal),
                        "years" => DateTime.UtcNow.AddYears(unitTimeVal),
                        _ => DateTime.UtcNow.AddDays(unitTimeVal)
                    };

                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: dateExp,
                        signingCredentials: signIn);

                    if (_IUsersToken.GetUsersToken()?.Where(x => x.Username == user.Username).Count() == 1)
                    {
                        _IUsersToken.DeleteUsersToken(user.Id);
                    }

                    _IUsersToken.AddUsersToken(new UsersToken
                    {
                        Username = user.Username,
                        Email = user.Email,
                        Displayname = user.Displayname,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        DateExp = dateExp.ToString(),
                        DateCreated = DateTime.UtcNow,
                        UsersId = user.Id
                    });

                    return new UsersTokenInfo
                    {
                        Date = dateExp,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        ClaimsInfo = enableinfoclaims ? JsonConvert.SerializeObject(claims) : null,
                        IsValid = true,
                        Username = user.Username,
                        Userid = user.Id,
                        Msg = "Succesfully generated token for user " + user.Username
                    };
                }
                else
                {
                    return new UsersTokenInfo
                    {
                        Msg = "Invalid credentials"
                    };
                }
            }
            else
            {
                return new UsersTokenInfo
                {
                    Msg = "Invalid credentials"
                };
            }
        }

        private UsersTokenInfo GetDateExpByUserId(int id = 1)
        {
            var lstuserstoken = _IUsersToken.GetUsersToken()?.FirstOrDefault(x => x.UsersId == id);
            return new UsersTokenInfo
            {
                Date = lstuserstoken != null ? Convert.ToDateTime(lstuserstoken?.DateExp?.ToString()) : DateTime.UtcNow.AddDays(7),
                Msg = "Succesfully fetched the date expiration of token of user " + id
            };
        }

        private async Task<UsersTokenInfo> GetToken(List<Users> DefUsers, int id = 1, TokenUnitTime tkunit = TokenUnitTime.months, int tkval = 1)
        {
            var uname = DefUsers.Where(x => x.Id == id).Select(x => x.Username).First();
            var upass = DefUsers.Where(x => x.Id == id).Select(x => x.Password).First();
            var objlog = new UsersTokenLogin { Username = uname, Password = upass };

            return await GenerateToken(objlog, tkunit, tkval);
        }

        private async Task<ActionResult<UsersTokenInfo>> CheckIfItemTokenIsValid(string token)
        {
            var lsttokens = _IUsersToken.GetUsersToken()?.Where(x => x.Token.Contains(token) || x.DateExp == DateTime.UtcNow.ToString()).FirstOrDefault();

            return await Task.FromResult(new UsersTokenInfo
            {
                Token = token,
                Date = lsttokens != null ? Convert.ToDateTime(lsttokens.DateExp?.ToString()) : DateTime.UtcNow,
                ClaimsInfo = null,
                IsValid = lsttokens != null ? true : false,
                Username = lsttokens != null ? lsttokens.Username : "",
                Userid = lsttokens != null ? lsttokens.UsersId : -1,
                Msg = lsttokens != null ? "Token is valid" : "Token is invalid"
            });
        }

        private async Task<Users> GetUser(string uname, string password)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Username == uname && u.Password == password);
        }
    }
}
