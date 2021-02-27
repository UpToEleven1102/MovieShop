using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Core.ServiceInterface;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly MovieShopDbContext _dbContext;
        private readonly IMovieRepository _movieRepository;

        public AdminService(MovieShopDbContext dbContext,
            IMovieRepository movieRepository)
        {
            _dbContext = dbContext;
            _movieRepository = movieRepository;
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

        public Task<Movie> CreateMovie(MovieRequestModel requestModel)
        {
            var movie = new Movie()
            {
                Title = requestModel.Title,
                Overview = requestModel.Overview,
                Tagline = requestModel.Tagline,
                Budget = requestModel.Budget,
                Revenue = requestModel.Revenue,
                TmdbUrl = requestModel.TmdbUrl,
                PosterUrl = requestModel.PosterUrl,
                BackdropUrl = requestModel.BackdropUrl,
                OriginalLanguage = requestModel.OriginalLanguage,
                ReleaseDate = requestModel.ReleaseDate
            };

            return _movieRepository.AddAsync(movie);
        }

        public Task<Movie> UpdateMovie(int id, MovieRequestModel requestModel)
        {
            var movie = new Movie()
            {
                Id = id,
                Title = requestModel.Title,
                Overview = requestModel.Overview,
                Tagline = requestModel.Tagline,
                Budget = requestModel.Budget,
                Revenue = requestModel.Revenue,
                TmdbUrl = requestModel.TmdbUrl,
                PosterUrl = requestModel.PosterUrl,
                BackdropUrl = requestModel.BackdropUrl,
                OriginalLanguage = requestModel.OriginalLanguage,
                ReleaseDate = requestModel.ReleaseDate
            };

            return _movieRepository.UpdateAsync(movie);
        }
    }
}