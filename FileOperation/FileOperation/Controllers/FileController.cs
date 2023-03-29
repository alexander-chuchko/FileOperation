using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileOperation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FileOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;
        public FileController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient; 
        }

        [HttpPost]
        public async Task<ActionResult> Create()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("container1");
             
            var blobClient = containerClient.GetBlobClient("FileName");
            //await blobClient.UploadAsync();
            return Ok();
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<IActionResult> AddAsync() 
        {
            var formCollection = await Request.ReadFormAsync();
            var files = formCollection.Files;
            foreach (var file in files)
            {
                var blobContainerClient = new BlobContainerClient("UseDevelopmentStorage=true", "images");
                blobContainerClient.CreateIfNotExists();
                var containerClient = blobContainerClient.GetBlobClient(file.FileName);
                var blobHttpHeader = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                };
                await containerClient.UploadAsync(file.OpenReadStream(), blobHttpHeader);
            }

            return Ok();
        }

        [HttpPost]
        [Route("createfile")]
        public async Task<IActionResult> CreateFile([FromForm] DataUser dataUser)
        {
            if (ModelState.IsValid)
            {
                var tempProfile = dataUser;
                return Ok();
            }

            return BadRequest();
        }
    }
}
