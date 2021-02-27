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

        public string Email =>
            _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        public List<string> Roles => GetUserRoles();
        public bool IsAdmin => GetUserRoles().Contains("admin");
        public int UserId => GetUserId();

        private int GetUserId()
        {
            var idClaim = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return idClaim != null ? int.Parse(idClaim.Value) : -1;
        }

        private string GetFullName()
        {
            var firstName = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)
                ?.Value;
            var lastName = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)
                ?.Value;

            return lastName + ", " + firstName;
        }

        private List<string> GetUserRoles()
        {
            var response = new List<string>();
            
            var roles = _httpContext.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            foreach (var role in roles)
            {
                response.Add(role.Value);
            }
            return response;
        }
    }
}