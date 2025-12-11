using ERP.MvcUI.Models;
using ERP.MvcUI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ERP.MvcUI.Controllers
{
    public class InventoryController : Controller
    {

        private readonly ApiClient _api;

        public InventoryController(ApiClient api)
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

            var items = await client.GetFromJsonAsync<List<InventoryItem>>(
                "http://localhost:5113/inventory/api/Product");

            return View(items);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new InventoryItem());
        }

        [HttpPost]
        public async Task<IActionResult> Create(InventoryItem model)
        {
            var client = _api.CreateClient();

            //var token = HttpContext.Session.GetString("JWT");
            var token = User.FindFirst("token")?.Value;
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // 🔥 Correct endpoint for POST
            var response = await client.PostAsJsonAsync(
                "http://localhost:5113/inventory/api/Product", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Product creation failed.");
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}
