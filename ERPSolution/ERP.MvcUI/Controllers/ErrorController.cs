using Microsoft.AspNetCore.Mvc;

namespace ERP.MvcUI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/ContactAdmin")]
        public IActionResult ContactAdmin()
        {
            return View("~/Views/Shared/ContactAdmin.cshtml");
        }

        [Route("Error/Status")]
        public IActionResult Status(int code)
        {
            return RedirectToAction("ContactAdmin");
        }
    }
}
