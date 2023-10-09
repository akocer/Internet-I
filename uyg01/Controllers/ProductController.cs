using Microsoft.AspNetCore.Mvc;
using uyg01.Models;

namespace uyg01.Controllers
{

    public class ProductController : Controller
    {
        private readonly ProductRepository productRepository;

        public ProductController(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = productRepository.GetList();

            return View(products);
        }

        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            var product = productRepository.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            productRepository.Remove(product.Id);
            return RedirectToAction("Index");
        }
    }
}
