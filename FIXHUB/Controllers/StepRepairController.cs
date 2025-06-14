using FIXHUB.Models;
using FIXHUB.ViewModels;
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
    }
}
