using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace BroadcastApi.Helpers
{
    public class MailHelper : IMailHelper
    {
        readonly IConfiguration configuration;

        public MailHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendMail(string to, string subject, string body)
        {
            var from = this.configuration["Mail:From"];
            var smtp = this.configuration["Mail:Smtp"];
            var port = this.configuration["Mail:Port"];
            var password = this.configuration["Mail:Password"];

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(from));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            client.Connect(smtp, int.Parse(port), false);
            client.Authenticate(from, password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
