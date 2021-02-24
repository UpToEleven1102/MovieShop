using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostReview(ReviewRequestModel reviewModel, int movieId)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Missing Params";
                return RedirectToAction("Details", "Movies", new { Id = movieId}); 
            }

            var res = await _movieService.PostReview(reviewModel);
            if (res)
            {
                TempData["SuccessMessage"] = "Review posted!";
            }
            else
            {
                // better to allow user edit review here
                TempData["ErrorMessage"] = "You already posted review for this movie!";
            }
            return RedirectToAction("Details", "Movies", new { Id = movieId});   
        }
    }
}