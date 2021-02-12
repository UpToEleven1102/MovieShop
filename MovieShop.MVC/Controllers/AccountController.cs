using System;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Details(int id)
        {
            Console.WriteLine(id);
            return View(id);
        }

        [HttpPost]
        public IActionResult Create(Object accountRequest) // Replace with Account request model later
        {
            Console.WriteLine(accountRequest);
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