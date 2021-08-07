using System;

namespace TalktifAPI.Dtos
{
    public class GetAllReportRequest
    {
        public int  Top { get; set; }
        public string OderBy { get; set; }   
        public String Filter { get; set; }
        public String Search { get; set; }          
    }
}