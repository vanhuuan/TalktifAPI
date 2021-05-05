using System;  
using System.Text;  
using System.Security.Claims;  
using Microsoft.IdentityModel.Tokens;  
using System.IdentityModel.Tokens.Jwt;  
using Microsoft.Extensions.Configuration;  

namespace TalktifAPI.Data
{
    public class JwtRepo : IJwtRepo
    {
        private readonly string _secret;
        private readonly string _secret2;
        private readonly string _expDate;
        private readonly string _expMonth;

        public JwtRepo(IConfiguration config)  
        {  
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;  
            _secret2 = config.GetSection("JwtConfig").GetSection("secret2").Value;  
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInHours").Value;  
            _expMonth = config.GetSection("JwtConfig").GetSection("expirationInMonths").Value;  
        }

        public string GenerateRefreshToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();  
            var key = Encoding.ASCII.GetBytes(_secret2);  
            var tokenDescriptor = new SecurityTokenDescriptor  
            {  
                Subject = new ClaimsIdentity(new[]  
                {  
                    new Claim(ClaimTypes.Email, email)  
                }),  
                IssuedAt = DateTime.Now,
                Expires = DateTime.UtcNow.AddHours(double.Parse(_expMonth)),  
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)  
            };  
  
            var token = tokenHandler.CreateToken(tokenDescriptor);  
            return tokenHandler.WriteToken(token);  
        }

        public string GenerateSecurityToken(string email)  
        {  
            var tokenHandler = new JwtSecurityTokenHandler();  
            var key = Encoding.ASCII.GetBytes(_secret);  
            var tokenDescriptor = new SecurityTokenDescriptor  
            {  
                Subject = new ClaimsIdentity(new[]  
                {  
                    new Claim(ClaimTypes.Email, email)  
                }),  
                IssuedAt = DateTime.Now,
                Expires = DateTime.UtcNow.AddHours(double.Parse(_expDate)),  
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)  
            };  
  
            var token = tokenHandler.CreateToken(tokenDescriptor);  
            return tokenHandler.WriteToken(token);  
        }  
    }
}