using LCPSNWebApi.Context;
using LCPSNWebApi.Interfaces.Auth;
using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;

namespace LCPSNWebApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DBContext _dbContext;
        private readonly ITokenService _tokenService;
        
        public AuthController(DBContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        /// <summary>
        /// This will authenticate the user and generate his token for logged user with valid credientals.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] UserAuth loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid user credientals");
            }
            
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == loginModel.Username);
            
            if (user is null || !BC.Verify(loginModel.Password, user.Password, false, BCrypt.Net.HashType.SHA256)) return Unauthorized();
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Username!),
                new Claim(ClaimTypes.Role, user!.Role!)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.CurrentToken = accessToken;
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMonths(1);
            
            _dbContext.SaveChanges();

            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                UsersInfo = new User() {
                    UserId = user.UserId,
                    Username = user.Username,
                    Password = null!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role,
                    Status = user.Status,
                    Biography = user.Biography,
                    AvatarUrl = user.AvatarUrl,
                    CoverUrl = user.CoverUrl,
                    DateAccountCreated = user.DateAccountCreated,
                    CurrentToken = user.CurrentToken,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime                    
                }
            });
        }
    }
}