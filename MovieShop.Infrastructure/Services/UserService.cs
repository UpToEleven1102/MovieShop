using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Exceptions;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ICryptoService _cryptoService;
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository, ICryptoService cryptoService)
        {
            _repository = repository;
            _cryptoService = cryptoService;
        }

        public async Task<bool> RegisterUser(UserRegisterRequestModel userRequestModel)
        {
            var dbUser = await _repository.GetUserByEmail(userRequestModel.Email);
            if (dbUser != null) throw new ConflictException("Email already exists");

            var salt = _cryptoService.GenerateRandomSalt();
            var hashedPassword = _cryptoService.HashPasswordWithSalt(userRequestModel.Password, salt);

            // save user to the database
            var user = new User
            {
                Email = userRequestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = userRequestModel.FirstName,
                LastName = userRequestModel.LastName,
                DateOfBirth = userRequestModel.DateOfBirth
            };

            var createdUser = _repository.AddAsync(user);

            if (createdUser != null && createdUser.Id > 0) return true;

            return false;
        }

        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            var user = await _repository.GetUserByEmail(email);
            if (user == null) return null;

            var hashedPassword = _cryptoService.HashPasswordWithSalt(password, user.Salt);
            if (hashedPassword == user.HashedPassword)
            {
                var responseUser = new UserLoginResponseModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth
                };
                responseUser.Roles = new List<RoleModel>();
                foreach (var role in user.Roles)
                    responseUser.Roles.Add(new RoleModel
                    {
                        Id = role.Id,
                        Name = role.Name
                    });

                return responseUser;
            }

            return null;
        }

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var dbUser = await _repository.GetByIdAsync(id);
            
            return new UserResponseModel()
            {
                Id = dbUser.Id,
                Email = dbUser.Email,
                PhoneNumber = dbUser.PhoneNumber,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                DateOfBirth = dbUser.DateOfBirth,
            };

        }
    }
}