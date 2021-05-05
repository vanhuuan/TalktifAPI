using System.Collections.Generic;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.Admin;
using TalktifAPI.Models;

namespace TalktifAPI.Data
{
    public interface IAdminRepo
    {
        List<ReadUserDto> GetAllUser(GetAllUserRequest request);
        List<GetReportRespond> GetAllReport(GetAllReportRequest request);
        bool UpdateReport(UpdateReportRequest request);
        bool IsAdmin(int id);
    }
}