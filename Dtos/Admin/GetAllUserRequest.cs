using System;

namespace TalktifAPI.Dtos
{
    public class GetAllUserRequest
    {
        public int  Top { get; set; }
        public String OderBy { get; set; }
        public String Filter { get; set; }
        public String Search { get; set; }     
        
    }
}