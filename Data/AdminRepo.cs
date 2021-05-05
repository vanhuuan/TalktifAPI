using System;
using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.Admin;
using TalktifAPI.Models;

namespace TalktifAPI.Data
{
    public class AdminRepo : IAdminRepo
    {
        private readonly TalktifContext _context;

        public AdminRepo(TalktifContext context)
        {
            _context = context;
        }

        public List<GetReportRespond> GetAllReport(GetAllReportRequest request)
        {
            throw new NotImplementedException();
        }

        public List<ReadUserDto> GetAllUser()
        {
            List<ReadUserDto> list = new List<ReadUserDto>();
            try{
            var read = _context.Users.Where(p => p.Id != 0);
            foreach(var u in read){
                list.Add(new ReadUserDto{
                    Email =  u.Email, Name =  u.Name, Id =  u.Id 
                });
            }
            }catch(Exception err){
                Console.Write(err.ToString());
            }
            return list;
        }

        public List<ReadUserDto> GetAllUser(GetAllUserRequest request)
        {
            throw new NotImplementedException();
        }

        public bool IsAdmin(int id)
        {
            if(_context.Users.FirstOrDefault(p => p.Id == id).IsAdmin==true)
                return true;
            return false;
        }

        public bool UpdateReport(UpdateReportRequest request)
        {
            throw new NotImplementedException();
        }
    }
}