using MovieShop.Core.Models.Response;

namespace MovieShop.Core.ServiceInterface
{
    public interface IJwtService
    {
        string GenerateJwtToken(UserLoginResponseModel userLoginModel);
    }
}