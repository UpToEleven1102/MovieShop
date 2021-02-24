using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel userModel)
        {
            if (!ModelState.IsValid) return View();

            await _userService.RegisterUser(userModel);
            return RedirectToAction("Login");
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // [HttpPost]
        // public Task<IActionResult> Login(UserLoginRequestModel userModel)
        // {
        //     
        // }
    }
}