using DiffMatchPatch;
using FIXHUB.Models;
using FIXHUB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.RegularExpressions;

namespace FIXHUB.Controllers
{
    public class StepRepairController : Controller
    {
        private readonly FixHubDbContext _context;
        public StepRepairController(FixHubDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var historystep = (from h in _context.HistorySteps
                               join s in _context.GuideSteps on h.StepId equals s.StepId
                               join u in _context.Users on h.UserId equals u.UserId
                               where h.StepId == id
                               select new
                               {
                                   h.HistoryStepId,
                                   h.StepId,
                                   h.UserId,
                                   h.CreatedDate,
                                   h.Title,
                                   StepNumber = s.StepNumber,
                                   UserName = u.UserName
                               }).ToList();

            var historystepVM = historystep.Select(h => new HistoryStepVM
            {
                HistoryStepId = h.HistoryStepId,
                StepId = h.StepId ?? 0,
                Title = h.Title ?? "No Title",
                StepNumber = h.StepNumber ?? 0,
                UserId = h.UserId ?? 0,
                UserName = h.UserName ?? "Unknown User",
                CreatedAt = h.CreatedDate ?? DateTime.Now,
            }).ToList();

            return View(historystepVM);
        }
        public IActionResult ChangestoStep(int id)
        {
            var changestep = (from h in _context.HistorySteps
                              join s in _context.GuideSteps on h.StepId equals s.StepId
                              join u in _context.Users on h.UserId equals u.UserId
                              where h.HistoryStepId == id
                              select new
                              {
                                  h.HistoryStepId,
                                  h.StepId,
                                  h.UserId,
                                  h.CreatedDate,
                                  h.Title,
                                  h.ImgUrl,
                                  h.Details,
                                  OldImage = s.ImageUrl,
                                  OldContent = s.Instruction,
                                  StepNumber = s.StepNumber,
                                  UserName = u.UserName
                              }).FirstOrDefault();

            if (changestep == null) return NotFound();

            // ✂️ Hàm loại bỏ thẻ HTML
            string StripHtml(string html)
            {
                return Regex.Replace(html ?? "", "<.*?>", string.Empty);
            }

            string oldText = StripHtml(changestep.OldContent);
            string newText = StripHtml(changestep.Details);

            // So sánh nội dung đã loại bỏ HTML
            var dmp = new diff_match_patch();
            var diffs = dmp.diff_main(oldText, newText);
            dmp.diff_cleanupSemantic(diffs);
            string htmlDiff = dmp.diff_prettyHtml(diffs);

            var changestepVM = new HistoryStepVM
            {
                HistoryStepId = changestep.HistoryStepId,
                StepId = changestep.StepId ?? 0,
                Title = changestep.Title ?? "No Title",
                StepNumber = changestep.StepNumber ?? 0,
                UserId = changestep.UserId ?? 0,
                UserName = changestep.UserName ?? "Unknown User",
                CreatedAt = changestep.CreatedDate ?? DateTime.Now,
                ImgUrl = changestep.ImgUrl ?? "No Image",
                OldImage = changestep.OldImage ?? "No Image", 
                OldContent = changestep.OldContent, // giữ nguyên HTML gốc để hiển thị nếu muốn
                NewContent = changestep.Details,
                HtmlDiff = htmlDiff
            };

            var step = _context.GuideSteps.FirstOrDefault(s => s.StepId == changestep.StepId);
            ViewBag.chagestep = step;

            return View(changestepVM);
        }
    }
}
