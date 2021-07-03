using System.Collections.Generic;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.Admin;

namespace TalktifAPI.Service
{
    public interface IAdminService
    {
        List<ReadUserDto> GetAllUser(GetAllUserRequest request);
        List<GetReportRespond> GetAllReport(GetAllReportRequest request);
        ReadUserDto GetUserById(int uid);
        GetReportRespond GetReportById(int uid);
        bool UpdateReport(UpdateReportRequest request);
        bool UpdateUser(UpdateUserRequest request);
        bool IsAdmin(int id);
        bool DeleteUser(int id);
        bool DeleteNonReferenceChatRoom();
        ReadUserDto CreateUser(SignUpRequest user);
        Counts GetNumOfReCord();
        
    }
}