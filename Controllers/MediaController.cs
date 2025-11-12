using Microsoft.AspNetCore.Mvc;
using ABC_Retail2.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABC_Retail2.Controllers
{
    public class MediaController : Controller
    {
        private readonly BlobStorageService _blobService;

        public MediaController(BlobStorageService blobService)
        {
            _blobService = blobService;
        }

        // GET: Display all blobs with SAS URLs
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blobNames = await _blobService.ListBlobsAsync();

            var blobUrls = new List<string>();
            foreach (var name in blobNames)
            {
                blobUrls.Add(_blobService.GetBlobSasUri(name));
            }

            return View(blobUrls);
        }

        // GET: If someone types /Media/Upload, redirect to Index
        [HttpGet]
        public IActionResult Upload()
        {
            return RedirectToAction(nameof(Index));
        }

        // POST: Upload a file
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                await _blobService.UploadBlobAsync(file);
            }

            // Redirect back to Index to show updated list
            return RedirectToAction(nameof(Index));
        }

        // GET: Delete a blob
        [HttpGet]
        public async Task<IActionResult> Delete(string blobName)
        {
            if (!string.IsNullOrEmpty(blobName))
            {
                await _blobService.DeleteBlobAsync(blobName);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
