﻿using FIXHUB.Models;
using FIXHUB.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [Authorize]
        public IActionResult CreateRepair(int id) {
            ViewBag.CategoryId = id;

            return View();
        
        }
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateRepair(RepairGuide repair)
        {
            int userID = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            var user = (from u in _context.Users
                        where u.UserId == userID
                        select u).FirstOrDefault();
            var categoryid = ViewBag.Id;
            if (ModelState.IsValid)
            {
                // Xử lý ảnh nếu có
                if (repair.ImageFile != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(repair.ImageFile.FileName);
                    var extension = Path.GetExtension(repair.ImageFile.FileName);
                    string newFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Guides", newFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await repair.ImageFile.CopyToAsync(stream);
                    }

                    repair.ImgUrl = "~/img/Guides/" + newFileName;
                }
                //repair.CategoryId = categoryid;
                repair.UserId= userID;
                repair.CreatedBy = user.UserName;
                repair.CreatedAt = DateTime.Now;

                //

                _context.RepairGuides.Add(repair);
                await _context.SaveChangesAsync();

                return RedirectToAction("Createstep", new {guideid = repair.GuideId});


            }
          
            return View(repair);

        }
        [HttpGet]
        public IActionResult Createstep(int guideid)
        {
            ViewBag.GuideId = guideid;
            var title = (from t in _context.RepairGuides
                         where t.GuideId == guideid
                         select t).FirstOrDefault();

            ViewBag.GuidesTitle = title.Title;
            return View();
        }

        public async Task<IActionResult> Createstep(GuideStep step)
        {
            int userID = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            var user = (from u in _context.Users
                        where u.UserId == userID
                        select u).FirstOrDefault();
            if (ModelState.IsValid)
            {
                // Lấy số bước hiện có của hướng dẫn
                var existingSteps = _context.GuideSteps
                    .Where(s => s.GuideId == step.GuideId)
                    .OrderByDescending(s => s.StepNumber)
                    .ToList();

                step.StepNumber = existingSteps.Count > 0 ? existingSteps.First().StepNumber + 1 : 1;

                // Xử lý ảnh như trước
                if (step.ImageFile != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(step.ImageFile.FileName);
                    var extension = Path.GetExtension(step.ImageFile.FileName);
                    string newFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Guides", newFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await step.ImageFile.CopyToAsync(stream);
                    }

                    step.ImageUrl = "~/img/steps/Guides" + newFileName;
                }
                step.UserId = userID;
                _context.GuideSteps.Add(step);
                await _context.SaveChangesAsync();

                return RedirectToAction("Createstep", new { guideId = step.GuideId });
            }

            ViewBag.GuideId = step.GuideId;
            return View(step);
        }

    }
}
