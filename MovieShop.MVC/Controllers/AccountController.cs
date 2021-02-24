using System;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;

namespace MovieShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register(UserRegisterRequestModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return View();
        }
        
        [HttpPost]
        public IActionResult Login(Object accountRequest) // Replace with Account request model later
        {
            Console.WriteLine(accountRequest);
            return View();
        }
        
    }
}