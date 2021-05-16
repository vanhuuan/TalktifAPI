using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TalktifContext context) : base(context)
        {
        }

        public List<User> GetAllUSer(int top,string oderby)
        {
            switch(oderby){
                case "Id" : return Entities.OrderByDescending(p => p.Id).Take(top).ToList(); 
                case "Name" : return Entities.OrderByDescending(p => p.Name).Take(top).ToList();
                case "Email" : return Entities.OrderByDescending(p => p.Email).Take(top).ToList();
                default : return Entities.OrderByDescending(p => p.Id).Take(top).ToList();
            }           
        }

        public User GetUserByEmail(string email)
        {
            return Entities.FirstOrDefault(p => p.Email == email);
        }
    }
}