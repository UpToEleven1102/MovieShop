using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;

namespace MovieShop.Core.RepositoryInterface
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetTopRevenueMovies();

        Task<IEnumerable<Movie>> GetTopRatedMovies();
        
        Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId);
    }
}