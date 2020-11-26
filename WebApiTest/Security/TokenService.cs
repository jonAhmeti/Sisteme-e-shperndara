using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApiTest.Models;

namespace WebApiTest.Security
{
    public class TokenService
    {
        public readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //public readonly string Secret;

        //public TokenService(string secret)
        //{
        //    Secret = secret;
        //}

        public string GenerateToken(User user, int expireInMinutes = 20)
        {
            var secretBytes = Encoding.ASCII.GetBytes(_configuration.GetSection("Secret").Value);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SymmetricSecurityKey key = new SymmetricSecurityKey(secretBytes);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("username", user.Username),
                    new Claim("password", user.Password), 
                    new Claim("id", user.Id.ToString()),
                    new Claim("role", user.RoleId.ToString())
                }),
                Expires = DateTime.Now.AddMinutes(expireInMinutes),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

            return handler.WriteToken(token);
        }
    }
}
