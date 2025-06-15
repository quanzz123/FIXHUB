using FIXHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly FixHubDbContext _context;
        public HomeController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var listofRequests = (from r in _context.ServiceRequests
                                  select r).ToList();
            ViewBag.ServiceRequests = listofRequests;
            return View();
        }
    }
}
