using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Net;
using WebAPIPractice.Models.Domain;
using System.Web;
using WebAPIPractice.Repositories;
using Azure.Storage.Blobs.Models;
using WebAPIPractice.Command;
using MediatR;

namespace WebAPIPractice.Controllers
{
    [EnableCors]
    [Route("[controller]/[Action]")]
    [ApiController]
    public class UploadController : Controller
    {
        private readonly IFileService fileService;

        public IMediator Mediator { get; }

        public UploadController(IFileService fileService, IMediator mediator)
        {
            this.fileService = fileService;
            Mediator = mediator;
        }
        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(List<IFormFile> formFiles, string subDirectory)
        {
            try
            {
                //fileService.UploadFiles(formFiles, subDirectory);
                var model = new UploadFileCommand(formFiles, subDirectory);
                await Mediator.Send(model);
                return Ok(new { formFiles.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[AllowAnonymous]
        //public async Task<HttpResponseMessage> PostUserImage()
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    try
        //    {

        //        var httpRequest = HttpContext.Request;

        //        foreach (string file in httpRequest.)
        //        {
        //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

        //            var postedFile = httpRequest.Files[file];
        //            if (postedFile != null && postedFile.ContentLength > 0)
        //            {

        //                int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

        //                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        //                var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
        //                var extension = ext.ToLower();
        //                if (!AllowedFileExtensions.Contains(extension))
        //                {

        //                    var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else if (postedFile.ContentLength > MaxContentLength)
        //                {

        //                    var message = string.Format("Please Upload a file upto 1 mb.");

        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else
        //                {



        //                    var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension);

        //                    postedFile.SaveAs(filePath);

        //                }
        //            }

        //            var message1 = string.Format("Image Updated Successfully.");
        //            return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
        //        }
        //        var res = string.Format("Please Upload a image.");
        //        dict.Add("error", res);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        //    }
        //    catch (Exception ex)
        //    {
        //        var res = string.Format("some Message");
        //        dict.Add("error", res);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        //    }
        //}
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
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> uploadImage(List<IFormFile> formFiles, string subDirectory)
        {
            if(formFiles.Count > 0) 
            {
                foreach (IFormFile formFile in formFiles)
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
                        catch (Exception ex) { }
                        var result = fileUrl;
                        return Ok(result);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest();
                    }
                }
            }
            return BadRequest();

        }
    }
}
