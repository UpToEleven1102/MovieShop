using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Exceptions;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;

        public AccountController(ICurrentUser currentUser, IUserService userService)
        {
            _currentUser = currentUser;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            return Ok(_currentUser);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRegisterRequestModel userRequestModel)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid params!");

            try
            {
                var registerRes = await _userService.RegisterUser(userRequestModel);
                return StatusCode(registerRes ? 201 : 500);
            }
            catch (ConflictException ex)
            {
                return BadRequest("Email is already used!");
            }
            catch (Exception e)
            {
                // LogException
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        [Route("login")]
        public Task<IActionResult> Login(UserLoginRequestModel userModel)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            return user == null ? NotFound("No User Found!") : Ok(user);
        }
    }
}