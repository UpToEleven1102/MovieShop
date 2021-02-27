using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;

namespace MovieShop.Core.ServiceInterface
{
    public interface IMovieService
    {
        public Task<MovieDetailsResponse> GetMovieById(int id);
        
        Task<IEnumerable<MovieDetailsResponse>> GetTopGrossingMovies();

        Task<IEnumerable<Movie>> GetTopRatedMovies();

        Task<MovieDetailsResponse> BuyMovie(int id);

        Task<bool> PostReview(ReviewRequestModel requestModel);

        Task<PaginationResponse<MovieDetailsResponse>> GetMoviesPaginated(int pageNumber, int pageSize);
        Task<IEnumerable<MovieDetailsResponse>> GetMoviesByGenreId(int genreId);

        Task<IEnumerable<ReviewResponseModel>> GetReviewsByMovieId(int movieId);
    }
}