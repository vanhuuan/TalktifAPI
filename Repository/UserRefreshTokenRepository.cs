using System;
using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public class UserRefreshTokenRepository : GenericRepository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(TalktifContext context) : base(context)
        {
        }

        public UserRefreshToken GetTokenByToken(int userid)
        {
            return Entities.Where(p => p.User == userid).OrderByDescending(p => p.CreateAt).FirstOrDefault();
        }

        public List<UserRefreshToken> GetTokenByUID(int uid)
        {
            return Entities.Where(p => p.User == uid).ToList();
        }

        public UserRefreshToken GetTokenByUserAndDevice(int userid,string device)
        {
            return Entities.FirstOrDefault(p => p.User == userid && p.Device.Equals(device));
        }
    }
}