using Azure.Storage.Blobs;

namespace HeracleumSosnowskyiService.Services
{
    public class StorageService : IStorageService
    {
        public readonly BlobServiceClient _blobServiceClient;
        public readonly IConfiguration _configuration;

        public StorageService(BlobServiceClient blobServiceClient, IConfiguration configuration) 
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        public void Upload(IFormFile formFile)
        {
            var containerName = _configuration.GetSection("Storage:ContainerName").Value;

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(formFile.FileName);

            using var stream = formFile.OpenReadStream();
            blobClient.Upload(stream, true);
        }
    }
}
