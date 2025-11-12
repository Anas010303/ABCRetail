using ABC_Retail2.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ABC_Retail2.Controllers
{
    [AuthFilter]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
