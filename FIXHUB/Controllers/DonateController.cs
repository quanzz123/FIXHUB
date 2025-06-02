using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIXHUB.Controllers
{
    public class DonateController : Controller
    {
        private readonly FixHubDbContext _context;
        public DonateController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Donatepoint([FromBody] DonateRequest request)
        {
            try
            {
                // Lấy UserId từ claim (khi đăng nhập đã thêm claim này)
                var userIdClaim = User.FindFirst("UserId")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userID))
                {
                    return Json(new { success = false, message = "Không thể xác định người dùng đăng nhập." });
                }

                // Kiểm tra dữ liệu đầu vào
                if (request == null || request.AuthorId <= 0 || request.Amount <= 0)
                {
                    return Json(new { success = false, message = "Dữ liệu donate không hợp lệ." });
                }

                if (userID == request.AuthorId)
                {
                    return Json(new { success = false, message = "Bạn không thể donate cho chính mình." });
                }

                // Lấy thông tin người gửi và người nhận
                var donor = await _context.UserPoints
                    .Include(u => u.User)
                    .FirstOrDefaultAsync(u => u.UserId == userID);

                var author = await _context.UserPoints
                    .FirstOrDefaultAsync(u => u.UserId == request.AuthorId);

                if (donor == null || author == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy người dùng." });
                }

                if (donor.PointBalance < request.Amount)
                {
                    return Json(new { success = false, message = "Bạn không đủ điểm để donate." });
                }

                // Cập nhật điểm
                donor.PointBalance -= request.Amount;
                author.PointBalance += request.Amount;

                // Ghi nhật ký giao dịch
                var transaction = new PointTransaction
                {
                    SenderId = userID,
                    ReceiverId = request.AuthorId,
                    Points = request.Amount,
                    Type = "Donation",
                    Description = $"Donated {request.Amount} points to user ID {request.AuthorId}",
                    CreatedAt = DateTime.UtcNow
                };
                _context.PointTransactions.Add(transaction);

                // Gửi thông báo cho người nhận
                var notification = new Notification
                {
                    UserId = request.AuthorId,
                    Message = $"Bạn đã nhận được {request.Amount} điểm từ người dùng {donor.User?.UserName ?? "ẩn danh"}",
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                };
                _context.Notifications.Add(notification);

                // Lưu tất cả thay đổi
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Donate thành công." });
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        public class DonateRequest
        {
            public int AuthorId { get; set; }
            public int Amount { get; set; }
            
        }
    }
}
