using Microsoft.AspNetCore.Mvc;
using ABC_Retail2.Data;
using ABC_Retail2.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using System;

namespace ABC_Retail2.Controllers
{
    
    public class CartController : Controller
    {
        private readonly ABCRetail2DbContext _context;

        public CartController(ABCRetail2DbContext context)
        {
            _context = context;
        }

        // ✅ VIEW CART
        [HttpGet]
        public IActionResult Index()
        {
            // Get the logged-in user ID from session
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login", "Account");

            // Convert to int safely
            if (!int.TryParse(userIdStr, out int userId))
                return RedirectToAction("Login", "Account");

            // Find pending order
            var order = _context.Orders
                .FirstOrDefault(o => o.CustomerId == userId && o.Status == "Pending");

            if (order == null)
                return View(new List<OrderItem>());

            // Get all items for that order
            var items = _context.OrderItems
                .Where(i => i.OrderId == order.OrderId)
                .ToList();

            return View(items);
        }

        // ✅ ADD ITEM TO CART
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login", "Account");

            if (!int.TryParse(userIdStr, out int userId))
                return RedirectToAction("Login", "Account");

            // Find or create pending order
            var order = _context.Orders
                .FirstOrDefault(o => o.CustomerId == userId && o.Status == "Pending");

            if (order == null)
            {
                order = new Order
                {
                    CustomerId = userId,
                    OrderDate = DateTime.Now,
                    Status = "Pending"
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }

            // Check if product already in cart
            var existingItem = _context.OrderItems
                .FirstOrDefault(i => i.OrderId == order.OrderId && i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.OrderItems.Add(newItem);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ✅ CONFIRM ORDER
        [HttpPost]
        public IActionResult ConfirmOrder()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login", "Account");

            if (!int.TryParse(userIdStr, out int userId))
                return RedirectToAction("Login", "Account");

            var order = _context.Orders
                .FirstOrDefault(o => o.CustomerId == userId && o.Status == "Pending");

            if (order != null)
            {
                order.Status = "Confirmed";
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
