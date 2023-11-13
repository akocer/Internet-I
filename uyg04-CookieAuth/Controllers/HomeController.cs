using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Diagnostics;
using System.Security.Claims;
using uyg04_CookieAuth.Models;
using uyg04_CookieAuth.ViewModels;

namespace uyg04_CookieAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly INotyfService _notify;
        private readonly IConfiguration _config;
        public HomeController(ILogger<HomeController> logger, AppDbContext context, INotyfService notify, IConfiguration config)
        {
            _logger = logger;
            _context = context;
            _notify = notify;
            _config = config;
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
            var hashedpass = MD5Hash(model.Password);
            var user = _context.Users.Where(s => s.UserName == model.UserName && s.Password == hashedpass).SingleOrDefault();

            if (user == null)

            {
                _notify.Error("Kullanıcı Adı veya Parola Geçersizdir!");
                return View();
            }

            List<Claim> claims = new List<Claim>() {

                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim("UserName",user.UserName),
                new Claim("PhotoUrl",user.PhotoUrl),

                };

            ClaimsIdentity idetity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = new ClaimsPrincipal(idetity);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = model.KeepMe
            };
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

            return RedirectToAction("Index");

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


            var hashedpass = MD5Hash(model.Password);
            var user = new User();
            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Password = hashedpass;
            user.Email = model.Email;
            user.PhotoUrl = "-";
            user.Role = "Uye";
            _context.Users.Add(user);
            _context.SaveChanges();

            _notify.Success("Üye Kaydı Yapılmıştır. Oturum Açınız");

            return RedirectToAction("Login");
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string MD5Hash(string pass)
        {
            var salt = _config.GetValue<string>("AppSettings:MD5Salt");
            var password = pass + salt;
            var hashed = password.MD5();
            return hashed;
        }
    }
}