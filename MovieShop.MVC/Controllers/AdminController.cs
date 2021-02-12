using System;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Object movieRequest)
        {
            return View();
        }

        [HttpPut]
        public IActionResult Edit(int id, Object movieRequest)
        {
            return View();
        }
    }
}