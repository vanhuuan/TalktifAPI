using System;

namespace TalktifAPI.Dtos
{
    public class GetAllReportRequest
    {
        public int From { get; set; }
        public int  To { get; set; }
        public string OderBy { get; set; }   
        public String Filter { get; set; }
        public String Search { get; set; }          
    }
}