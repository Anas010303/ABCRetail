using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ABC_Retail2.Services
{
    public class FileShareService
    {
        private readonly ShareClient _shareClient;

        public FileShareService(string connectionString, string shareName)
        {
            _shareClient = new ShareClient(connectionString, shareName);
            _shareClient.CreateIfNotExists();
        }

        // ✅ Upload file
        public async Task UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return;

            var directoryClient = _shareClient.GetRootDirectoryClient();
            var fileClient = directoryClient.GetFileClient(file.FileName);

            // Delete if it exists (to "overwrite")
            await fileClient.DeleteIfExistsAsync();

            // Create the file with the correct size
            await fileClient.CreateAsync(file.Length);

            // Upload the file stream
            using (var stream = file.OpenReadStream())
            {
                await fileClient.UploadAsync(stream);
            }
        }


        // ✅ List files
        public async Task<List<string>> ListFilesAsync()
        {
            var files = new List<string>();
            var directoryClient = _shareClient.GetRootDirectoryClient();

            await foreach (ShareFileItem item in directoryClient.GetFilesAndDirectoriesAsync())
            {
                if (!item.IsDirectory)
                    files.Add(item.Name);
            }

            return files;
        }

        // ✅ Delete file
        public async Task DeleteFileAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            var directoryClient = _shareClient.GetRootDirectoryClient();
            var fileClient = directoryClient.GetFileClient(fileName);

            await fileClient.DeleteIfExistsAsync();
        }
    }
}
