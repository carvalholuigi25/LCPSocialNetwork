using LCPSNWebApi.Classes;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;

namespace LCPSNWebApi.Context;

public class DBSeedData
{
    private readonly ModelBuilder modelBuilder;

    public DBSeedData(ModelBuilder modelBuilder)
    {
        this.modelBuilder = modelBuilder;
    }

    public void Seed(bool isSeedDefaultData = true)
    {
        if (isSeedDefaultData)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    Password = BC.HashPassword("admin2024", BC.GenerateSalt(12), false, BCrypt.Net.HashType.SHA256),
                    Email = "luiscarvalho239@gmail.com",
                    FirstName = "Luis",
                    LastName = "Carvalho",
                    DateBirthday = new DateTime(1996, 6, 4, 0, 0, 0).ToUniversalTime(),
                    PhoneNumber = "123456789",
                    Role = UserRoles.Administrator.ToString(),
                    Status = "public",
                    Biography = "Hello, I'm Luis Carvalho.",
                    AvatarUrl = "images/users/avatars/luis.jpg",
                    CoverUrl = "images/users/covers/luis_cover.jpg",
                    DateAccountCreated = DateTime.UtcNow,
                    CurrentToken = null,
                    RefreshToken = null,
                    RefreshTokenExpiryTime = DateTime.UtcNow
                },
                new User
                {
                    UserId = 2,
                    Username = "guest",
                    Password = BC.HashPassword("guest2024", BC.GenerateSalt(12), false, BCrypt.Net.HashType.SHA256),
                    Email = "guest@localhost.loc",
                    FirstName = "Guest",
                    LastName = "Convidado",
                    DateBirthday = new DateTime(1996, 6, 4, 0, 0, 0).ToUniversalTime(),
                    PhoneNumber = "123456789",
                    Role = UserRoles.Guest.ToString(),
                    Status = "public",
                    Biography = "Hello, I'm Guest.",
                    AvatarUrl = "images/users/avatars/guest.png",
                    CoverUrl = "images/users/covers/guest_cover.jpeg",
                    DateAccountCreated = DateTime.UtcNow,
                    CurrentToken = null,
                    RefreshToken = null,
                    RefreshTokenExpiryTime = DateTime.UtcNow
                }
            );
        }
    }
}
