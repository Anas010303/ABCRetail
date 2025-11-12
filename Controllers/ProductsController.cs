using Microsoft.AspNetCore.Mvc;
using ABC_Retail2.Models;
using ABC_Retail2.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ABC_Retail2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly TableStorageService _tableService;
        private const string ProductTable = "Products";

        public ProductsController(TableStorageService tableService)
        {
            _tableService = tableService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _tableService.GetEntitiesAsync<ProductEntity>(ProductTable);
            return View(products);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ProductEntity product)
        {
            if (!ModelState.IsValid) return View(product);

            product.RowKey = Guid.NewGuid().ToString();
            product.PartitionKey = "Products";

            await _tableService.AddEntityAsync(ProductTable, product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            var products = await _tableService.GetEntitiesAsync<ProductEntity>(ProductTable);
            var product = products.FirstOrDefault(p => p.RowKey == id);
            return product == null ? NotFound() : View(product);
        }
    }
}
