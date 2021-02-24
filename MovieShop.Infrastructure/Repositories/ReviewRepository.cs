using System.Data.Entity;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Infrastructure.Data;
using System.Linq;

namespace MovieShop.Infrastructure.Repositories
{
    public class ReviewRepository: EfRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Review> GetByIdAsync(int userId, int movieId)
        {
            return await db.Reviews.FindAsync(movieId, userId);
        }
    }
}