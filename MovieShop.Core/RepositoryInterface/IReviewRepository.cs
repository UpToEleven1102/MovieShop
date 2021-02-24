using System.Threading.Tasks;
using MovieShop.Core.Entities;

namespace MovieShop.Core.RepositoryInterface
{
    public interface IReviewRepository: IAsyncRepository<Review>
    {
        Task<Review> GetByIdAsync(int userId, int movieId);
    }
}