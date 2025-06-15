using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FIXHUB.Controllers
{
    public class ContactController : Controller
    {
        private readonly FixHubDbContext _context;
        public ContactController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name, string email, string phone, string message)
        {
            
            try
            {
                // Tạo đối tượng review mới
                Contact con = new Contact
                {
                    Name = name,
                    Phone = phone,
                    Email = email,
                    CreatedDate = DateTime.Now,
                    Message = message,

                };

                // Thêm vào DbSet và lưu vào cơ sở dữ liệu
                _context.Contacts.Add(con);
                await _context.SaveChangesAsync(); 

                return Json(new { status = true }); 
            }
            catch (Exception ex)
            {
                // Ghi log lỗi (nếu cần) và trả về trạng thái thất bại
                return Json(new { status = false, message = ex.Message });
            }
            return RedirectToAction("Index");
        }
    }
}
