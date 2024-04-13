using LCPSNWebApi.Classes;
using LCPSNWebApi.Classes.Files;
using LCPSNWebApi.Context.Conventions;
using LCPSNWebApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LCPSNWebApi.Context;

public class DBContext : DbContext
{
    private readonly IConfiguration _config;
    private readonly IHostEnvironment _environment;
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<FileData> FilesData { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<Reply> Replies { get; set; }
    public DbSet<Share> Shares { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }

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
        base.OnModelCreating(modelBuilder);
        new DBSeedData(modelBuilder).Seed(true);
    }
}