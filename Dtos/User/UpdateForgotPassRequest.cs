namespace TalktifAPI.Dtos.User
{
    public class UpdateForgotPassRequest
    {
        public int Id { get; set; }
        public string NewForgotPass { get; set; }  
        public string oldForgotPass { get; set; }        
    }
}