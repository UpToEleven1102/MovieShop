using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetTopRevenueMovies()
        {
            return await db.Movies.OrderByDescending(m => m.Revenue).Take(25).ToListAsync();
        }

        public Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId)
        {
            var genre = await db.Genres.Where(g => g.Id == genreId).Include(g => g.Movies).FirstOrDefaultAsync();
            return genre?.Movies;
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            return await db.Movies.Include(m => m.MovieCasts)
                .ThenInclude(mc => mc.Cast)
                .Include(m => m.Genres)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PaginationResponse<Movie>> GetMoviesPaginated(int pageNumber = 0, int pageSize = 30)
        {
            int pageCount = await db.Movies.CountAsync() / pageSize;

            if (pageNumber >= pageCount) pageNumber = pageCount - 1;

            return new PaginationResponse<Movie>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = await db.Movies.Skip(pageNumber * pageSize).Take(pageSize).Include(m => m.MovieCasts)
                    .ThenInclude(mc => mc.Cast)
                    .Include(m => m.Genres)
                    .Include(m => m.Reviews).ToListAsync(),
                PageCount = pageCount
            };
        }
    }
}