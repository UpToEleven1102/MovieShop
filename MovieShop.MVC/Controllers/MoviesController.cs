using Microsoft.AspNetCore.Mvc;

namespace MovieShop.MVC.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/movies/top-revenue-movies")]
        public IActionResult TopRevenueMovies()
        {
            return View();
        }

        [Route("/movies/top-rated-movies")]
        public IActionResult TopRatedMovies()
        {
            return View();
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