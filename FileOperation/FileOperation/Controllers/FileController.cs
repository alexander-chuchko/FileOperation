using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileOperation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace FileOperation.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FileController : ControllerBase
    {
        //private readonly BlobServiceClient _blobServiceClient;
        //public FileController(BlobServiceClient blobServiceClient)
        public FileController()
        {
            //_blobServiceClient = blobServiceClient; 
        }
      

        [HttpPost, DisableRequestSizeLimit]
        [Route("createfile")]
        public async Task<IActionResult> CreateFile([FromForm] DataUser dataUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tempProfile = dataUser;
                    BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=storagealexchuchko;AccountKey=Udh+PMdPg0F8ZXvtyLzSogNFPSzf/o40WhnG/30QohOaA4pDq5Gay64Px4uA/xq4TMVlHlMaI6kt+AStAmU4HQ==;EndpointSuffix=core.windows.net");
                    BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient("container1");

                    blobContainerClient.CreateIfNotExists();
                    var containerClient = blobContainerClient.GetBlobClient(tempProfile.Picture.FileName);
                    var blobHttpHeader = new BlobHttpHeaders
                    {
                        ContentType = tempProfile.Picture.ContentType,
                    };
                    await containerClient.UploadAsync(tempProfile.Picture.OpenReadStream(), blobHttpHeader); 
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
               

                return Ok();
            }

            return BadRequest();
        }
    }
}
