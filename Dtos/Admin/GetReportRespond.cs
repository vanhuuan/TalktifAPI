using System;

namespace TalktifAPI.Dtos
{
    public class GetReportRespond
    {
        public int Id { get; set; }
        public int Reporter { get; set; }
        public int Suspect { get; set; }
        public string Reason { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public DateTime? createAt { get; set; }
    }
}