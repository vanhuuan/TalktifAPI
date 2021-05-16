using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(TalktifContext context) : base(context)
        {
        }

        public List<Message> GetMessageFromChatRoom(int chatRoomId, int top)
        {
            return Entities.Where(p => p.ChatRoomId == chatRoomId).OrderByDescending(p => p.SentAt).Take(top).ToList();
        }

        public void RemoveMessageByChatRoomId(int chatRoomId)
        {
            Entities.RemoveRange(Entities.Where(p => p.ChatRoomId == chatRoomId));
        }
    }
}