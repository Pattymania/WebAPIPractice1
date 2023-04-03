using Azure.Storage.Blobs;
using Microsoft.Extensions.Hosting.Internal;
using WebAPIPractice.Data;
using WebAPIPractice.Models.Domain;
using WebAPIPractice.Controllers;

namespace WebAPIPractice.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly WebAPIDbContext webAPIDbContext;
        private readonly IWebHostEnvironment hostingEnvironment;


        public DataAccess(WebAPIDbContext webAPIDbContext, IWebHostEnvironment hostingEnvironment)
        {
            this.webAPIDbContext = webAPIDbContext;
            this.hostingEnvironment = hostingEnvironment;
        }
        public Region AddRegion(Region region)
        {
            region.Id = Guid.NewGuid();
            webAPIDbContext.Regions.Add(region);
            webAPIDbContext.SaveChanges();
            return region;
        }

        public Region DeleteRegion(Guid Id)
        {
            var region = webAPIDbContext.Regions.Find(Id);
            if (region == null)
            {
                return null;
            }
            webAPIDbContext.Regions.Remove(region);
            webAPIDbContext.SaveChanges();
            return region;
        }

        public Region GetRegion(Guid Id)
        {
            var region = webAPIDbContext.Regions.Where(i => i.Id == Id).FirstOrDefault();
            if (region == null)
            {
                return null;
            }
            return region;
        }

        public List<Region> GetRegions()
        {
            return webAPIDbContext.Regions.ToList();
        }

        public Region UpdateRegion(Guid Id, Region region)
        {
            var existingRegion = webAPIDbContext.Regions.Find(Id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Area = region.Area;
            existingRegion.Name = region.Name;
            existingRegion.Population = region.Population;
            existingRegion.Code = region.Code;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;

            webAPIDbContext.SaveChanges();
            return existingRegion;
        }
        private string GenerateFileName(string fileName, string CustomerName)
        {
            try
            {
                string strFileName = string.Empty;
                string[] strName = fileName.Split('.');
                strFileName = CustomerName + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/"
                   + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." +
                   strName[strName.Length - 1];
                return strFileName;
            }
            catch (Exception ex)
            {
                return fileName;
            }
        }
        public bool UploadFile(List<IFormFile> files, string subDirectory)
        {
            //upload to system directory

            //subDirectory = subDirectory ?? string.Empty;
            //var target = Path.Combine(hostingEnvironment.ContentRootPath, subDirectory);

            //Directory.CreateDirectory(target);

            //files.ForEach(async file =>
            //{
            //    if (file.Length <= 0) return;
            //    var filePath = Path.Combine(target, file.FileName);
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await file.CopyToAsync(stream);
            //    }
            //});
            //return true;

            // upload to Azure Portal 

            if (files.Count > 0)
            {
                foreach (IFormFile formFile in files)
                {
                    try
                    {
                        var filename = GenerateFileName(formFile.FileName, subDirectory);
                        var fileUrl = "";
                        string connectionstring = "DefaultEndpointsProtocol=https;AccountName=teststoragewebapipractis;AccountKey=VdWqhX6FMaMDxbEtoooI5913/2wRa9mFFW7HkVzYjAtuGC6ps1zXy/v93aod5WfK+A0OPzEp+1hs+ASt5Db49g==;EndpointSuffix=core.windows.net";
                        string key = "demofileupload";
                        BlobContainerClient container = new BlobContainerClient(connectionstring, key);
                        try
                        {
                            BlobClient blob = container.GetBlobClient(filename);
                            using (Stream stream = formFile.OpenReadStream())
                            {
                                blob.Upload(stream);
                            }
                            fileUrl = blob.Uri.AbsoluteUri;
                        }
                        catch (Exception ex) { return false; }
                        var result = fileUrl;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
