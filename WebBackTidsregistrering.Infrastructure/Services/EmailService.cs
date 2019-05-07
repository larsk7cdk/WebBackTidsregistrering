using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using WebBackTidsregistrering.Application.Interfaces;

namespace WebBackTidsregistrering.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmail(string mailTo, string subject, string message)
        {
            return await Task.Run(() =>
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("asmtp.unoeuro.com", 8080, SecureSocketOptions.None);

                    client.Authenticate("xxx", "xxx");

                    var options = FormatOptions.Default.Clone();

                    if (client.Capabilities.HasFlag(SmtpCapabilities.UTF8))
                        options.International = true;

                    var mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress("Tidsregistrering",
                        "tidsregistrering@tidsregistrering.dk"));
                    mimeMessage.To.Add(new MailboxAddress("", mailTo));
                    mimeMessage.Subject = subject;
                    mimeMessage.Body = new TextPart(TextFormat.Html)
                    {
                        Text = message
                    };

                    client.Send(options, mimeMessage);

                    client.Disconnect(true);

                    return true;
                }
            });
        }
    }
}