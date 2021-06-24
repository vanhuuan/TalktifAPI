using System;
using System.Collections.Generic;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.Message;
using TalktifAPI.Dtos.User;
using TalktifAPI.Models;
using TalktifAPI.Repository;

namespace TalktifAPI.Service
{
    public class ChatService : IChatService
    {
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IMessageRepository _messRepository;
        private readonly IUserChatRoomRepository _userChatRoomRepository;

        public ChatService(IChatRoomRepository chatRoomRepository,IMessageRepository messRepository,IUserChatRoomRepository userChatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
            _messRepository = messRepository;
            _userChatRoomRepository = userChatRoomRepository;
        }
        public bool AddMessage(AddMessageRequest mess)
        {
            try{
            _messRepository.Insert(new Message{
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

        public bool ChangeNickName(ChangeNickNameRequest r)
        {
            UserChatRoom u = _userChatRoomRepository.GetById(r.idchatroom);
            if(u==null) throw new Exception("Chat room doesn't exist");
            u.NickName = r.nickname;
            return true;
        }

        public void  CheckDuplicateChatRoom(int uid1,int uid2){
            try{
                var list = _userChatRoomRepository.GetSharedChatRoon(uid1,uid2);
                foreach(var i in list){
                    var chatroon = _chatRoomRepository.GetById(i);
                    if(chatroon.NumOfMember==2){
                            throw new Exception("You guys has already been a friend");
                        }
                    else{
                        continue;
                        }
                }
            }catch(NullReferenceException){

            }
        }
        public CreateChatRoomRespond CreateChatRoom(CreateChatRoomRequest r)
        {
            CheckDuplicateChatRoom(r.User1Id,r.User2Id);
            _chatRoomRepository.Insert(new ChatRoom{
                    NumOfMember = 2,
                    ChatRoomName = r.User1Id + "and" + r.User2Id,
                    CreatedAt = DateTime.Now
                });
            ChatRoom i = _chatRoomRepository.GetChatRoomByName(r.User1Id + "and" + r.User2Id);
            _userChatRoomRepository.Insert(new UserChatRoom{
                    ChatRoomId = i.Id,
                    NickName = r.User1NickName,
                    User = r.User1Id
                });
            _userChatRoomRepository.Insert(new UserChatRoom{
                    ChatRoomId = i.Id,
                    NickName = r.User2NickName,
                    User = r.User2Id
                });
            i.ChatRoomName=r.User1NickName+ " and "+r.User2NickName;
            _chatRoomRepository.Update(i);
            return new CreateChatRoomRespond{
                RoomId = i.Id,
                RoomName = i.ChatRoomName,
            };
        }

        public void DeleteChatRoom(DeleteFriendRequest room)
        {
            ChatRoom c =_chatRoomRepository.GetById(room.RoomId);
            UserChatRoom cr1 = _userChatRoomRepository.GetUserChatRoomByFK(room.UserId,room.RoomId);
            _userChatRoomRepository.Delete(cr1);
            c.NumOfMember--;
            if(c.NumOfMember == 0) {
                _chatRoomRepository.Delete(c);
            }else {
            _chatRoomRepository.Update(c);
            }      
        }

        public List<MessageRespond> FecthAllMessageInChatRoom(FetchMessageRequest request)
        {
            List<Message> list = _messRepository.GetMessageFromChatRoom(request.RoomId,request.Top);
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

        public List<FetchAllChatRoomRespond> FetchAllChatRoom(int id)
        {
            List<FetchAllChatRoomRespond> list = new List<FetchAllChatRoomRespond>();
            List<UserChatRoom> l = _userChatRoomRepository.GetAllUserChatRoom(id);
            foreach(UserChatRoom i in l){
                ChatRoom c = _chatRoomRepository.GetById(i.ChatRoomId);
                FetchAllChatRoomRespond r = new FetchAllChatRoomRespond{
                    id = c.Id,
                    Name = c.ChatRoomName
                };
                list.Add(r);
            }
            return list;
        }

        public GetChatRoomInfoRespond GetChatRoomInfo(GetChatRoomInfoRequest room)
        {
            UserChatRoom[] list = _userChatRoomRepository.GetAllChatRoomMember(room.Id);
            if(list.Length>1){
                GetChatRoomInfoRespond r = new GetChatRoomInfoRespond{
                    NickName1 = list[0].NickName,
                    User1Id = list[0].User,
                    NickName2 = list[1].NickName,
                    User2Id = list[1].User
                };
                return r;
            }else{
                GetChatRoomInfoRespond r = new GetChatRoomInfoRespond{
                    NickName1 = list[0].NickName,
                    User1Id = list[0].User,
                    NickName2 = "Unknown",
                    User2Id = 0
                };
                return r;
            }
        }
    }
}