using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RequestServicesController : Controller
    {
        private readonly FixHubDbContext _context;
        public RequestServicesController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var serviceRequests = _context.ServiceRequests
            .Include(sr => sr.ServiceType)
            .Include(u => u.User)
            .ToList();
            return View(serviceRequests);
        }

        public IActionResult Details(int id)
        {
            var serviceRequest = _context.ServiceRequests
                .Include(sr => sr.ServiceType)
                .Include(sr => sr.User)
                .FirstOrDefault(sr => sr.RequestId == id);
            if (serviceRequest == null)
            {
                return NotFound();
            }
            return View(serviceRequest);
        }

        public async Task<IActionResult> AcceptRequest(ServiceRequest model)
        {
            int userID = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

            var technician = await _context.Technicians
                .FirstOrDefaultAsync(t => t.UserId == userID);

            if (technician == null)
            {
                return NotFound("Không tìm thấy kỹ thuật viên phù hợp.");
            }

            var serviceRequest = await _context.ServiceRequests.FindAsync(model.RequestId);
            if (serviceRequest == null)
            {
                return NotFound();
            }
            //cậ nhật lại trang thái của yêu cầu dịch vụ
            serviceRequest.Status = "accepted";
            _context.ServiceRequests.Update(serviceRequest);

            // Tạo bản ghi mới trong
            var asigment = new RequestAssignment
            {
                RequestId = serviceRequest.RequestId,
                TechnicianId = technician.TechnicianId,
                AssignedAt = DateTime.Now,
                Notes = "Yêu cầu đã được chấp nhận bởi kỹ thuật viên.",
            };
            _context.RequestAssignments.Add(asigment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
