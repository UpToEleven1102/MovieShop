using System;
using System.ComponentModel.DataAnnotations;

namespace MovieShop.Core.Models.Request
{
    public class UserRegisterRequestModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string RePassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }

    public class UserLoginRequestModel
    {
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ReviewRequestModel
    {
        [Required]
        [Range(0, 10)]
        public decimal Rating { get; set; }

        public string ReviewText { get; set; }

        [Required]
        public int MovieId { get; set; }
    }


    public class MovieRequestModel
    {
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }
        
        public string Overview { get; set; }
        
        [MaxLength(512)]
        public string Tagline { get; set; }
        
        public decimal? Budget { get; set; }
        
        public decimal? Revenue { get; set; }
        public string ImdbUrl { get; set; }
        
        public string TmdbUrl { get; set; }
        
        public string PosterUrl { get; set; }
        public string BackdropUrl { get; set; }
        public string OriginalLanguage { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        
    }
}