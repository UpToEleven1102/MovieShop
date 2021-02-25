using System;
using System.Collections.Generic;

namespace MovieShop.Core.Models.Response
{
    public class UserLoginResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<RoleModel> Roles { get; set; }
    }

    public class RoleModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class UserResponseModel
    {
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
    }

    public class ReviewResponseModel
    {
        public decimal Rating { get; set; }

        public string ReviewText { get; set; }
    }
}