using System.Collections.Generic;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public interface IReportRepository : IGenericRepository<Report>
    {
         List<Report> GetAllReport(int to,string oderby);
    }
}