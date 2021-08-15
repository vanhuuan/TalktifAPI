using System;
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

        public List<User> GetAllUSer(int top,string oderby,String filter,String search)
        {
            if(search.Equals("null")) search = "";
            switch(oderby){
                case "Id" : 
                {
                    switch(filter){
                        case "ID" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Id.ToString().Contains(search)).Take(top).ToList();
                        case "Name" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Name.Contains(search)).Take(top).ToList();
                        case "Email" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Email.Contains(search)).Take(top).ToList();
                        default : {
                            return Entities.OrderByDescending(p => p.Id).Take(top).ToList();
                        }
                    }
                }
                case "Name" : 
                {
                    switch(filter){
                        case "ID" : return Entities.OrderByDescending(p => p.Name).Where(p => p.Id.ToString().Contains(search)).Take(top).ToList();
                        case "Name" : return Entities.OrderByDescending(p => p.Name).Where(p => p.Name.Contains(search)).Take(top).ToList();
                        case "Email" : return Entities.OrderByDescending(p => p.Name).Where(p => p.Email.Contains(search)).Take(top).ToList();
                        default : return Entities.OrderByDescending(p => p.Name).Take(top).ToList();
                    }
                }
                case "Email" : 
                {
                    switch(filter){
                        case "ID" : return Entities.OrderByDescending(p => p.Email).Where(p => p.Id.ToString().Contains(search)).Take(top).ToList();
                        case "Name" : return Entities.OrderByDescending(p => p.Email).Where(p => p.Name.Contains(search)).Take(top).ToList();
                        case "Email" : return Entities.OrderByDescending(p => p.Email).Where(p => p.Email.Contains(search)).Take(top).ToList();
                        default : return Entities.OrderByDescending(p => p.Email).Take(top).ToList();
                    }
                }
                default : 
                {
                    switch(filter){
                        case "ID" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Id.ToString().Contains(search)).Take(top).ToList();
                        case "Name" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Name.Contains(search)).Take(top).ToList();
                        case "Email" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Email.Contains(search)).Take(top).ToList();
                        default : return Entities.OrderByDescending(p => p.Id).Take(top).ToList();
                    }
                }
            }           
        }

        public User GetUserByEmail(string email)
        {
            return Entities.FirstOrDefault(p => p.Email == email);
        }
    }
}