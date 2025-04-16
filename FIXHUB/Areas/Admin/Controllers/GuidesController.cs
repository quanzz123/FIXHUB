using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GuidesController : Controller
    {
        private readonly FixHubDbContext _context;
        public GuidesController(FixHubDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var guiCate = _context.GuideCategories.Select(m => new GuideCategory
            {
                CategoryId = m.CategoryId,
                CategoryName = m.CategoryName,
                Description = m.Description,
                ImgUrl = m.ImgUrl,
                ParentId = m.ParentId,
                Levels = m.Levels,
                IsActive = m.IsActive,
            }).ToList();
            return View(guiCate);
        }

        public IActionResult RepairGuides()
        {
            var listofrepair = (from l in _context.RepairGuides
                                select l).ToList();
            return View(listofrepair);
        }

        
    }
}
