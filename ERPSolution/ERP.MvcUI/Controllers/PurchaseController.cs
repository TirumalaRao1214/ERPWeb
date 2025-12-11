using ERP.MvcUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

public class PurchaseController : Controller
{
    private readonly HttpClient _client;

    public PurchaseController(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient();
        _client.BaseAddress = new Uri("http://localhost:5113/");
    }

    // ---------------------------------------------------------
    // LOAD PURCHASE LIST
    // ---------------------------------------------------------
    public async Task<IActionResult> Index()
    {
        // ✔ Attach JWT token
        var token = User.FindFirst("token")?.Value;
        if (!string.IsNullOrEmpty(token))
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await _client.GetAsync("purchase/api/Purchase");

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Failed to load purchases";
            return View(new List<PurchaseListItem>());
        }

        string json = await response.Content.ReadAsStringAsync();
        var purchases = JsonConvert.DeserializeObject<List<PurchaseListItem>>(json);

        return View(purchases);
    }

    // ---------------------------------------------------------
    // SHOW CREATE FORM
    // ---------------------------------------------------------
    public IActionResult Create()
    {
        var vm = new PurchaseViewModel();
        vm.Items.Add(new PurchaseItemViewModel()); // Default empty row

        return View(vm);
    }

    // ---------------------------------------------------------
    // POST PURCHASE
    // ---------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> Create(PurchaseViewModel model)
    {
        // ✔ Attach JWT token
        var token = User.FindFirst("token")?.Value;
        if (!string.IsNullOrEmpty(token))
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        string json = JsonConvert.SerializeObject(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("purchase/api/Purchase", content);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        ViewBag.Error = "Purchase creation failed!";
        return View(model);
    }
}
