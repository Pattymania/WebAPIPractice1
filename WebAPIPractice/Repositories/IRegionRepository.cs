using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
