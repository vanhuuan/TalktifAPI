using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TalktifAPI.Data;
using TalktifAPI.Dtos;
using TalktifAPI.Models;

namespace TalktifAPI.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepo _repository;

        public ChatController(IChatRepo repository)
        {
            _repository = repository;
        }
        [HttpGet]   
        [Authorize]     
        [Route("CreateChatRoom")]
        public ActionResult Create(CreateChatRoomRequest createChatRoom)
        {
            try{
                bool check =_repository.CreateChatRoom(createChatRoom);
                if(check==true){ 
                    _repository.SaveChange();
                    return Ok();
                }
                else return NoContent();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"createchatroom err");
                return NoContent();
            }
        }
        [HttpPost]   
        [Authorize]     
        [Route("FetchAllChatRoom/{userid}")]
        public ActionResult<List<FetchAllChatRoomRespond>> FetchAllChatRoom(int userid)
        {
            try{
                List<FetchAllChatRoomRespond> list =_repository.FetchAllChatRoom(userid);
                if(list!=null) return Ok(list);
                else return BadRequest();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"createchatroom err");
                return BadRequest();
            }
        }

        [HttpPost]   
        [Authorize]     
        [Route("FetchMessage")]
        public ActionResult<List<MessageRespond>> FecthMessage(FetchMessageRequest request)
        {
            try{
                List<MessageRespond> list =_repository.FecthAllMessageInChatRoom(request);
                if(list!=null) return Ok(list);
                else return BadRequest();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"fecth err");
                return BadRequest();
            }
        } 
        [HttpPost]   
        [Authorize]     
        [Route("GetChatRoomInfo/{roomid}")]
        public ActionResult<GetChatRoomInfoRespond> GetChatRoomInfo(int roomid)
        {
            try{
                GetChatRoomInfoRespond g =_repository.GetChatRoomInfo(roomid);
                if(g!=null) return Ok(g);
                else return BadRequest();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"\n get info err");
                return BadRequest();
            }
        }
        [HttpPost]   
        [Authorize]     
        [Route("AddMessage")]
        public ActionResult<GetChatRoomInfoRespond> AddMessage(AddMessageRequest mess)
        {
            try{
                bool check =_repository.AddMessage(mess);
                if( check!=false ){
                    _repository.SaveChange();
                    return Ok();
                }
                else return BadRequest();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"\n get info err");
                return BadRequest();
            }
        }
        [HttpDelete]   
        [Authorize]     
        [Route("Delete")]
        public ActionResult<GetChatRoomInfoRespond> DeleteFriend(DeleteFriendRequest mess)
        {
            try{
                bool check =_repository.DeleteChatRoom(mess);
                if( check!=false ){
                    _repository.SaveChange();
                    return Ok();
                }
                else return BadRequest();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"\n delete err");
                return BadRequest();
            }
        }
    }
}