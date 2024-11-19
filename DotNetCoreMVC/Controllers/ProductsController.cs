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
            return View("AddProduct");
        }

        [HttpPost]
        public IActionResult AddProduct(Product product) 
        { 
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();

            //_context.Products.Add(new Product {Name = name, Price = price, Stock = stock, Color = color });
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
