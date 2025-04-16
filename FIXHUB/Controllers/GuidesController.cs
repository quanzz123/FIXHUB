using FIXHUB.Models;
using FIXHUB.ViewModels;
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
        
        public IActionResult RepairDetails(int  id)
        {
            var repairdetails = (from step in _context.GuideSteps
                                 join guide in _context.RepairGuides
                                 on step.GuideId equals guide.GuideId
                                 where step.GuideId == id
                                 select new GuideStepDetails
                                 {
                                     StepId = step.StepId,
                                     StepNumber = step.StepNumber ?? 0,
                                     Instruction = step.Instruction,
                                     ImagePath = step.ImageUrl,
                                     GuideTitle = guide.Title,
                                     GuideImgUrl= guide.ImgUrl,
                                     GuideIntro = guide.Content,
                                     
                                 }).ToList(); 
            return View(repairdetails);
        }
        public IActionResult EditStep(int id)
        {
            return View();
        }

        public ActionResult CreateRepair(int id) {


            return View();
        
        }
    }
}
