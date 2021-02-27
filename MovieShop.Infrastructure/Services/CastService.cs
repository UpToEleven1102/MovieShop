using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.Infrastructure.Services
{
    public class CastService: ICastService
    {
        private readonly IAsyncRepository<Cast> _repository;

        public CastService(IAsyncRepository<Cast> repository)
        {
            _repository = repository;
        }

        public Task<Cast> GetCastById(int id)
        {
            return _repository.GetByIdAsync(id);
        }
    }
}