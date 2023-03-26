using Azure.Storage.Blobs;
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
    }
}
