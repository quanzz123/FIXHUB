using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeFixes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FIXHUB.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly FixHubDbContext _context;
        public ServiceRequestsController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tech = (from t in _context.Technicians
                        join u in _context.Users on t.UserId equals u.UserId
                        join p in _context.UserProfiles on u.UserId equals p.UserId
                        select new
                        {
                            u.UserId,
                            u.UserName,
                            p.FullName,
                            p.Bio,
                            t.Expertise,
                            t.Rating,
                        });

            return View();
        }
        [HttpGet]
        public IActionResult GetServiceTypes()
        {
            var services = _context.ServiceTypes
                            .Select(s => new {
                                id = s.ServiceTypeId,
                                name = s.Name,
                            }).ToList();
            return Json(services);
        }


        [HttpPost]
        [Route("ServiceRequest/CreateRequest")]
        public async Task<IActionResult> CreateRequest(
                int technicianId,
                int serviceTypeId,
                string title,
                string description,
                string address,
                string preferredDate,
                string preferredTime)
        {

            
            try
            {
                var user = (from u in _context.Technicians
                            where u.TechnicianId == technicianId
                            select u).FirstOrDefault();
                // Tạo đối tượng request
                var request = new ServiceRequest
                {
                    UserId = user.UserId,
                    ServiceTypeId = serviceTypeId,
                    Title = title,
                    Description = description,
                    Address = address,
                    PreferredDate = DateTime.Parse(preferredDate),
                    //PreferredTime = preferredTime,
                    //CreatedDate = DateTime.Now,
                    Status = "Pending"
                };

                // Lưu vào database
                _context.ServiceRequests.Add(request);
                await _context.SaveChangesAsync();

                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
            
        }


    }
}
