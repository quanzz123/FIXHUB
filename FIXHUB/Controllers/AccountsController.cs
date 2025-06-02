using FIXHUB.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FIXHUB.ViewModels;
using FIXHUB.Utilities;
using FIXHUB.Services;
namespace FIXHUB.Controllers
{
    public class AccountsController : Controller
    {
        private readonly FixHubDbContext _context;

        public AccountsController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }
            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == model.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại.");
                ViewBag.Message = "Tên đăng nhập hoặc email đã tồn tại";
                return View(model);
            }
            var user = new User
            {
                UserName = model.UserName,
                PasswordHash = func.HashPassword( model.Password), 
                Email = model.Email
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            // Tạo role cho user mới
            var userRole = new UserRole
            {
                UserId = user.UserId,
                RoleId = 3 // role 3 là của customer 
            };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }
        [ HttpGet ]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Trả lại view kèm lỗi
            }

            var account = (from u in _context.Users
                           join ur in _context.UserRoles on u.UserId equals ur.UserId
                           join r in _context.Roles on ur.RoleId equals r.RoleId
                           where u.UserName == model.UserName 
                           select new
                           {
                               u.UserId,
                               u.UserName,
                               u.Email,
                               u.PasswordHash,
                               RoleName = r.RoleName
                           }).FirstOrDefault();

            if (account == null || !func.VerifyPassword(model.Password, account.PasswordHash))
            {
                ViewBag.Message = "Sai tên đăng nhập hoặc mật khẩu";
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.Role, account.RoleName), 
                new Claim("UserId",account.UserId.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (account.RoleName == "admin" || account.RoleName == "tech")
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

    }
}
