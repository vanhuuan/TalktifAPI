using System.Text;  
using Microsoft.IdentityModel.Tokens;  
using Microsoft.Extensions.Configuration;  
using Microsoft.Extensions.DependencyInjection;  
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.Extensions.Options;
using TalktifAPI.Dtos;
using TalktifAPI.Service;

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
        public async Task Invoke(HttpContext context,IUserService userService,IAdminService adminService,IJwtService jwtService)
        {
            try{
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (token != null)
                    attachUserToContext(context, token,userService,adminService,jwtService);
                await _next(context);         
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
         private void attachUserToContext(HttpContext context, string token,IUserService userRepo,IAdminService adminRepo,IJwtService jwtService)
        {
            try
            {
                jwtService.ValidSecurityToken(token);
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                int id = jwtService.GetId(token);          
                var user = userRepo.getInfoById(id);
                context.Items["IsAdmin"] = user.IsAdmin==true?1:0;
                context.Items["TokenExp"] = false; 
            }
            catch(SecurityTokenExpiredException err)
            {          
                context.Items["TokenExp"] = true; 
                context.Items["IsAdmin"] = 0;    
                Console.WriteLine(err.Message+" 1");
            }
            catch(Exception err)
            {             
                context.Items["TokenExp"] = true;  
                context.Items["IsAdmin"] = 0;    
                Console.WriteLine(err.Message+" 2");
            }
        }
    }
}