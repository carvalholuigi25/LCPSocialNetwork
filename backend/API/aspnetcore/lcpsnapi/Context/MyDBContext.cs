using Microsoft.EntityFrameworkCore;
using lcpsnapi.Classes;

namespace lcpsnapi.Context
{
    public class MyDBContext : DbContext
    {
        private readonly IConfiguration configuration;

        public MyDBContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Users>? User { get; set; }
        public DbSet<UsersToken>? UserToken { get; set; }
        public DbSet<Posts>? Post { get; set; }
        public DbSet<Attachments>? Attachment { get; set; }
        public DbSet<Media>? Media { get; set; }
        public DbSet<Achievements>? Achievements { get; set; }
        public DbSet<Todo>? Todo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            if (!ob.IsConfigured)
            {
                ob.UseSqlServer(this.configuration.GetConnectionString("MyDBConn"));
            }
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            new SeedData(mb).Seed(true);
        }
    }
}
