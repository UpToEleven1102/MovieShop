using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.Infrastructure.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContext;

        public CurrentUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public bool IsAuthenticated => _httpContext.HttpContext.User.Identity.IsAuthenticated;

        public string FullName => GetFullName();
        
        public string Email { get; }
        public List<string> Roles { get; }
        public bool IsAdmin { get; }
        public int UserId { get; }

        private string GetFullName()
        {
            var firstName = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)
                ?.Value;
            var lastName = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;

            return lastName + ", " + firstName;
        }
    }
}