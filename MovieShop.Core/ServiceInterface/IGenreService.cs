using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;

namespace MovieShop.Core.ServiceInterface
{
    public interface IGenreService
    {
        public Task<IEnumerable<Genre>> GetAllGenres();
    }
}