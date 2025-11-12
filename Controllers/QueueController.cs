using Microsoft.AspNetCore.Mvc;
using ABC_Retail2.Services;
using System.Threading.Tasks;

namespace ABC_Retail2.Controllers
{
    public class QueueController : Controller
    {
        private readonly QueueStorageService _queueService;

        public QueueController(QueueStorageService queueService)
        {
            _queueService = queueService;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await _queueService.PeekMessagesAsync();
            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
                await _queueService.SendMessageAsync(message);

            return RedirectToAction("Index");
        }
    }
}
