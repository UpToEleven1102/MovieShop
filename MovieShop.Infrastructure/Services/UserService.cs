using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Exceptions;
using MovieShop.Core.Models.Request;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.Infrastructure.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ICryptoService _cryptoService;

        public UserService(IUserRepository repository, ICryptoService cryptoService)
        {
            _repository = repository;
            _cryptoService = cryptoService;
        }

        public async Task<bool> RegisterUser(UserRegisterRequestModel userRequestModel)
        {
            var dbUser = await _repository.GetUserByEmail(userRequestModel.Email);
            if (dbUser != null)
            {
                throw new ConflictException("Email already exists");
            }

            var salt = _cryptoService.GenerateRandomSalt();
            var hashedPassword = _cryptoService.HashPasswordWithSalt(userRequestModel.Password, salt);

            // save user to the database
            var user = new User()
            {
                Email = userRequestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = userRequestModel.FirstName,
                LastName = userRequestModel.LastName,
                DateOfBirth = userRequestModel.DateOfBirth,
            };

            var createdUser = _repository.AddAsync(user);

            if (createdUser != null && createdUser.Id > 0)
            {
                return true;
            }

            return false;
        }
    }
}