using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Core.ServiceInterface;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Services
{
    public class AdminService: IAdminService 
    {
        private readonly MovieShopDbContext _dbContext;

        public AdminService(IAsyncRepository<Purchase> repository, MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginationResponse<Purchase>> GetAllPurchases(int pageNumber = 0, int pageSize = 30)
        {
            var pageCount = await _dbContext.Purchases.CountAsync() / pageSize;

            if (pageNumber >= pageCount) pageNumber = pageCount;

            return new PaginationResponse<Purchase>
            {
                PageCount = pageCount + 1,
                PageNumber = pageNumber + 1,
                PageSize = pageSize,
                Data = await _dbContext.Purchases.OrderByDescending(p => p.PurchaseDateTime)
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize).ToListAsync()
            };
        }
    }
}