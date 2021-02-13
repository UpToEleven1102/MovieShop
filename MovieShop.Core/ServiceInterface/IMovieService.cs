using System.Collections;
using System.Collections.Generic;
using MovieShop.Core.Entities;

namespace MovieShop.Core.ServiceInterface
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetTopGrossingMovies();

        IEnumerable<Movie> GetTopRatedMovies();
    }
}