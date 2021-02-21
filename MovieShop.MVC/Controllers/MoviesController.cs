using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index(int id)
        {
            var movieDetail = await _service.GetMovieById(id);
            return View(movieDetail);
        }

        [Route("/movies/top-revenue-movies")]
        public async Task<IActionResult> TopRevenueMovies()
        {
            var movies = await _service.GetTopGrossingMovies();
            return View(movies);
        }

        [Route("/movies/top-rated-movies")]
        public async Task<IActionResult> TopRatedMovies()
        {
            var movies = await _service.GetTopRatedMovies();
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