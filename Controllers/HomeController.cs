using System.Collections.Generic;
using System.IO;

using Azure.Storage.Blobs;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorage.Controllers
{
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(List<IFormFile> postedFiles)
        {
            const string connString = "DefaultEndpointsProtocol=https;AccountName=cosmodbfileupload;AccountKey=VBolHbSAJ1JBdg7cUjadGGArFv0itgX8YwK2dHu721dGNtGbBfMgvzr3KxA0FUGCQQxbOo75OlAfh4+DIUfHmg==;EndpointSuffix=core.windows.net";
            BlobStorageService blobStorageService = new BlobStorageService(connString);
            foreach (IFormFile postedFile in postedFiles)
            {
                byte[] fileData;
                using (var target = new MemoryStream())
                {
                    postedFile.CopyTo(target);
                    fileData = target.ToArray();
                }
            string fileStorageUrl = blobStorageService.UploadFileToBlob(postedFile.FileName, fileData, postedFile.ContentType);
            }
            return View();
        }
         
    }
}
