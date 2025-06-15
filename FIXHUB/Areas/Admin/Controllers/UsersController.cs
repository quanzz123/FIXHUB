using FIXHUB.Areas.Admin.ViewModel;
using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        public FixHubDbContext _context;
        public UsersController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var users = (from u in _context.Users
                         join up in _context.UserProfiles on u.UserId equals up.UserId
                         join p in _context.UserPoints on u.UserId equals p.UserId
                         select new
                         {
                             u.UserId,
                             u.UserName,
                             u.Email,
                             u.CreatedAt,
                             u.IsActive,
                             Fullname = up.FullName,
                             Address = up.Address,
                             Bio = up.Bio,
                             phone = up.PhoneNumber,
                             avt = up.AvatarUrl,
                             point = p.PointBalance ?? 0,
                         }).ToList();
            var UserVM = users.Select(t => new UsersVM
            {
                UserId = t.UserId,
                UserName = t.UserName,
                FullName = t.Fullname,
                Email = t.Email,
                PhoneNumber = t.phone,
                Address = t.Address,
                ProfilePicture = t.avt,
                Bio = t.Bio,

                IsActive = t.IsActive ?? true, // Default to true if IsActive is null
                points = t.point,
                CreatedAt = (DateTime)t.CreatedAt
            }).ToList();

            return View(UserVM);
        }

        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsActive = false;
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> EditAccount(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            var userVM = new UsersVM
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FullName = userProfile.FullName,
                Email = user.Email,
                PhoneNumber = userProfile.PhoneNumber,
                Address = userProfile.Address,
                ProfilePicture = userProfile.AvatarUrl,
                Bio = userProfile.Bio,



            };
            return View(userVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditAccount(UsersVM userVM)
        {
           
            var user = await _context.Users.FindAsync(userVM.UserId);
            if (user == null)
            {
                return NotFound();
            }
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == userVM.UserId);
            if (userProfile == null)
            {
                return NotFound();
            }
            user.UserName = userVM.UserName;
            user.Email = userVM.Email;
            userProfile.FullName = userVM.FullName;
            userProfile.PhoneNumber = userVM.PhoneNumber;
            userProfile.Address = userVM.Address;
            //userProfile.AvatarUrl = userVM.ProfilePicture;
            //userProfile.Bio = userVM.Bio;
            _context.Users.Update(user);
            _context.UserProfiles.Update(userProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
