using Microsoft.EntityFrameworkCore;
using URLShortenerApp.Models;

namespace URLShortenerApp.Data
{
    public class UrlShortenerContext : DbContext
    {
        public UrlShortenerContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ShortUrlModel> ShortUrls { get; set; }
        public DbSet<UserMasterModel> UserMasterModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UrlShortenerContext).Assembly);
        }
    }
}
