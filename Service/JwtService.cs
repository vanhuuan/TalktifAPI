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
        private readonly IUserRefreshTokenRepository _context;

        public JwtService(IConfiguration config,IUserRefreshTokenRepository context)  
        {  
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;  
            _secret2 = config.GetSection("JwtConfig").GetSection("secret2").Value;  
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInHours").Value;  
            _expMonth = config.GetSection("JwtConfig").GetSection("expirationInMonths").Value;  
            _context = context;
        }

        public string GenerateRefreshToken(int user)
        {
            int id = _context.GetLastIdToken(); id++;
            var tokenHandler = new JwtSecurityTokenHandler();  
            var key = Encoding.ASCII.GetBytes(_secret2);  
            var tokenDescriptor = new SecurityTokenDescriptor  
            {    
                Subject = new ClaimsIdentity(new[]  
                {  
                    new Claim(ClaimTypes.Email, id.ToString()), 
                    new Claim(ClaimTypes.Name, user.ToString())  
                }),  
                IssuedAt = DateTime.Now,
                Expires = DateTime.UtcNow.AddMonths(Int32.Parse(_expMonth)),  
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)  
            };  
  
            var token = tokenHandler.CreateToken(tokenDescriptor);  
            return tokenHandler.WriteToken(token);  
        }

        public string GenerateSecurityToken(int id)  
        {  
            var tokenHandler = new JwtSecurityTokenHandler();  
            var key = Encoding.ASCII.GetBytes(_secret);  
            var tokenDescriptor = new SecurityTokenDescriptor  
            {  
                Subject = new ClaimsIdentity(new[]  
                {  
                    new Claim(ClaimTypes.Email, id.ToString())  
                }),  
                IssuedAt = DateTime.Now,
                Expires = DateTime.UtcNow.AddHours(double.Parse(_expDate)),  
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)  
            };   
            var token = tokenHandler.CreateToken(tokenDescriptor);  
            return tokenHandler.WriteToken(token);  
        }

        public int GetId(string token)
        {
            try{
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                return Convert.ToInt32(jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value);
            }catch(Exception e){
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool ValidRefreshToken(String _token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(_token) as JwtSecurityToken;
                UserRefreshToken token = new UserRefreshToken{
                    RefreshToken = _token,
                    Id = Convert.ToInt32(jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email").Value),
                    User = Convert.ToInt32(jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value)
                };
                var key = Encoding.ASCII.GetBytes(_secret2);
                tokenHandler.ValidateToken(token.RefreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                }, out SecurityToken validatedRefreshToken);     
                var t = _context.GetById(token.Id);       
                if(t==null||t.User!=token.User||String.Compare(t.RefreshToken,_token)!=0) throw new Exception("Token doesn't exist");
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