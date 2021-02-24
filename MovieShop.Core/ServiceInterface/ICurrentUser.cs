using System.Collections.Generic;

namespace MovieShop.Core.ServiceInterface
{
    public interface ICurrentUser
    {
        public bool IsAuthenticated { get; }
        
        public string FullName { get; }
        
        public string Email { get; }

        public List<string> Roles { get; }
        
        public bool IsAdmin { get; }
        
        public int UserId { get; }
    }
}