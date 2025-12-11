using ERP.MvcUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP.MvcUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _factory;

        public AccountController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsJsonAsync(
                "http://localhost:5207/api/Login/login", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Invalid login");
                return View(model);
            }

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Username),
            new Claim("token", result.Token)
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
