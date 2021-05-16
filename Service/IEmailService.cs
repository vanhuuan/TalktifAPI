using System.Threading.Tasks;
using TalktifAPI.Dtos;

namespace TalktifAPI.Service
{
    public interface IEmailService
    {
        Task SendMail(MailContent mailContent);   
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}