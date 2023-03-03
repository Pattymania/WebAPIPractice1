using Microsoft.EntityFrameworkCore;
using WebAPIPractice.Data;
using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WebAPIDbContext dbContext;
        public RegionRepository(WebAPIDbContext webAPIDbContext)
        {
            this.dbContext = webAPIDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }
    }
}
