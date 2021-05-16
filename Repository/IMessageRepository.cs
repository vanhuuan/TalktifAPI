using System.Collections.Generic;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        List<Message> GetMessageFromChatRoom(int chatRoomId,int top);
        void RemoveMessageByChatRoomId(int chatRoomId);
    }
}