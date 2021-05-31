using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.Message;
using TalktifAPI.Dtos.User;
using TalktifAPI.Models;
using TalktifAPI.Service;

namespace TalktifAPI.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _service;

        public ChatController(IChatService service)
        {
            _service = service;
        }
        [HttpPost]   
        [Authorize]     
        [Route("CreateChatRoom")]
        public ActionResult Create(CreateChatRoomRequest createChatRoom)
        {
            try{
                CreateChatRoomRespond respond =_service.CreateChatRoom(createChatRoom);
                return Ok(respond);
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"createchatroom err");
                return BadRequest(e.Message);
            }
        }
        [HttpGet]   
        [Authorize]     
        [Route("FetchAllChatRoom/{userid}")]
        public ActionResult<List<FetchAllChatRoomRespond>> FetchAllChatRoom(int userid)
        {
            try{
                List<FetchAllChatRoomRespond> list =_service.FetchAllChatRoom(userid);
                if(list!=null) return Ok(list);
                else return BadRequest();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"createchatroom err");
                return BadRequest(e.Message);
            }
        }

        [HttpGet]   
        [Authorize]     
        [Route("FetchMessage")]
        public ActionResult<List<MessageRespond>> FecthMessage(FetchMessageRequest request)
        {
            try{
                List<MessageRespond> list =_service.FecthAllMessageInChatRoom(request);
                if(list!=null) return Ok(list);
                else return BadRequest();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"fecth err");
                return BadRequest(e.Message);
            }
        } 
        [HttpGet]   
        [Authorize]     
        [Route("GetChatRoomInfo")]
        public ActionResult<GetChatRoomInfoRespond> GetChatRoomInfo(GetChatRoomInfoRequest room)
        {
            try{
                GetChatRoomInfoRespond g =_service.GetChatRoomInfo(room);
                return Ok(g);
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"\n get info err");
                return BadRequest(e.Message);
            }
        }
        [HttpPost]   
        [Authorize]     
        [Route("AddMessage")]
        public ActionResult AddMessage(AddMessageRequest mess)
        {
            try{
                bool check =_service.AddMessage(mess);
                if( check!=false ){
                    return Ok();
                }
                else return BadRequest();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"\n get info err");
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]   
        [Authorize]     
        [Route("Delete")]
        public ActionResult<GetChatRoomInfoRespond> DeleteFriend(DeleteFriendRequest mess)
        {
            try{
                bool check =_service.DeleteChatRoom(mess);
                if( check!=false ){
                    return Ok();
                }
                else return BadRequest();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"\n delete err");
                return BadRequest(e.Message);
            }
        }
    }
}