using System;
using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Models;

namespace TalktifAPI.Repository
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(TalktifContext context) : base(context)
        {
        }

        public List<Report> GetAllReport(int top,string oderby,String filter,String search)
        {
            if(search.Equals("null")) search = "";
            switch(oderby){
                case "Id" : 
                {
                    switch(filter){
                        case "ID" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Id.ToString().Contains(search)).Take(top).ToList();
                        case "Suspecter" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Suspect.ToString().Contains(search)).Take(top).ToList();
                        case "Reason" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Reason.Contains(search)).Take(top).ToList();
                        default : return Entities.OrderByDescending(p => p.Id).Take(top).ToList();
                    }
                }
                case "Status" : 
                {
                    switch(filter){
                        case "ID" : return Entities.OrderByDescending(p => p.Status).Where(p => p.Id.ToString().Contains(search)).Take(top).ToList();
                        case "Suspecter" : return Entities.OrderByDescending(p => p.Status).Where(p => p.Suspect.ToString().Contains(search)).Take(top).ToList();
                        case "Reason" : return Entities.OrderByDescending(p => p.Status).Where(p => p.Reason.Contains(search)).Take(top).ToList();
                        default : return Entities.OrderByDescending(p => p.Status).Take(top).ToList();
                    }
                }
                case "Reporter" : 
                {
                    switch(filter){
                        case "ID" : return Entities.OrderByDescending(p => p.Reporter).Where(p => p.Id.ToString().Contains(search)).Take(top).ToList();
                        case "Suspecter" : return Entities.OrderByDescending(p => p.Reporter).Where(p => p.Suspect.ToString().Contains(search)).Take(top).ToList();
                        case "Reason" : return Entities.OrderByDescending(p => p.Reporter).Where(p => p.Reason.Contains(search)).Take(top).ToList();
                        default : return Entities.OrderByDescending(p => p.Reporter).Take(top).ToList();
                    }
                }
                case "Suspect" : 
                {
                    switch(filter){
                        case "ID" : return Entities.OrderByDescending(p => p.Suspect).Where(p => p.Id.ToString().Contains(search)).Take(top).ToList();
                        case "Suspecter" : return Entities.OrderByDescending(p => p.Suspect).Where(p => p.Suspect.ToString().Contains(search)).Take(top).ToList();
                        case "Reason" : return Entities.OrderByDescending(p => p.Suspect).Where(p => p.Reason.Contains(search)).Take(top).ToList();
                        default : return Entities.OrderByDescending(p => p.Suspect).Take(top).ToList();
                    }
                }
                case "Reason" : 
                {
                    switch(filter){
                        case "ID" : return Entities.OrderByDescending(p => p.Reason).Where(p => p.Id.ToString().Contains(search)).Take(top).ToList();
                        case "Suspecter" : return Entities.OrderByDescending(p => p.Reason).Where(p => p.Suspect.ToString().Contains(search)).Take(top).ToList();
                        case "Reason" : return Entities.OrderByDescending(p => p.Reason).Where(p => p.Reason.Contains(search)).Take(top).ToList();
                        default : return Entities.OrderByDescending(p => p.Reason).Take(top).ToList();
                    }
                }
                case "DayReport" : {
                    switch(filter){
                        case "ID" : return Entities.OrderByDescending(p => p.CreatedAt).Where(p => p.Id.ToString().Contains(search)).Take(top).ToList();
                        case "Suspecter" : return Entities.OrderByDescending(p => p.CreatedAt).Where(p => p.Suspect.ToString().Contains(search)).Take(top).ToList();
                        case "Reason" : return Entities.OrderByDescending(p => p.CreatedAt).Where(p => p.Reason.Contains(search)).Take(top).ToList();
                        default : return Entities.OrderByDescending(p => p.CreatedAt).Take(top).ToList();
                    }
                }
                default : 
                {
                    switch(filter){
                        case "ID" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Id.ToString().Contains(search)).Take(top).ToList();
                        case "Suspecter" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Suspect.ToString().Contains(search)).Take(top).ToList();
                        case "Reason" : return Entities.OrderByDescending(p => p.Id).Where(p => p.Reason.Contains(search)).Take(top).ToList();
                        default : return Entities.OrderByDescending(p => p.Id).Take(top).ToList();
                    }
                }
            }     
        }
    }
}