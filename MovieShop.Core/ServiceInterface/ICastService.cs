using System.Threading.Tasks;
using MovieShop.Core.Entities;

namespace MovieShop.Core.ServiceInterface
{
    public interface ICastService
    {
        Task<Cast> GetCastById(int id);
    }
}