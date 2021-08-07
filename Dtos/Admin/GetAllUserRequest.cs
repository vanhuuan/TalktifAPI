using System;

namespace TalktifAPI.Dtos
{
    public class GetAllUserRequest
    {
        public int From { get; set; }
        public int  To { get; set; }
        public String OderBy { get; set; }
        public String Filter { get; set; }
        public String Search { get; set; }     
        
    }
}