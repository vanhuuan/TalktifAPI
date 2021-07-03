using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(TalktifContext context) : base(context)
        {
        }

        public List<Report> GetAllReport(int top,string oderby)
        {
            switch(oderby){
                case "Id" : return Entities.OrderByDescending(p => p.Id).Take(top).ToList(); 
                case "Status" : return Entities.OrderByDescending(p => p.Status).Take(top).ToList();
                case "Reporter" : return Entities.OrderByDescending(p => p.Reporter).Take(top).ToList();
                case "Suspect" : return Entities.OrderByDescending(p => p.Suspect).Take(top).ToList();
                case "Reason" : return Entities.OrderByDescending(p => p.Reason).Take(top).ToList();
                case "DayReport" : return Entities.OrderByDescending(p => p.CreatedAt).Take(top).ToList();
                default : return Entities.OrderByDescending(p => p.Id).Take(top).ToList();
            }     
        }
    }
}