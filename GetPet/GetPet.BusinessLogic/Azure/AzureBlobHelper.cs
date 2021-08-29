using Azure.Storage.Blobs;
using GetPet.Common;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Azure
{
    public class AzureBlobHelper
    {
        private const string containerName = "upload-content";

        private readonly ImageHelper _imageHelper;

        public AzureBlobHelper(ImageHelper imageHelper)
        {
            _imageHelper = imageHelper;
        }

        private async Task<BlobContainerClient> GetBlobContainerClient()
        {
            BlobServiceClient blobServiceClient = new(Constants.AzureStorageConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            
            return containerClient;
        }

        public async Task<string> Upload(string fileName, Stream stream)
        {            
            var extension = fileName.Split(".").Last();
            var newFileName = $"{Guid.NewGuid()}.{extension}";

            var imageStream = _imageHelper.PrepareImage(stream);
            imageStream.Seek(0, SeekOrigin.Begin);

            var fileRelativePath = await UploadToContainer(newFileName, imageStream);

            return fileRelativePath;
        }

        public async Task<string> Upload(string url)
        {
            var stream = GetStream(url);

            var imageStream = _imageHelper.PrepareImage(stream);
            imageStream.Seek(0, SeekOrigin.Begin);

            var extension = url.Split(".").Last();
            var fileName = $"{Guid.NewGuid()}.{extension}";

            var fileRelativePath = await UploadToContainer(fileName, imageStream);

            return fileRelativePath;
        }

        private async Task<string> UploadToContainer(string fileName, Stream stream)
        {
            var containerClient = await GetBlobContainerClient();

            await containerClient.UploadBlobAsync(fileName, stream);

            return $"{containerName}/{fileName}";
        }

        private Stream GetStream(string url)
        {            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            return response.GetResponseStream();
        }
    }
}