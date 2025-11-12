using Microsoft.AspNetCore.Mvc;
using ABC_Retail2.Models;
using ABC_Retail2.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ABC_Retail2.Controllers
{
    public class CustomersController : Controller
    {
        private readonly TableStorageService _tableService;

        public CustomersController(TableStorageService tableService)
        {
            _tableService = tableService;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _tableService.GetAllCustomersAsync();
            return View(customers);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CustomerEntity customer)
        {
            if (!ModelState.IsValid) return View(customer);

            customer.RowKey = Guid.NewGuid().ToString();
            customer.PartitionKey = "Customers";

            await _tableService.AddOrUpdateCustomerAsync(customer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var customers = await _tableService.GetAllCustomersAsync();
            var customer = customers.FirstOrDefault(c => c.RowKey == id);
            return customer == null ? NotFound() : View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CustomerEntity updatedCustomer)
        {
            if (!ModelState.IsValid) return View(updatedCustomer);

            updatedCustomer.PartitionKey = "Customers";
            updatedCustomer.RowKey = id;

            await _tableService.AddOrUpdateCustomerAsync(updatedCustomer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            await _tableService.DeleteCustomerAsync(id);
            return RedirectToAction("Index");
        }
    }
}
