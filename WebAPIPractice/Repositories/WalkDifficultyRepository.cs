using Microsoft.EntityFrameworkCore;
using WebAPIPractice.Data;
using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly WebAPIDbContext dbContext;
        public WalkDifficultyRepository(WebAPIDbContext webAPIDbContext)
        {
            this.dbContext = webAPIDbContext;
        }
        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await dbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await dbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var difficultyRecord = await dbContext.WalkDifficulty.FindAsync(id);
            if (difficultyRecord != null)
            {
                dbContext.WalkDifficulty.Remove(difficultyRecord);
                await dbContext.SaveChangesAsync();
                return difficultyRecord;
            }
            return null;
        }

        public async Task<List<WalkDifficulty>> GetAllAsync()
        {
            return await dbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            var walkDifficultyRecord = await dbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            return walkDifficultyRecord;
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingDifficulty = await dbContext.WalkDifficulty.FindAsync(id);
            if (existingDifficulty != null)
            {
                existingDifficulty.Code = walkDifficulty.Code;
                await dbContext.SaveChangesAsync();
                return existingDifficulty;
            }
            return null;
        }
    }
}
