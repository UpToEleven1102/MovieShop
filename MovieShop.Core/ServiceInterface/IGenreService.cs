using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Response;

namespace MovieShop.Core.ServiceInterface
{
    public interface IGenreService
    {
        public Task<IEnumerable<GenreModel>> GetAllGenres();
    }
}