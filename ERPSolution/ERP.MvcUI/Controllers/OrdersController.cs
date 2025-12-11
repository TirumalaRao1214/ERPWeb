using ERP.MvcUI.Models;
using ERP.MvcUI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ERP.MvcUI.Controllers
{
    public class OrdersController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly ApiClient _api;

        public OrdersController(ApiClient api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var client = _api.CreateClient();

            // ✔ Read JWT from cookie authentication claims
            var token = User.FindFirst("token")?.Value;

            if (token is not null)
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            // 🔥 Call SALES API instead of Inventory API
            var sales = await client.GetFromJsonAsync<List<Order>>(
                "http://localhost:5113/sales/api/Sales");

            return View(sales);
        }

        public async Task<IActionResult> Create()
        {
            var client = _api.CreateClient();

            // load products for dropdown
            var products = await client.GetFromJsonAsync<List<Product>>(
                "http://localhost:5113/inventory/api/Product");

            var vm = new CreateOrderViewModel();
            vm.Items.Add(new SalesItemViewModel());
            vm.Products = products;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            var client = _api.CreateClient();

            // attach token
            var token = User.FindFirst("token")?.Value;
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            // Only send items array (Sales API expects this format)
            var body = new
            {
                items = model.Items.Select(x => new
                {
                    productId = x.ProductId,
                    qty = x.Qty,
                    price = x.Price
                }).ToList()
            };

            var response = await client.PostAsJsonAsync(
                "http://localhost:5113/sales/api/Sales",
                body);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = "Failed to create sales order!";
            return View(model);
        }

    }

   
}
