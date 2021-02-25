using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;

namespace MovieShop.Core.ServiceInterface
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserRegisterRequestModel userRequestModel);

        Task<UserLoginResponseModel> ValidateUser(string email, string password);

        Task<UserResponseModel> GetUserById(int id);
    }
}