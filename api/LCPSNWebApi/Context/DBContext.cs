using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Files;
using LCPSNWebApi.Context.Conventions;
using LCPSNWebApi.Extensions;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace LCPSNWebApi.Context;

public class DBContext : DbContext
{
    private readonly IConfiguration _config;
    private readonly IHostEnvironment _environment;
    public DbSet<User> Users { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<FileData> FilesData { get; set; }

    public DBContext(IConfiguration config, IHostEnvironment environment) : base()
    {
        _config = config;
        _environment = environment;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (_environment.IsDevelopment())
        {
            options.UseSqlServer(_config["ConnectionStrings:SQLServer"])
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        else
        {
            options.UseSqlServer(_config["ConnectionStrings:SQLServer"]);
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyUtcDateTimeConverter();
        modelBuilder.Entity<User>().HasData(new User
        {
            UserId = 1,
            Username = "admin",
            Password = BC.HashPassword("admin2024", BC.GenerateSalt(12), false, BCrypt.Net.HashType.SHA256),
            FirstName = "Luis",
            LastName = "Carvalho",
            AvatarUrl = "images/users/avatars/luis.jpg",
            CoverUrl = "images/users/covers/luis_cover.jpg",
            Role = UserRoles.Administrator.ToString(),
            DateAccountCreated = DateTime.UtcNow,
            CurrentToken = null,
            RefreshToken = null,
            RefreshTokenExpiryTime = DateTime.UtcNow
        });
        base.OnModelCreating(modelBuilder);
    }
}