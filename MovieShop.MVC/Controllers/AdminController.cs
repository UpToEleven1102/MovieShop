using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateMovie(Object movieRequest)
        {
            return View("Create");
        }

        public IActionResult EditMovie(int id, Object movieRequest)
        {
            return View("Edit");
        }
    }
}