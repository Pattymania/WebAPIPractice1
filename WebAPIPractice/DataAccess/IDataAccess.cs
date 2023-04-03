using WebAPIPractice.Models.Domain;

namespace WebAPIPractice.DataAccess
{
    public interface IDataAccess
    {
        List<Region> GetRegions();
        Region AddRegion(Region region);
        Region GetRegion(Guid Id);
        Region DeleteRegion(Guid Id);
        Region UpdateRegion(Guid Id, Region region);
        bool UploadFile(List<IFormFile> files, string subDirectory);
    }
}
