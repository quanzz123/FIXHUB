using FIXHUB.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FIXHUB.ViewModels;

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
                           where u.UserName == model.UserName && u.PasswordHash == model.Password
                           select new
                           {
                               u.UserName,
                               u.Email,
                               u.PasswordHash,
                               RoleName = r.RoleName
                           }).FirstOrDefault();

            if (account == null)
            {
                ViewBag.Message = "Sai tên đăng nhập hoặc mật khẩu";
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.Role, account.RoleName)
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
