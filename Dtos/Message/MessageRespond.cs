using System;

namespace TalktifAPI.Dtos
{
    public class MessageRespond
    {
        public int Id { get; set; }
        public int Sender { get; set; }
        public string Content { get; set; }
        public DateTime? SentAt { get; set; }
    }
}