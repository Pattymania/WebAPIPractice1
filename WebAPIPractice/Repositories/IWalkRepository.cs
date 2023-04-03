using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetAsync(Guid Id);
        Task<Walk> AddAsync(Walk item);
        Task<Walk> UpdateAsync(Guid Id, Walk item);
        Task<Walk> DeleteAsync(Guid Id);
    }
}
