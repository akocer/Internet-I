using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace uyg04_CookieAuth.Controllers
{
    // [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
