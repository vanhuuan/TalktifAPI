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

        public List<int> GetSharedChatRoon(int uid1, int uid2)
        {
            var list = Entities.Where(p => p.User == uid1 || p.User == uid2).ToList();
            List<int> l = new List<int>();
            foreach(var i in list){
                l.Add(i.ChatRoomId);
            }
            return  l.GroupBy(p => p).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
        }

        public UserChatRoom GetUserChatRoomByFK(int uid, int crid)
        {
            return Entities.FirstOrDefault(p => p.ChatRoomId == crid && p.User == uid);
        }
    }
}