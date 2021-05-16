using TalktifAPI.Models;

namespace TalktifAPI.Service
{
    public interface IJwtService
    {
        string GenerateSecurityToken(bool IdAdmin);
        string GenerateRefreshToken(bool IdAdmin);
        bool ValidRefreshToken(UserRefreshToken token);
        bool ValidSecurityToken(string token);
        bool GetRole(string token);
    }
}