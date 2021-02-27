using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;

namespace MovieShop.Core.RepositoryInterface
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetTopRevenueMovies();

        Task<IEnumerable<Movie>> GetTopRatedMovies();
        
        Task<PaginationResponse<Movie>> GetMoviesByGenreId(int genreId, int pageNumber, int pageSize);

        Task<PaginationResponse<Movie>> GetMoviesPaginated(int pageNumber, int pageSize);
    }
}