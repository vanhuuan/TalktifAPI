using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public class ChatRoomRepository : GenericRepository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(TalktifContext context) : base(context)
        {
        }

        public List<ChatRoom> GetAllChatRoom()
        {
            return Entities.Where(p => p.NumOfMember <= 0).ToList();
        }

        public ChatRoom GetChatRoomByName(string name)
        {
            return Entities.OrderByDescending(x => x.Id).FirstOrDefault(p => p.ChatRoomName.Equals(name));
        }
    }
}