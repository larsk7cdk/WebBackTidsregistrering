using System.Threading.Tasks;

namespace WebBackTidsregistrering.Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string mailTo, string subject, string message);
    }
}