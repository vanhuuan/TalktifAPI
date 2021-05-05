using System;
using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Dtos;
using TalktifAPI.Models;

namespace TalktifAPI.Data
{
    public class ChatRepo : IChatRepo
    {
        private readonly TalktifContext _context;

        public ChatRepo(TalktifContext context)
        {
            _context = context;
        }
        public bool SaveChange()
        {
            return (_context.SaveChanges() >= 0);
        }
        public bool ChangeNickName(ChangeNickNameRequest r)
        {
            UserChatRoom u = _context.UserChatRooms.FirstOrDefault(p => p.ChatRoomId == r.idchatroom);
            if(u==null) throw new Exception();
            u.NickName = r.nickname;
            return true;

        }
        public bool CreateChatRoom(CreateChatRoomRequest r)
        {
            try{
                _context.ChatRooms.Add(new ChatRoom{
                    NumOfMember = 2,
                    ChatRoomName = r.User1Id + "and" + r.User2Id,
                });
                _context.SaveChanges();
                ChatRoom i = _context.ChatRooms.FirstOrDefault(p => p.ChatRoomName ==( r.User1Id + "and" + r.User2Id));
                _context.UserChatRooms.Add(new UserChatRoom{
                    ChatRoomId = i.Id,
                    NickName = r.User1NickName,
                    User = r.User1Id
                });
                _context.UserChatRooms.Add(new UserChatRoom{
                    ChatRoomId = i.Id,
                    NickName = r.User2NickName,
                    User = r.User2Id
                });
                i.ChatRoomName=r.User1NickName+ " and "+r.User2NickName;
                return true;
            }catch(Exception){
                return false;
            }
        }

        public bool DeleteChatRoom(DeleteFriendRequest room)
        {
            try{
                ChatRoom r  = _context.ChatRooms.FirstOrDefault(r => r.Id == room.RoomId); 
                int i = r.NumOfMember;
                if(_context.UserChatRooms.FirstOrDefault(r => r.ChatRoomId == room.RoomId && r.User==room.UserId)==null) return false;
                do{
                    _context.UserChatRooms.Remove(_context.UserChatRooms.FirstOrDefault(r => r.ChatRoomId == room.RoomId));
                    _context.SaveChanges();
                    i--;
                }while(i>0);
                return true;
            }catch(Exception){
                return false;
            }

        }
        public List<MessageRespond> FecthAllMessageInChatRoom(FetchMessageRequest request)
        {
            List<Message> list = _context.Messages.Where(p => p.ChatRoomId == request.RoomId).OrderByDescending(p => p.SentAt).Take(request.Top).ToList();
            List<MessageRespond> l = new List<MessageRespond>();
            foreach(Message i in list){
                MessageRespond r = new MessageRespond{
                    Id = i.Id,
                    Content = i.Content,
                    Sender = i.Sender,
                    SentAt = i.SentAt
                };
                l.Add(r);
            }
            if(l != null) return l;
            else throw new Exception("Khong co tin nhan nao");
        }

        public bool AddMessage(AddMessageRequest mess)
        {
            try{
            _context.Messages.Add(new Message{
                Sender = mess.IdSender,
                ChatRoomId  = mess.IdChatRoom,
                Content = mess.Message,
                SentAt = DateTime.Now
            });
            return true;
            }catch(Exception err){
                Console.WriteLine(err.Message);
                return false;
            }
        }

        public List<FetchAllChatRoomRespond> FetchAllChatRoom(int id)
        {

            // var l = _context.ChatRooms.Join(_context.UserChatRooms,p=>p.Id,q=>q.ChatRoomId,(p,q)=>new {
            //     IdRoom =p.Id,
            //     NameRoom = p.ChatRoomName,
            //     UserId = q.User
            // }).Select(p => p.UserId==id);
                       
            // foreach(var i in l){
            //     FetchAllChatRoomRespond r = new FetchAllChatRoomRespond{
            //         id = i.IdRoom,
            //         Name = i.NameRoom
            //     };
            // }

            List<FetchAllChatRoomRespond> list = new List<FetchAllChatRoomRespond>();
            List<UserChatRoom> l = _context.UserChatRooms.Where(p => p.User == id).ToList();
            foreach(UserChatRoom i in l){
                ChatRoom c = _context.ChatRooms.FirstOrDefault(p => p.Id == i.ChatRoomId);
                FetchAllChatRoomRespond r = new FetchAllChatRoomRespond{
                    id = c.Id,
                    Name = c.ChatRoomName
                };
                list.Add(r);
            }

            return list;
        }

        public GetChatRoomInfoRespond GetChatRoomInfo(int roomid)
        {
            UserChatRoom[] list = _context.UserChatRooms.Where(p => p.ChatRoomId == roomid).ToArray();
            Console.Write(list.Length);
            GetChatRoomInfoRespond r = new GetChatRoomInfoRespond{
                NickName1 = list[0].NickName,
                User1Id = list[0].User,
                NickName2 = list[1].NickName,
                User2Id = list[1].User
            };
            return r;
        }
        
    }
}