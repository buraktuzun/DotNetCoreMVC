using DotNetCoreMVC.Helpers;
using DotNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [HttpGet]
        public IActionResult Add()
        {
            List<int> Expire = new List<int> { 1, 2, 3, 6, 12};
            ViewBag.Expire = Expire;

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>
            {
                new ColorSelectList { Data = "Red", Value = "Red" },
                new ColorSelectList { Data = "Green", Value = "Green" },
                new ColorSelectList { Data = "Blue", Value = "Blue" },
                new ColorSelectList { Data = "Yellow", Value = "Yellow" },
                new ColorSelectList { Data = "Black", Value = "Black" },
                new ColorSelectList { Data = "White", Value = "White" },
            }, "Value", "Data");

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

            List<int> Expire = new List<int> { 1, 2, 3, 6, 12 };
            ViewBag.Expire = Expire;
            ViewBag.SelectedExpire = product.Expire;

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>
            {
                new ColorSelectList { Data = "Red", Value = "Red" },
                new ColorSelectList { Data = "Green", Value = "Green" },
                new ColorSelectList { Data = "Blue", Value = "Blue" },
                new ColorSelectList { Data = "Yellow", Value = "Yellow" },
                new ColorSelectList { Data = "Black", Value = "Black" },
                new ColorSelectList { Data = "White", Value = "White" },
            }, "Value", "Data", product.Color);



            return View(product);
        }
    }
}
