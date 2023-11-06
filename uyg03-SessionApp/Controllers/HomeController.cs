using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using uyg03_SessionApp.Auth;
using uyg03_SessionApp.Models;
using uyg03_SessionApp.ViewModels;

namespace uyg03_SessionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly INotyfService _notify;
        public HomeController(ILogger<HomeController> logger, AppDbContext context, INotyfService notify)
        {
            _logger = logger;
            _context = context;
            _notify = notify;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (_context.Users.Count(s => s.UserName == model.UserName && s.Password == model.Password) > 0)
            {
                HttpContext.Session.SetString("UserName", model.UserName);
                return RedirectToAction("Index");
            }
            else
            {
                _notify.Error("Kullanıcı Adı veya Parola Geçersizdir!");
            }

            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (_context.Users.Count(s => s.UserName == model.UserName) > 0)
            {
                _notify.Error("Girilen Kullanıcı Adı Kayıtlıdır!");
                return View(model);
            }
            if (_context.Users.Count(s => s.Email == model.Email) > 0)
            {
                _notify.Error("Girilen E-Posta Adresi Kayıtlıdır!");
                return View(model);
            }

            var user = new User();
            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Password = model.Password;
            user.Email = model.Email;
            _context.Users.Add(user);
            _context.SaveChanges();

            _notify.Success("Üye Kaydı Yapılmıştır. Oturum Açınız");

            return RedirectToAction("Login");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        [AuthFilter]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}