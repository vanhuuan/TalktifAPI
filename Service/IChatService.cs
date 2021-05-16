using System.Collections.Generic;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.Message;
using TalktifAPI.Dtos.User;

namespace TalktifAPI.Service
{
    public interface IChatService
    {
        List<MessageRespond> FecthAllMessageInChatRoom(FetchMessageRequest request);
        List<FetchAllChatRoomRespond> FetchAllChatRoom(int id);
        GetChatRoomInfoRespond GetChatRoomInfo(GetChatRoomInfoRequest room);
        CreateChatRoomRespond CreateChatRoom(CreateChatRoomRequest r);
        bool DeleteChatRoom(DeleteFriendRequest room);
        bool ChangeNickName(ChangeNickNameRequest r);
        bool AddMessage(AddMessageRequest mess);
    }
}