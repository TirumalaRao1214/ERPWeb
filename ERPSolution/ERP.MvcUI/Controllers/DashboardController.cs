using ERP.MvcUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ERP.MvcUI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly HttpClient _http;

        public DashboardController(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient("GatewayClient");
        }

        public async Task<IActionResult> Index()
        {
            var inventory = await _http.GetFromJsonAsync<InventoryAnalyticsDto>("dashboard/inventory/analytics");
            var sales = await _http.GetFromJsonAsync<SalesAnalyticsDto>("dashboard/sales/analytics");
            var purchase = await _http.GetFromJsonAsync<PurchaseAnalyticsDto>("dashboard/purchase/analytics");

            return View(new DashboardViewModel
            {
                Inventory = inventory,
                Sales = sales,
                Purchase = purchase
            });
        }
    }
}
