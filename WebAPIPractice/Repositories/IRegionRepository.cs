using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid Id);
        Task<Region> AddAsync(Region Region);
        Task<Region> DeleteAsync(Guid Id);
        Task<Region> UpdateAsync(Guid Id, Region region);

    }
}
