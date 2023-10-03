using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.DTOs;
using WebUI.Models;

namespace WebUI.Controllers
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
            if (User.Identity.IsAuthenticated)
            {
            return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var checkEmail = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (checkEmail is null) 
            {
                ModelState.AddModelError("Error", "Email or Password is incorrect!");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(checkEmail.UserName, loginDTO.Password, loginDTO.RememberMe, true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ModelState.AddModelError("Error", "Email or Password is incorrect!");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var chackEmail = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (chackEmail is not null)
            {
                ModelState.AddModelError("Error", "Email is already existed!");
                return View();
            }

            User newUser = new()
            {
                Email = registerDTO.Email,
                FirstName = registerDTO.Firstname,
                LastName = registerDTO.Lastname,
                UserName = registerDTO.Email,
                AboutAuther = string.Empty,
                PhotoUrl = "/"

            };

            var resault = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (resault.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                foreach (var error in resault.Errors)
                {
                    ModelState.AddModelError("Error", error.Description);
                }
                return View();
            }

        }
    }
}
