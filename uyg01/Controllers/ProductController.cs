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

        public IActionResult Detail(int id)
        {
            var product = productRepository.GetById(id);
            return View(product);
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Product product)
        {
            productRepository.Add(product);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var product = productRepository.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            productRepository.Update(product);
            return RedirectToAction("Index");
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
