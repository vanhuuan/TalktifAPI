using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalktifAPI.Data;
using TalktifAPI.Dtos;
using TalktifAPI.Models;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace TalktifAPI.Controllers
{
    
    [Route("api/[controller]")]  
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _repository;

        public UsersController(IUserRepo repository)
        {
            _repository = repository;
        }
        [HttpPost]        
        [Route("SignUp")]
        public ActionResult<SignUpRespond> signUp(SignUpRequest user)
        {
            try{
                SignUpRespond r = _repository.signUp(user);
                _repository.saveChange();
                setTokenCookie(r.RefreshToken);
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
                LoginRespond r = _repository.signIn(user);
                if(r!=null) setTokenCookie(r.RefreshToken);
                _repository.saveChange();
                return Ok(r);
            }catch(Exception){
                return NotFound();
            }
        }
        [HttpPost]
        [Route("ResetPass")]
        public ActionResult<ReadUserDto> ResetPassword(ResetPassRequest user)
        {
            try{
                LoginRespond r = _repository.resetPass(user.Email,user.NewPass);
                _repository.saveChange();
                return Ok(r);
            }catch(Exception){
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize]
        [Route("{email}")]
        public ActionResult<ReadUserDto> getUserInfo(string email)
        {
            try{
                if(email == null) NotFound();
                return Ok(_repository.getInfoByEmail(email));
            }catch(Exception){
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost]  
        [Route("UpdateInfo")]
        public ActionResult<ReadUserDto> updateUserInfo(UpdateInfoRequest update){
            try{
                return _repository.updateInfo(update);
            }catch(Exception){
                return NotFound();
            }
        }
        [Authorize]
        [HttpGet] 
        [Route("InActiveUser")]
        public ActionResult InActiveUser (string email){
            try{
                _repository.inActiveUser(email);
                _repository.saveChange();
                return Ok();
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return NotFound();
            }
        }
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(RefreshTokenRequest request)
        {
            try{
            var refreshToken = Request.Cookies["RefreshToken"].ToString();
            var response = _repository.RefreshToken(refreshToken,request.email);
            _repository.saveChange();
            return Ok(response);
            }catch(Exception e){
                Console.Write(e.Message);
                return Unauthorized();
            }
        }
        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMonths(1)
            };
            Response.Cookies.Append("RefreshToken", token, cookieOptions);
        }
    }
}
