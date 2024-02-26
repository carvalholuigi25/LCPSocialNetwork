namespace LCPSNWebApi.Classes.Auth;
public class AuthenticatedResponse
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public User? UsersInfo { get; set; }
}