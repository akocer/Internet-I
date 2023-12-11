using Microsoft.AspNetCore.Mvc;
using uyg04_CookieAuth.Models;
using uyg04_CookieAuth.ViewModels;

namespace uyg04_CookieAuth.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productModels = _context.Products.Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name
            }).ToList();

            return View(productModels);
        }

        public IActionResult PageList(string searchText = "", int page = 1, int size = 6)
        {
            var products = _context.Products.Where(s => s.Name.ToLower().Contains(searchText.ToLower())).AsQueryable();

            int pageskip = (page - 1) * size;
            var productModels = products.Skip(pageskip).Take(size).Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name
            }).ToList();
            int recordCount = products.Count();
            int pageCount = (int)Math.Ceiling(Convert.ToDecimal(recordCount / size));


            ViewBag.PageCount = pageCount;
            ViewBag.RecordCount = recordCount;
            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.SearchText = searchText;

            return View(productModels);
        }
    }
}
