using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IJwtService _jwtService;

        public ChatController(IChatService service,IJwtService jwtService)
        {
            _service = service;
            _jwtService = jwtService;
        }
        [HttpPost]   
        [Authorize]     
        [Route("CreateChatRoom")]
        public ActionResult Create(CreateChatRoomRequest createChatRoom)
        {
            try{
                try{
                    CheckId(createChatRoom.User1Id);
                }catch(Exception e){
                    if(e.Message=="You don't have permission to do this action"){
                        CheckId(createChatRoom.User2Id);
                    }
                }
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
                CheckId(userid);
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
        [Route("FetchMessage/{userid}/{roomid}/{top}")]
        public ActionResult<List<MessageRespond>> FecthMessage(int userid,int roomid,int top)
        {
            try{
                CheckId(userid);
                FetchMessageRequest request = new FetchMessageRequest{
                    RoomId = roomid, Top = top
                };
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
        [Route("GetChatRoomInfo/{id}/{userid}")]
        public ActionResult<GetChatRoomInfoRespond> GetChatRoomInfo(int id,int userid)
        {
            try{
                CheckId(userid);
                GetChatRoomInfoRequest room = new GetChatRoomInfoRequest {
                    Id = id, UserId = userid
                };
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
                CheckId(mess.IdSender);
                bool check =_service.AddMessage(mess);
                if( check!=false ){
                    return Ok();
                }
                else return BadRequest();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"\n add message err");
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]   
        [Authorize]     
        [Route("Delete/{userid}/{roomid}")]
        public ActionResult<GetChatRoomInfoRespond> DeleteFriend(int userid,int roomid)
        {
            try{
                CheckId(userid);
                DeleteFriendRequest mess = new DeleteFriendRequest{
                    UserId = userid, RoomId = roomid
                };
                _service.DeleteChatRoom(mess);
                return Ok();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"\n delete err");
                return BadRequest(e.Message);
            }
        }
        public int GetId(){
            String token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            return _jwtService.GetId(token);
        }
        public bool CheckId(int id){
            if(GetId()!=id) throw new Exception("You don't have permission to do this action");
            else return true;
        }
    }
}