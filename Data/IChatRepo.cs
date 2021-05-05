using System.Collections.Generic;
using TalktifAPI.Dtos;
using TalktifAPI.Models;

namespace TalktifAPI.Data
{
    public interface IChatRepo
    {
        bool SaveChange();
        List<MessageRespond> FecthAllMessageInChatRoom(FetchMessageRequest request);
        List<FetchAllChatRoomRespond> FetchAllChatRoom(int id);
        GetChatRoomInfoRespond GetChatRoomInfo(int roomid);
        bool CreateChatRoom(CreateChatRoomRequest r);
        bool DeleteChatRoom(DeleteFriendRequest room);
        bool ChangeNickName(ChangeNickNameRequest r);
        bool AddMessage(AddMessageRequest mess);
    }
}