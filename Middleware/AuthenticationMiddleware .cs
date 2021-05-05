using System.Text;  
using Microsoft.IdentityModel.Tokens;  
using Microsoft.Extensions.Configuration;  
using Microsoft.Extensions.DependencyInjection;  
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using TalktifAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.Extensions.Options;
using TalktifAPI.Dtos;

namespace TalktifAPI.Middleware
{
    public  class AuthenticationMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly JwtConfig _jwtConfig;

        public AuthenticationMiddleware (RequestDelegate next, IOptions<JwtConfig> JwtConfig)
        {
            _next = next;
            this._jwtConfig = JwtConfig.Value;
        }
        public async Task Invoke(HttpContext context,IUserRepo userRepo,IAdminRepo adminRepo)
        {
            try{
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (token != null)
                    attachUserToContext(context, token,userRepo,adminRepo);
                await _next(context);         
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
         private void attachUserToContext(HttpContext context, string token,IUserRepo userRepo,IAdminRepo adminRepo)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfig.secret);
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                string mail = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email").Value;
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,  
                    ValidateAudience = false,
                    RequireExpirationTime = true
                }, out SecurityToken validatedToken);
                jwtToken = (JwtSecurityToken)validatedToken;
                ReadUserDto r = userRepo.getInfoByEmail(mail);
                context.Items["User"] = r;  
                context.Items["IsAdmin"] = adminRepo.IsAdmin(r.Id)?1:0;
                context.Items["TokenExp"] = false; 
            }
            catch(SecurityTokenExpiredException err)
            {          
                context.Items["TokenExp"] = true;     
                Console.WriteLine(err.Message+" 1");
            }
            catch(Exception err)
            {             
                context.Items["TokenExp"] = false; 
                context.Items["User"] = null;     
                Console.WriteLine(err.Message+" 2");
            }
        }
    }
}