using Microsoft.AspNetCore.Mvc;
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

        public async Task<Region> GetAsync(Guid Id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(i => i.Id == Id);
            return region;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid Id)
        {
            var region = await dbContext.Regions.FindAsync(Id);
            if(region == null)
            {
                return null;
            }

            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid Id, Region region)
        {
            var existRegion = await dbContext.Regions.FindAsync(Id);
            if(existRegion == null)
            {
                return null;
            }
            existRegion.Code = region.Code;
            existRegion.Name = region.Name;
            existRegion.Area = region.Area;
            existRegion.Lat = region.Lat;
            existRegion.Long = region.Long;
            existRegion.Population = region.Population;

            await dbContext.SaveChangesAsync();

            return existRegion;
        }
    }
}
