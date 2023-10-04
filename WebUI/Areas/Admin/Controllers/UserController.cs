using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.ViewModel;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> AddRole(string userId)
        {
            if (userId == null) return NotFound();

            var checkUser = await _userManager.FindByIdAsync(userId);

            if (checkUser == null) return NotFound();

            var userRoles = (await _userManager.GetRolesAsync(checkUser)).ToList();
            var roles = _roleManager.Roles.Select(x => x.Name).ToList();

            UserRoleVM userRoleVM = new()
            {
                User = checkUser,
                Roles = roles.Except(userRoles),
            };
            return View(userRoleVM);
        }

        [HttpPost]

        public async Task<IActionResult> AddRole(string userId, string role)
        {
            if (userId == null) return NotFound();

            var checkUser = await _userManager.FindByIdAsync(userId);

            if (checkUser == null) return NotFound();

            var userAddRole = await _userManager.AddToRoleAsync(checkUser, role);

            if (!userAddRole.Succeeded)
            {
                ViewData["Error"] = "Something went wrong!";
                return View();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditRole(string userId)
        {
            if (userId == null) return NotFound();

            var checkUser = await _userManager.FindByIdAsync(userId);

            if (checkUser == null) return NotFound();
            return View(checkUser);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string userId, string role)
        {
            if (userId == null || role == null) return NotFound();
            var checkUser = await _userManager.FindByIdAsync(userId);

            if (checkUser == null) return NotFound();

            IdentityResult result = await _userManager.RemoveFromRoleAsync(checkUser, role);

            if (!result.Succeeded)
            {
                ViewData["Error"] = "Something went wrong!";
                return View();
            }
            return RedirectToAction("Index");
        }

    }
}
