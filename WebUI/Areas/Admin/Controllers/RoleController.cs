using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult Create() 
        {
        return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (role.Name == null)
            {
                ViewData["Error"] = "Rol is Empty!";
                return View();
            }

            var checkRole = await _roleManager.FindByNameAsync(role.Name);
            if (checkRole != null) 
            {
                ViewData["Error"] = "Rol is slready exists!";
                return View();
            }
            
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
    }
}
