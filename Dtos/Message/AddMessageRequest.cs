namespace TalktifAPI.Dtos
{
    public class AddMessageRequest
    {
        public string Message {get; set;}
        public int IdSender {get; set; }
        public int IdChatRoom {get; set; }
    }
}