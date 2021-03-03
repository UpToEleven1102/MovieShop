using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieShop.Core.Models.Response;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.Infrastructure.Services
{
    public class JwtService: IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(UserLoginResponseModel userLoginModel)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userLoginModel.Email),
                new Claim(ClaimTypes.GivenName, userLoginModel.FirstName),
                new Claim(ClaimTypes.Surname, userLoginModel.LastName),
                new Claim(ClaimTypes.DateOfBirth, userLoginModel.DateOfBirth.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userLoginModel.Id.ToString()),
            };

            foreach (var role in userLoginModel.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var securityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenSettings:PrivateKey"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            
            var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<double>("TokenSettings:ExpirationHours"));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenObject = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _configuration["TokenSettings:Issuer"],
                Audience = _configuration["TokenSettings:Audience"],
            };

            var encodedJwt = tokenHandler.CreateToken(tokenObject);

            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}