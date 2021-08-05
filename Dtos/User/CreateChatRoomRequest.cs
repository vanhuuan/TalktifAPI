using System;

namespace TalktifAPI.Dtos
{
    public class CreateChatRoomRequest
    {
        
        public int User1Id { get; set; }
        public String User1NickName { get; set; }
        public String User2NickName { get; set; }
        public int User2Id { get; set; }
    }
}