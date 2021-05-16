using System;  
using System.Text;  
using System.Security.Claims;  
using Microsoft.IdentityModel.Tokens;  
using System.IdentityModel.Tokens.Jwt;  
using Microsoft.Extensions.Configuration;
using TalktifAPI.Repository;
using TalktifAPI.Models;
using System.Linq;

namespace TalktifAPI.Service
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _secret2;
        private readonly string _expDate;
        private readonly string _expMonth;
        private readonly IGenericRepository<UserRefreshToken> _context;

        public JwtService(IConfiguration config,IGenericRepository<UserRefreshToken> context)  
        {  
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;  
            _secret2 = config.GetSection("JwtConfig").GetSection("secret2").Value;  
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInHours").Value;  
            _expMonth = config.GetSection("JwtConfig").GetSection("expirationInMonths").Value;  
            _context = context;
        }

        public string GenerateRefreshToken(bool IsAdmin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();  
            var key = Encoding.ASCII.GetBytes(_secret2);  
            var tokenDescriptor = new SecurityTokenDescriptor  
            {    
                Subject = new ClaimsIdentity(new[]  
                {  
                    new Claim(ClaimTypes.Role, IsAdmin==true?"Admin":"User")  
                }),  
                IssuedAt = DateTime.Now,
                Expires = DateTime.UtcNow.AddHours(double.Parse(_expMonth)),  
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)  
            };  
  
            var token = tokenHandler.CreateToken(tokenDescriptor);  
            return tokenHandler.WriteToken(token);  
        }

        public string GenerateSecurityToken(bool IsAdmin)  
        {  
            var tokenHandler = new JwtSecurityTokenHandler();  
            var key = Encoding.ASCII.GetBytes(_secret);  
            var tokenDescriptor = new SecurityTokenDescriptor  
            {  
                Subject = new ClaimsIdentity(new[]  
                {  
                    new Claim(ClaimTypes.Role, IsAdmin==true?"Admin":"User")  
                }),  
                IssuedAt = DateTime.Now,
                Expires = DateTime.UtcNow.AddHours(double.Parse(_expDate)),  
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)  
            };   
            var token = tokenHandler.CreateToken(tokenDescriptor);  
            return tokenHandler.WriteToken(token);  
        }

        public bool GetRole(string token)
        {
            try{
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                return jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value == "Admin"?true:false;
            }catch(Exception e){
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool ValidRefreshToken(UserRefreshToken token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secret2);
                tokenHandler.ValidateToken(token.RefreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                }, out SecurityToken validatedRefreshToken);            
                if(_context.GetById(token.Id)==null) throw new Exception("Token doesn't exist");
                return true;
            }catch(SecurityTokenExpiredException e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool ValidSecurityToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);
                return true;
            }catch(SecurityTokenExpiredException e)
            {
                throw new Exception(e.Message);
            }catch(Exception e){
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}