using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;

namespace MovieShop.Core.ServiceInterface
{
    public interface IMovieService
    {
        public Task<MovieDetailsResponse> GetMovieById(int id);
        
        Task<IEnumerable<MovieDetailsResponse>> GetTopGrossingMovies();

        Task<IEnumerable<Movie>> GetTopRatedMovies();
    }
}