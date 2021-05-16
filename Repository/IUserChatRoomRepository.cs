using System.Collections.Generic;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public interface IUserChatRoomRepository : IGenericRepository<UserChatRoom>
    {
        List<UserChatRoom> GetAllUserChatRoom(int uid);
        UserChatRoom GetUserChatRoomByFK(int uid,int crid);
        UserChatRoom GetAnotherUserChatRoom(int uid,int crid);
        UserChatRoom[] GetAllChatRoomMember(int chatroomId);
    }
}