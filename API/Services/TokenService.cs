
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])); //SymmetricSecurityKey --> take lat of byte   
        }
        public string CreateToken(AppUser user)
        {
            //list of all claim
            var claims = new List<Claim> //do list of claims because can be more than one
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
                
            };
            //signing credentials
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); //(_key, algorithms_used.spesify)
            //describe the token that return
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            // token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            //create our token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}