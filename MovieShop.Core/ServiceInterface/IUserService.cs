using System.Threading.Tasks;
using MovieShop.Core.Models.Request;

namespace MovieShop.Core.ServiceInterface
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserRegisterRequestModel userRequestModel);
    }
}