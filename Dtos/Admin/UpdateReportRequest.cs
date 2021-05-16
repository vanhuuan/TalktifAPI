namespace TalktifAPI.Dtos.Admin
{
    public class UpdateReportRequest
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
    }
}