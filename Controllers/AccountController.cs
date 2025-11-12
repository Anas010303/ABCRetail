using Microsoft.AspNetCore.Mvc;
using ABC_Retail2.Data;
using ABC_Retail2.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace ABC_Retail2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ABCRetail2DbContext _context;

        public AccountController(ABCRetail2DbContext context)
        {
            _context = context;
        }

        // ✅ LOGIN PAGE
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ✅ LOGIN POST
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Please enter both email and password.";
                return View();
            }

            // Hash entered password
            var hashedPassword = Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(password))
            );

            // Check user in database
            var user = _context.Customers
                .FirstOrDefault(u => u.Email == email && u.PasswordHash == hashedPassword);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            // Save session
            HttpContext.Session.SetString("UserId", user.CustomerId.ToString());
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.FullName);

            // Redirect based on role
            if (user.Role == "Admin")
                return RedirectToAction("AllOrders", "Admin");
            else
                return RedirectToAction("Index", "Home");
        }

        // ✅ REGISTER PAGE
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // ✅ REGISTER POST
        [HttpPost]
        public IActionResult Register(Customer model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Hash the password
            var hash = Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(model.PasswordHash))
            );

            var existingUser = _context.Customers.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ViewBag.Error = "Email already registered.";
                return View();
            }

            var user = new Customer
            {
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = hash,
                Role = "Customer"
            };

            _context.Customers.Add(user);
            _context.SaveChanges();

            ViewBag.Message = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }

        // ✅ LOGOUT
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
