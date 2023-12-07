using Entities.Models;
using HealthWebApi.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthWebApi.Services
{
    public class TokenService : ITokenServices
    {
        public string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("Id", user.Id),
                new Claim("UserName", user.UserName),
                new Claim("Name", user.Name),
                new Claim("role", user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("K48vjsn4Af8nmruicIw9c87ermQ38dmUe9"));
            var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    expires: DateTime.Now.AddHours(6),
                    claims: claims,
                    signingCredentials: signInCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
       


    }
}
