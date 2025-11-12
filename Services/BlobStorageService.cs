using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABC_Retail2.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobStorageService(string connectionString, string containerName)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Create container if it doesn't exist (private)
            _containerClient.CreateIfNotExists();
        }

        // Upload a file
        public async Task UploadBlobAsync(IFormFile file)
        {
            var blobClient = _containerClient.GetBlobClient(file.FileName);
            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        // List all blob names
        public async Task<List<string>> ListBlobsAsync()
        {
            var blobs = new List<string>();
            await foreach (var blob in _containerClient.GetBlobsAsync())
            {
                blobs.Add(blob.Name);
            }
            return blobs;
        }

        // Delete a blob
        public async Task DeleteBlobAsync(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        // Generate SAS URL for a blob (validMinutes default 60)
        public string GetBlobSasUri(string blobName, int validMinutes = 60)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);

            if (!blobClient.CanGenerateSasUri)
            {
                throw new InvalidOperationException("BlobClient cannot generate SAS. Ensure connection string includes account key.");
            }

            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = _containerClient.Name,
                BlobName = blobName,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(validMinutes)
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            return blobClient.GenerateSasUri(sasBuilder).ToString();
        }
    }
}
