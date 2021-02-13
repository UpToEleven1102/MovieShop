using System.Collections;
using System.Collections.Generic;
using MovieShop.Core.Entities;

namespace MovieShop.Core.RepositoryInterface
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetTopRevenueMovies();

        IEnumerable<Movie> GetTopRatedMovies();
    }
}