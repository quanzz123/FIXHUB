using FIXHUB.Areas.Admin.ViewModel;
using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TechnicianController : Controller
    {
        private readonly FixHubDbContext _context;
        public TechnicianController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tech = (from t in _context.Technicians
                        join u in _context.Users on t.UserId equals u.UserId
                        join up in _context.UserProfiles on u.UserId equals up.UserId
                        select new
                        {
                            t.TechnicianId,
                            t.UserId,
                            t.Expertise,
                            t.Rating,
                            UserName = u.UserName,
                            ProfilePicture = up.AvatarUrl,
                            IsActive = u.IsActive
                        } 
                        
                        
                        ).ToList();
           var techVM = tech.Select(t => new TechnicianVM
           {
               TechnicianID = t.TechnicianId,
               UserID = t.UserId,
               Expertise = t.Expertise,
               Rating = t.Rating,
               UserName = t.UserName,
               ProfilePicture = t.ProfilePicture,
                IsActive = t.IsActive ?? true // Default to true if IsActive is null
           }).ToList();
            return View(techVM);
        }
    }
}
