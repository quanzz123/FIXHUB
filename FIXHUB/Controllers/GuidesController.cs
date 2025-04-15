using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Controllers
{
    public class GuidesController : Controller
    {
        private readonly FixHubDbContext _context;

        public GuidesController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var listofguides = (from g in _context.RepairGuides
                                where g.CategoryId == 1
                                select g).ToList();
            var listofcate = _context.GuideCategories.ToList();
            ViewBag.GuidesCate = listofcate;
            ViewBag.Guides = listofguides;
            return View();
        }
        public IActionResult GuidesCategoryDetails(int id)
        {
            var category = _context.GuideCategories.FirstOrDefault(p => p.CategoryId == id);
            if (category == null) {

                return NotFound();
            
            }
            var listofguides = (from g in _context.RepairGuides
                                where g.CategoryId == id
                                select g).ToList();

            var listofcate = (from l in _context.GuideCategories
                              where l.ParentId == id
                              select l).ToList();


            ViewBag.GuidesCate = listofcate;
            ViewBag.Guides = listofguides;
            return View(category);
        } 
    }
}
