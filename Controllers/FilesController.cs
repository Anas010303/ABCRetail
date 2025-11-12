using Microsoft.AspNetCore.Mvc;
using ABC_Retail2.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ABC_Retail2.Controllers
{

    public class FilesController : Controller
    {
        private readonly FileShareService _fileService;

        public FilesController(FileShareService fileService)
        {
            _fileService = fileService;
        }

        // GET: Show list of files
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var files = await _fileService.ListFilesAsync();
            return View(files);
        }

        // GET: If someone types /Files/Upload directly, redirect to Index
        [HttpGet]
        public IActionResult Upload()
        {
            return RedirectToAction(nameof(Index));
        }

        // POST: Upload a file to Azure Files
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                await _fileService.UploadFileAsync(file);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Delete a file
        [HttpGet]
        public async Task<IActionResult> Delete(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                await _fileService.DeleteFileAsync(fileName);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
