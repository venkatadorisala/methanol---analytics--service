using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using JM.Integration.Common.Interfaces;
using Microsoft.Extensions.Azure;
using Newtonsoft.Json;

namespace JM.Integration.Methanol.Services.Services
{
    /// <inheritdoc cref="IBlobService"/>
    public class MethanolBlobService : IBlobService
    {
     
        private BlobServiceClient _gen1StorageClient, _gen2StorageClient;
        public MethanolBlobService(BlobServiceClient defaultClient, IAzureClientFactory<BlobServiceClient> clientFactory)
        {
            _gen1StorageClient = defaultClient;
            if(clientFactory!=null)
            _gen2StorageClient = clientFactory.CreateClient("Gen2DataLakeConnectionString");
        }

             public async Task<bool> UploadBlob(string name, System.IO.Stream fileStream, string containerName)
        {
            var containerClient = _gen2StorageClient.GetBlobContainerClient(containerName);

            // checking if the file exist 
            // if the file exist it will be replaced
            // if it doesn't exist it will create a temp space until its uploaded
            var blobClient = containerClient.GetBlobClient(name);
            var res = await blobClient.UploadAsync(fileStream, overwrite: true).ConfigureAwait(true);
            if (res != null)
                return true;

            return false;
        }

        public Stream GetBlobStream(string blobName)
        {
           //Read blob from Av Check container and return io stream.
            BlobClient blob = _gen1StorageClient.GetBlobContainerClient(Environment.GetEnvironmentVariable("AvCheckBlobContainer")).GetBlobClient(blobName);
            var contents = blob.OpenReadAsync().Result;
            return contents;
        }

        public bool DeleteBlob(string blobName)
        {
            BlobClient blob = _gen1StorageClient.GetBlobContainerClient(Environment.GetEnvironmentVariable("AvCheckBlobContainer")).GetBlobClient(blobName);
           return blob.DeleteIfExists();
        }

        public string GetBlobNameFromQueue(string queueMessage)
        {
            dynamic blobObject = JsonConvert.DeserializeObject<dynamic>(queueMessage);
            string blobUrl = blobObject?.data?.url;
            string[] pathSplit = blobUrl.Split("/");
            string blobName = pathSplit[(pathSplit.Length - 1)];
            return blobName;
        }


    }
}
