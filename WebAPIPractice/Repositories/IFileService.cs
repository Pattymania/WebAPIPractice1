namespace WebAPIPractice.Repositories
{
    public interface IFileService
    {
        void UploadFiles(List<IFormFile> files, string subDirectory);
    }
}
