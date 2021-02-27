using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IAsyncRepository<Genre> _repository;

        public GenreService(IAsyncRepository<Genre> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GenreModel>> GetAllGenres()
        {
            var genres = await _repository.ListAllAsync();
            var response = new List<GenreModel>();
            foreach (var genre in genres)
                response.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            return response;
        }
    }
}