using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.DTOs;
using WebUI.Models;

namespace WebUI.Areas.Admin.Views.Dashboard
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO) 
        {
        return View();  
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {

            var chackEmail = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (chackEmail is null) 
            {
                ModelState.AddModelError("Error", "Email is already existed!");
                    return View();
            }

            User newUser = new()
            {
                Email = registerDTO.Email,
                FirstName = registerDTO.Firstname,
                LastName = registerDTO.Lastname,
                UserName = registerDTO.Email
            };

            var resault = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (resault.Succeeded) 
            {
            return RedirectToAction(nameof(Login));
            }
            else{
                foreach (var error in resault.Errors) 
                {
                    ModelState.AddModelError("Error", error.Description);
                }
                return View();
            }

        }
    }
}
