using System.Collections.Generic;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.User;
using TalktifAPI.Models;

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
        RefreshTokenRespond RefreshToken(ReFreshToken token,int id);
        bool inActiveUser(int id);
        LoginRespond resetPass(string email,string newpass);
        bool ActiveEmail(string token,int id);
        bool CheckToken(string token,int id);

        List<Country> GetAllCountry();
        List<City> GettCityByCountry(int countryid);
    }
}