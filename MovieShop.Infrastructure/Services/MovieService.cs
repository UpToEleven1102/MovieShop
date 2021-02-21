using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<MovieDetailsResponse> GetMovieById(int id)
        {
            var movieDetail = new MovieDetailsResponse();
            var movie = await _repository.GetByIdAsync(id);
            movieDetail.Id = movie.Id;
            movieDetail.Title = movie.Title;
            movieDetail.Overview = movie.Overview;
            movieDetail.Tagline = movie.Tagline;
            movieDetail.Budget = movie.Budget;
            movieDetail.Revenue = movie.Revenue;
            movieDetail.ImdbUrl = movie.ImdbUrl;
            movieDetail.TmdbUrl = movie.TmdbUrl;
            movieDetail.PosterUrl = movie.PosterUrl;
            movieDetail.BackdropUrl = movie.BackdropUrl;
            movieDetail.OriginalLanguage = movie.OriginalLanguage;
            movieDetail.ReleaseDate = movie.ReleaseDate;
            movieDetail.RunTime = movie.RunTime;
            movieDetail.Price = movie.Price;
            movieDetail.Genres = new List<GenreModel>();
            foreach (var genre in movie.Genres)
                movieDetail.Genres.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });

            movieDetail.Casts = new List<CastResponseModel>();
            foreach (var mc in movie.MovieCasts)
                movieDetail.Casts.Add(new CastResponseModel
                {
                    Id = mc.Cast.Id,
                    Name = mc.Cast.Name,
                    Gender = mc.Cast.Gender,
                    TmdbUrl = mc.Cast.TmdbUrl,
                    ProfilePath = mc.Cast.ProfilePath,
                    Character = mc.Character
                });

            if (movie.Reviews.Any())
            {
                decimal rating = 0;
                foreach (var review in movie.Reviews) rating += review.Rating;

                movieDetail.Rating = rating / movie.Reviews.Count();
            }

            return movieDetail;
        }

        public async Task<IEnumerable<MovieDetailsResponse>> GetTopGrossingMovies()
        {
            var movieDetails = new List<MovieDetailsResponse>();
            var movies = await _repository.GetTopRevenueMovies();
            foreach (var movie in movies)
            {
                var movieCard = new MovieDetailsResponse
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Revenue = movie.Revenue,
                    Title = movie.Title
                };
                movieDetails.Add(movieCard);
            }

            return movieDetails;
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            return await _repository.GetTopRatedMovies();
        }
    }
}