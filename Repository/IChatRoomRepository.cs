using System.Collections.Generic;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public interface IChatRoomRepository : IGenericRepository<ChatRoom>
    {
        ChatRoom GetChatRoomByName(string name);
        List<ChatRoom> GetAllChatRoom();
    }
}