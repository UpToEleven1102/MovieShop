using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IMovieService _movieService;

        public UserController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BuyMovie(int id, string returnUrl = null)
        {
            var movieDetails = await _movieService.BuyMovie(id);
            if (movieDetails != null)
            {
                return View(movieDetails);
            }
            
            TempData["ErrorMessage"] = "Something went wrong!";
            return RedirectToAction("Details", "Movies", new { Id = id});
        }
    }
}