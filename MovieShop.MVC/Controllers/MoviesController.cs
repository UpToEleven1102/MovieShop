using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Entities;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.MVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _service;
        public MoviesController(IMovieService service)
        {
            _service = service;
        }
        
        [ActionName("Details")]
        public IActionResult Index(int id)
        {
            var movieDetail = _service.GetMovieById(id);
            return View(movieDetail);
        }

        [Route("/movies/top-revenue-movies")]
        public IActionResult TopRevenueMovies()
        {
            var movies = _service.GetTopGrossingMovies();
            return View(movies);
        }

        [Route("/movies/top-rated-movies")]
        public IActionResult TopRatedMovies()
        {
            var movies = _service.GetTopRatedMovies();
            return View(movies);
        }

        // id is not required? no?
        public IActionResult Genre(int id)
        {
            return View();
        }
        
        [Route("/movies/reviews/{id}")]
        public IActionResult Review(int id)
        {
            return View();
        } 
    }
}