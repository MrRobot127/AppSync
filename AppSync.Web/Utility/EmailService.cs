using System.Net.Mail;
using System.Net;
using AppSync.Web.Interfaces;

namespace AppSync.Web.Utility
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                var smtpServer = smtpSettings["SmtpServer"];
                var smtpPort = int.Parse(smtpSettings["SmtpPort"]);
                var smtpUsername = smtpSettings["SmtpUsername"];
                var smtpPassword = smtpSettings["SmtpPassword"];
                var enableSsl = bool.Parse(smtpSettings["EnableSsl"]);

                using (var client = new SmtpClient(smtpServer))
                {
                    client.Port = smtpPort;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = enableSsl;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpUsername),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(toEmail);

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
