using System.Threading.Tasks;
using MovieShop.Core.Entities;

namespace MovieShop.Core.RepositoryInterface
{
    public interface IUserRepository: IAsyncRepository<User>
    {
        public Task<User> GetUserByEmail(string email);

    }
}