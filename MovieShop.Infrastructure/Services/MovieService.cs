using System.Collections.Generic;
using MovieShop.Core.Entities;
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
        
        public IEnumerable<Movie> GetTopGrossingMovies()
        {
            return _repository.GetTopRevenueMovies();
        }

        public IEnumerable<Movie> GetTopRatedMovies()
        {
            return _repository.GetTopRatedMovies();
        }
    }
}