using Microsoft.AspNetCore.Hosting;
using Microsoft.Identity.Client;

namespace WebAPIPractice.Repositories
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public FileService(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public void UploadFiles(List<IFormFile> files, string subDirectory)
        {
            subDirectory = subDirectory ?? string.Empty;
            var target = Path.Combine(hostingEnvironment.ContentRootPath, subDirectory);

            Directory.CreateDirectory(target);

            files.ForEach(async file =>
            {
                if (file.Length <= 0) return;
                var filePath = Path.Combine(target, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            });
        }
    }
}
