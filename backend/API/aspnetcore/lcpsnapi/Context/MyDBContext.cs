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
        public DbSet<Games>? Game { get; set; }
        public DbSet<Movies>? Movie { get; set; }
        public DbSet<TVSeries>? TVSerie { get; set; }
        public DbSet<Animes>? Anime { get; set; }
        public DbSet<Mangas>? Manga { get; set; }
        public DbSet<ComicBooks>? ComicBook { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            if (!ob.IsConfigured)
            {
                #nullable disable
                ob.UseSqlServer(this.configuration.GetConnectionString("MyDBConn"));
                #nullable enable
            }
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            new SeedData(mb).Seed(true);
        }
    }
}
