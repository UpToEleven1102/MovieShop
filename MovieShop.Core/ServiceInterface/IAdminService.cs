using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;

namespace MovieShop.Core.ServiceInterface
{
    public interface IAdminService
    {
        Task<PaginationResponse<Purchase>> GetAllPurchases(int pageNumber = 0, int pageSize = 30);

        Task<Movie> CreateMovie(MovieRequestModel movie);

        Task<Movie> UpdateMovie(int id, MovieRequestModel movie);
    }
}