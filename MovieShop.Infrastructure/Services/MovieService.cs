using System.Collections.Generic;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService: IMovieService
    {
        private readonly IMovieRepository _repository;
        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public MovieDetailsResponse GetMovieById(int id)
        {
            MovieDetailsResponse movieDetail = new MovieDetailsResponse();
            var movie = _repository.GetByIdAsync(id);
            movieDetail.Id = movie.Id;
            // finish mapping movieDetail here
            return movieDetail;
        }

        public IEnumerable<MovieDetailsResponse> GetTopGrossingMovies()
        {
            List<MovieDetailsResponse> movieDetails = new List<MovieDetailsResponse>();
            var movies = _repository.GetTopRevenueMovies();
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

        public IEnumerable<Movie> GetTopRatedMovies()
        {
            return _repository.GetTopRatedMovies();
        }
    }
}