using TalktifAPI.Models;

namespace TalktifAPI.Service
{
    public interface IJwtService
    {
        string GenerateSecurityToken(int id);
        string GenerateRefreshToken(bool IdAdmin);
        bool ValidRefreshToken(UserRefreshToken token,int id);
        bool ValidSecurityToken(string token);
        int GetId(string token);
    }
}