using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly FixHubDbContext _context;
        public ProductController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var product = (from p in _context.Products
                           select p).ToList();
            return View(product);
        }
        public IActionResult Category() { 
            var category = (from c in _context.ProductCategories
                            select c).ToList();
            return View(category);
        }
    }
}
