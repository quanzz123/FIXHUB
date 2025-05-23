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

        public IActionResult Index(int? stepid)
        {
            var historystep = (from h in _context.HistorySteps
                               where h.StepId == stepid
                               select h).ToList();
            return View(historystep);
        }
    }
}
