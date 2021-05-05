namespace TalktifAPI.Dtos
{
    public class ChangeNickNameRequest
    {
        public int iduser { get; set; }
        public string nickname { get; set; }
        public int idchatroom { get; set; }
    }
}