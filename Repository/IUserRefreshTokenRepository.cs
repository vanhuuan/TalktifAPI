using System.Collections.Generic;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public interface IUserRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
        UserRefreshToken GetTokenByToken(string token);
        int GetLastIdToken();
        UserRefreshToken GetTokenByUserAndDevice(int userid,string device);
        List<UserRefreshToken> GetTokenByUID(int uid);  
    }
}