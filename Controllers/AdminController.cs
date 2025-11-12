using ABC_Retail2.Data;
using Microsoft.AspNetCore.Mvc;

namespace ABC_Retail2.Controllers
{
    public class AdminController : Controller
    {
        private readonly ABCRetail2DbContext _context;

        public AdminController(ABCRetail2DbContext context)
        {
            _context = context;
        }

        public IActionResult AllOrders()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(int orderId, string status)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = status;
                _context.SaveChanges();
            }
            return RedirectToAction("AllOrders");
        }
    }
}
