using System.Collections;
using System.Collections.Generic;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;

namespace MovieShop.Core.ServiceInterface
{
    public interface IMovieService
    {
        public MovieDetailsResponse GetMovieById(int id);
        
        IEnumerable<MovieDetailsResponse> GetTopGrossingMovies();

        IEnumerable<Movie> GetTopRatedMovies();
    }
}