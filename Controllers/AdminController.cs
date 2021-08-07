
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;  
using Microsoft.Extensions.Configuration;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.Admin;
using TalktifAPI.Models;
using TalktifAPI.Service;

namespace TalktifAPI.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]  
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private IConfiguration _config;

        public AdminController(IAdminService adminService,IConfiguration configuration)
        {
            _adminService = adminService;
            _config = configuration;
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        [Route("Count")]
        public ActionResult<Counts> Count(){
            try{              
                return Ok(_adminService.GetNumOfReCord());
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        [Route("GetAllUser/{top}/{orderby}/{filter}/{search}")]
        public ActionResult<List<ReadUserDto>> getAllUser(String top,String  orderby,String filter,String search)
        {
            try{
                GetAllUserRequest request = new GetAllUserRequest{
                    Top = Convert.ToInt32(top), OderBy = orderby,
                    Filter = filter, Search = search
                };
                return Ok(_adminService.GetAllUser(request));
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        [Route("GetAllReport/{top}/{orderby}/{filter}/{search}")]
        public ActionResult<List<GetReportRespond>> getAllReport(String top,String  orderby,String filter,String search)
        {
            try{
                GetAllReportRequest request = new GetAllReportRequest{
                    Top = Convert.ToInt32(top), OderBy = orderby,
                    Filter = filter, Search = search
                };
                return Ok(_adminService.GetAllReport(request));
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Authorize(Role.Admin)]
        [Route("GetUserInfo/{id}")]
        public ActionResult<ReadUserDto> getUserInfo(int id)
        {
            try{
                return Ok(_adminService.GetUserById(id));
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Authorize(Role.Admin)]
        [Route("GetReportInfo/{id}")]
        public ActionResult<GetReportRespond> getReportInfo(int id)
        {
            try{
                return Ok(_adminService.GetReportById(id));
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        [Authorize(Role.Admin)]
        [HttpPut]
        [Route("UpdateReport")]
        public ActionResult UpdateReport(UpdateReportRequest request)
        {
            try{
                if(_adminService.UpdateReport(request)==true)
                    return Ok();
                return BadRequest();
            }catch(Exception e){
                Console.Write(e.Message);
                return BadRequest(e.Message);
            }
        }
        [Authorize(Role.Admin)]
        [HttpPut]
        [Route("UpdateUser")]
        public ActionResult UpdateUser(UpdateUserRequest request)
        {
            try{
                if(_adminService.UpdateUser(request)==true)
                    return Ok();
                return BadRequest();
            }catch(Exception e){
                Console.Write(e.Message);
                return BadRequest(e.Message);
            }
        }
        [Authorize(Role.Admin)]
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public ActionResult DeleteUser(int id)
        {
            try{
                if(_adminService.DeleteUser(id)==true)
                    return Ok();
                return BadRequest();
            }catch(Exception e){
                Console.Write(e.Message);
                return BadRequest(e.Message);
            }
        }
        [Authorize(Role.Admin)]
        [HttpDelete]
        [Route("DeleteNonReferenceChatRoom")]
        public ActionResult DeleteNonReferenceChatRoom()
        {
            try{
                if(_adminService.DeleteNonReferenceChatRoom()==true)
                    return Ok();
                return BadRequest();
            }catch(Exception e){
                Console.Write(e.Message);
                return BadRequest(e.Message);
            }
        }
        [Authorize(Role.Admin)]
        [HttpPost]
        [Route("CreateNewAdmin")]
        public ActionResult CreateNewAdmin(SignUpRequest request)
        {
            try{
                var respond =_adminService.CreateUser(request);
                return Ok(respond);
            }catch(Exception e){
                Console.Write(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}