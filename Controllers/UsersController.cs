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

namespace TalktifAPI.Controllers
{
    
    [Route("api/[controller]")]  
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IEmailService _emailService;

        public UsersController(IUserService service,IEmailService emailService)
        {
            _service = service;
            _emailService = emailService;
        }
        [HttpPost]        
        [Route("SignUp")]
        public ActionResult<SignUpRespond> signUp(SignUpRequest user)
        {
            try{
                SignUpRespond r = _service.signUp(user);
                MailContent content = new MailContent {
                    To = user.Email,
                    Subject = "Confirm Email",
                    Body = "<h3><strong>Xin chào "+user.Name+" </strong></h3><p>Cảm ơn bạn vừa đăng ký tài khoản Talktif, nhập mã code này để hoàn tấy đăng ký : " + r.RefreshTokenId + ""  + r.RefreshToken+"</p>"
                };
                Console.WriteLine(content.Body);
                _emailService.SendMail(content);
                return Ok(r);
            }catch(Exception e){
                Console.WriteLine(e.ToString());
                return NoContent();
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
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("ResetPass")]
        public ActionResult ResetPassword(ResetPassRequest user)
        {
            try{
                if(_service.getInfoByEmail(user.Email)==null) throw new Exception("Email is not exist");
                Random random = new Random();
                int num = random.Next(1000,9999);
                MailContent content = new MailContent {
                    To = user.Email,
                    Subject = "Reset Password Email",
                    Body = "<h3><strong>Xin chào</strong></h3><p>Bạn vừa thay đổi mật  khẩu tài khoản Talktif, mã xác nhận của bạn là : "+num+"</p>"
                };
                _emailService.SendMail(content);
                return Ok(num);
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route("ActiveEmail")]
        public ActionResult ActiveEmail(ActiveEmailRequest request){
            try{
                if(_service.ActiveEmail(request.Token,request.Id)){
                    return Ok();
                }
                else return Unauthorized();
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return Unauthorized();
            }
        }
        [HttpGet]
        [Route("ReserPasswordEmail")]
        public ActionResult<ReadUserDto> ResetPasswordEmail(ResetPassEmailRequest request){
            try{
                LoginRespond r = _service.resetPass(request.Email,request.NewPass);
                return Ok(r);
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return Unauthorized();
            }
        }
        [HttpPost]
        [Authorize]
        [Route("{id}")]
        public ActionResult<ReadUserDto> getUserInfo(int id)
        {
            try{
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
                _service.inActiveUser(id);
                return Ok();
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpPost("Refresh-Token")]
        public IActionResult RefreshToken(RefreshTokenRequest request)
        {
            try{
            var refreshToken = Request.Cookies["RefreshToken"].ToString();
            var refreshTokenId = Request.Cookies["RefreshTokenId"].ToString();
            var response = _service.RefreshToken(new ReFreshToken{
                    Id = Convert.ToInt32(refreshTokenId),
                    RefreshToken = refreshToken
            });
            return Ok(response);
            }catch(Exception e){
                Console.Write(e.Message);
                return Unauthorized();
            }
        }
        [HttpPost("Report")]
        public IActionResult Report(ReportRequest request)
        {
            try{
            var respond = _service.Report(request);
            return Ok();
            }catch(Exception e){
                Console.Write(e.Message);
                return Unauthorized();
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
    }
}
