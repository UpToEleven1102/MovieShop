using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber, int pageSize)
        {
            pageNumber -= 1; // count from page 0
            if (pageSize <= 0 || pageSize > 30) pageSize = 30;
            if (pageNumber < 0) pageNumber = 0;
            try
            {
                var movies =
                    await _movieService.GetMoviesPaginated(pageNumber,
                        pageSize); // this sh!t takes forever, pagination???
                return Ok(movies);
            }
            catch (Exception _)
            {
                return StatusCode(500, "Something went wrong!");
            }
        }

        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<IActionResult> GetReviewsByMovieId(int id)
        {
            try
            {
                var reviews = await _movieService.GetReviewsByMovieId(id);

                return reviews == null ? NotFound() : Ok(reviews);
            }
            catch (Exception _)
            {
                return StatusCode(500, "Something went wrong!");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieById(id);

                return movie == null ? NotFound() : Ok(movie);
            }
            catch (Exception _)
            {
                return StatusCode(500, "Something went wrong!");
            }
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            try
            {
                var movies = await _movieService.GetTopRatedMovies();
                return Ok(movies);
            }
            catch (Exception _)
            {
                return StatusCode(500, "Something went wrong!");
            }
        }

        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovie()
        {
            try
            {
                var movies = await _movieService.GetTopGrossingMovies();
                return Ok(movies);
            }
            catch (Exception _)
            {
                return StatusCode(500, "Something went wrong!");
            }
        }

        [HttpGet]
        [Route("genre/{genreId}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber - 1;
            if (pageNumber < 0) pageNumber = 0;
            if (pageSize <= 0 || pageSize > 30) pageSize = 30;
            try
            {
                var movies = await _movieService.GetMoviesByGenreId(genreId, pageNumber, pageSize);
                return Ok(movies);
            }
            catch (Exception _)
            {
                return StatusCode(500, "Something went wrong!");
            }
        }
    }
}