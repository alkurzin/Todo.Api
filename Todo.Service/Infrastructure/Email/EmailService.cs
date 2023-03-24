using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace TaskManager.Service.Infrastructure.Email
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("TimeTask", "info@timetask.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("mail.hosting.reg.ru", 465, true);
                await client.AuthenticateAsync("info@timetask.ru", "timetask2022");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
