using TalktifAPI.Dtos;
using TalktifAPI.Dtos.User;

namespace TalktifAPI.Service
{
    public interface IUserService
    {
        bool isUserExists(int id);
        ReadUserDto getInfoById(int id);   
        ReadUserDto getInfoByEmail(string email);   
        SignUpRespond signUp(SignUpRequest user);
        LoginRespond signIn(LoginRequest user);
        ReadUserDto updateInfo(UpdateInfoRequest user);
        bool Report(ReportRequest request);
        RefreshTokenRespond RefreshToken(ReFreshToken token);
        bool inActiveUser(int id);
        LoginRespond resetPass(string email,string newpass);
        bool ActiveEmail(string token,int id);
        bool CheckToken(string token,int id);
    }
}