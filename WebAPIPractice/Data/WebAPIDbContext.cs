using Microsoft.EntityFrameworkCore;
using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Data
{
    public class WebAPIDbContext:DbContext
    {
        public WebAPIDbContext(DbContextOptions<WebAPIDbContext> options): base(options)
        {
            
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}
