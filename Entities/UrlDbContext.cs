using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Entities
{
    public class UrlDbContext : DbContext
    {
        public DbSet<UrlShorter> UrlShorteners { get; set; }
        public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
