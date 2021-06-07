using System;
using TalktifAPI.Models;

namespace TalktifAPI.Service
{
    public interface IJwtService
    {
        string GenerateSecurityToken(int id);
        string GenerateRefreshToken(int user);
        bool ValidRefreshToken(String token);
        bool ValidSecurityToken(string token);
        int GetId(string token);
    }
}