using DotNetCoreMVC.Helpers;
using DotNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreMVC.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private IHelper _helper;

        public ProductsController(AppDbContext context, IHelper helper)
        {
            _context = context;
            _helper = helper;
        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(products);
        }

        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            ViewBag.Expire = new List<string>() { "1 Ay", "2 Ay", "3 Ay", "6 Ay", "1 Yıl" };

            return View("AddProduct");
        }

        [HttpPost]
        public IActionResult AddProduct(Product product) 
        { 
            _context.Products.Add(product);
            _context.SaveChanges();

            TempData["Message"] = "Product added successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Update(Product updatedProduct)
        {
            _context.Products.Update(updatedProduct);
            _context.SaveChanges();

            TempData["Message"] = "Product updated successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int Id)
        {
            var product = _context.Products.Find(Id);
            return View(product);
        }
    }
}
