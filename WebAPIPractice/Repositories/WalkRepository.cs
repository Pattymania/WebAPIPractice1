using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIPractice.Data;
using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WebAPIDbContext dbContext;
        public WalkRepository(WebAPIDbContext webAPIDbContext)
        {
            this.dbContext = webAPIDbContext;
        }

        public async Task<Walk> AddAsync(Walk item)
        {
            item.Id = Guid.NewGuid();
            await dbContext.Walks.AddAsync(item);
            await dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Walk> DeleteAsync(Guid Id)
        {
            var walk = await dbContext.Walks.FindAsync(Id);
            if(walk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return 
                await dbContext.Walks
                .Include(i => i.Region)
                .Include(i => i.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid Id)
        {
            var walk = await dbContext.Walks.Include(i => i.Region).Include(i => i.WalkDifficulty).FirstOrDefaultAsync(i => i.Id == Id);
            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid Id, Walk item)
        {
            var existingWalk = await dbContext.Walks.FindAsync(Id);
            if (existingWalk == null) {
                return null;
            }

            //existingWalk.Id = item.Id;
            existingWalk.Name = item.Name;
            existingWalk.Length = item.Length;
            existingWalk.RegionID = item.RegionID;
            existingWalk.WalkDifficultyId = item.WalkDifficultyId;
            await dbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
