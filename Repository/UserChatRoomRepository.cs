using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public class UserChatRoomRepository : GenericRepository<UserChatRoom>, IUserChatRoomRepository
    {
        public UserChatRoomRepository(TalktifContext context) : base(context)
        {
        }

        public UserChatRoom[] GetAllChatRoomMember(int chatroomId)
        {
            return Entities.Where(p => p.ChatRoomId == chatroomId).ToArray();
        }

        public List<UserChatRoom> GetAllUserChatRoom(int uid)
        {
            return Entities.Where(p => p.User == uid).ToList();
        }

        public UserChatRoom GetAnotherUserChatRoom(int uid, int crid)
        {
            return Entities.FirstOrDefault(p => p.ChatRoomId == crid && p.User != uid);
        }

        public UserChatRoom GetUserChatRoomByFK(int uid, int crid)
        {
            return Entities.FirstOrDefault(p => p.ChatRoomId == crid && p.User == uid);
        }
    }
}