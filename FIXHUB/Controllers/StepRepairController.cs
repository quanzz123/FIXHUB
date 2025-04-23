using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Controllers
{
    public class StepRepairController : Controller
    {
        private readonly FixHubDbContext _context;
        public StepRepairController(FixHubDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
