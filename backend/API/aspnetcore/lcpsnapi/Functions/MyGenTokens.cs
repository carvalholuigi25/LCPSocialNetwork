using static lcpsnapi.Classes.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace lcpsnapi.Functions
{
    public partial class MyGenTokens
    {
        #nullable disable

        public static IConfiguration _configuration;

        public MyGenTokens(IConfiguration config)
        {
            _configuration = config;
        }

        public static DateTime GenDateExpOnly(TokenUnitTime unitTime = TokenUnitTime.months, int unitTimeVal = 1)
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

        public static string GenTokenOnly(string username, UserRole role = UserRole.user, TokenUnitTime unitTime = TokenUnitTime.weeks, int unitTimeVal = 1)
        {
            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub, "localhost"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Name, "" + username),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6ImxvY2FsaG9zdCIsImlhdCI6MTUxNjIzOTAyMn0"));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var dateExp = GenDateExpOnly(unitTime, unitTimeVal);

            var token = new JwtSecurityToken(
                "localhost",
                "localhost",
                claims,
                expires: dateExp,
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
