using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalktifAPI.Dtos;
using TalktifAPI.Models;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using TalktifAPI.Service;
using TalktifAPI.Dtos.User;
using System.Linq;

namespace TalktifAPI.Controllers
{
    
    [Route("api/[controller]")]  
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;

        public UsersController(IUserService service,IEmailService emailService,IJwtService jwtService)
        {
            _service = service;
            _emailService = emailService;
            _jwtService = jwtService;
        }
        [HttpPost]        
        [Route("SignUp")]
        public ActionResult<SignUpRespond> signUp(SignUpRequest user)
        {
            try{
                SignUpRespond r = _service.signUp(user);
                // MailContent content = new MailContent {
                //     To = user.Email,
                //     Subject = "Confirm Email",
                //     Body = "<h3><strong>Xin chào "+user.Name+" </strong></h3><p>Cảm ơn bạn vừa đăng ký tài khoản Talktif, nhập mã code này để hoàn tấy đăng ký : " + r.RefreshTokenId + ""  + r.RefreshToken+"</p>"
                // };
                // Console.WriteLine(content.Body);
                // _emailService.SendMail(content);
                if(r!=null) setTokenCookie(r.RefreshToken,r.RefreshTokenId);
                return Ok(r);
            }catch(Exception e){
                Console.WriteLine(e.ToString());
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("SignIn")]
        public ActionResult<ReadUserDto> signIn(LoginRequest user)
        {
            try{
                LoginRespond r = _service.signIn(user);
                if(r!=null) setTokenCookie(r.RefreshToken,r.RefreshTokenId);
                return Ok(r);
            }catch(Exception e){
                 Console.WriteLine(e.ToString());
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("ResetPass")]
        public ActionResult ResetPassword(ResetPassRequest user)
        {
            try{
                var u = _service.getInfoByEmail(user.Email);
                if(u==null) throw new Exception("Email is not exist");
                // Random random = new Random();
                // int num = random.Next(1000,9999);
                // MailContent content = new MailContent {
                //     To = user.Email,
                //     Subject = "Reset Password Email",
                //     Body = "<h3><strong>Xin chào</strong></h3><p>Bạn vừa thay đổi mật  khẩu tài khoản Talktif, mã xác nhận của bạn là : "+num+"</p>"
                // };
                // _emailService.SendMail(content);
                return Ok(u.Hobbies);
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        // [HttpPost]
        // [Route("ActiveEmail")]
        // public ActionResult ActiveEmail(ActiveEmailRequest request){
        //     try{
        //         if(_service.ActiveEmail(request.Token,request.Id)){
        //             return Ok();
        //         }
        //         else return Unauthorized();
        //     }catch(Exception e){
        //         Console.WriteLine(e.Message);
        //         return Unauthorized(e);
        //     }
        // }
        [HttpPost]
        [Route("ResetPasswordEmail")]
        public ActionResult<ReadUserDto> ResetPasswordEmail(ResetPassEmailRequest request){
            try{
                LoginRespond r = _service.resetPass(request.Email,request.NewPass);
                return Ok(r);
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("GetAllCountry")]
        public ActionResult<Country> GetAllCountry(){
            try{
                return Ok(_service.GetAllCountry());
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("GetAllCityCountry/{id}")]
        public ActionResult<City> GetAllCityCountry(int id){
            try{
                return Ok(_service.GettCityByCountry(id));
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Authorize]
        [Route("{id}")]
        public ActionResult<ReadUserDto> getUserInfo(int id)
        {
            try{
                CheckId(id);
                return Ok(_service.getInfoById(id));
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpPost]  
        [Route("UpdateInfo")]
        public ActionResult<ReadUserDto> updateUserInfo(UpdateInfoRequest update){
            try{
                CheckId(update.Id);
                return _service.updateInfo(update);
            }catch(Exception e){
                return NotFound(e.Message);
            }
        }
        [Authorize]
        [HttpGet] 
        [Route("InActiveUser/{id}")]
        public ActionResult InActiveUser (int id){
            try{
                CheckId(id);
                _service.inActiveUser(id);
                return Ok();
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken(RefreshTokenRequest request)
        {
            try{
                var refreshToken = Request.Cookies["RefreshToken"].ToString();
                var refreshTokenId = Request.Cookies["RefreshTokenId"].ToString();
                var response = _service.RefreshToken(new ReFreshToken{
                    Id = Convert.ToInt32(refreshTokenId),
                    RefreshToken = refreshToken
                },GetId());
            return Ok(response);
            }catch(Exception e){
                Console.Write(e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost("Report")]
        [Authorize]
        public IActionResult Report(ReportRequest request)
        {
            try{
                CheckId(request.Reporter);
                var respond = _service.Report(request);
                return Ok();
            }catch(Exception e){
                Console.Write(e.Message);
                return BadRequest(e.Message);
            }
        }
        private void setTokenCookie(string token,int id)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMonths(1)
            };
            Response.Cookies.Append("RefreshToken", token, cookieOptions);
            Response.Cookies.Append("RefreshTokenId", id.ToString(), cookieOptions);
        }
        public int GetId(){
            String token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            return _jwtService.GetId(token);
        }
        public void CheckId(int id){
            if(GetId()!=id) throw new Exception("You don't have permission to do this action");
        }
    }
}
