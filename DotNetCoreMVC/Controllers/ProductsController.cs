using DotNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreMVC.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;

            if(!_context.Products.Any())
            {
                _context.Products.Add(new Product { Name = "Product 1", Price = 100, Stock = 10 });
                _context.Products.Add(new Product { Name = "Product 2", Price = 200, Stock = 20 });
                _context.Products.Add(new Product { Name = "Product 3", Price = 300, Stock = 30 });
                _context.Products.Add(new Product { Name = "Product 4", Price = 400, Stock = 40 });

                _context.SaveChanges();
            }

            
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
    }
}
