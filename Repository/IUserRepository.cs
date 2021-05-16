using System.Collections.Generic;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUserByEmail(string email);
        List<User> GetAllUSer(int top,string oderby);
    }
}