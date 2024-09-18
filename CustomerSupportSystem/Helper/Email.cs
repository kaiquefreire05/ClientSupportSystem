using System.Net;
using System.Net.Mail;
using CustomerSupportSystem.Helper.Interfaces;

namespace CustomerSupportSystem.Helper
{
    public class Email : IEmail
    {
        // Dependencies Injection
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Sent(string email, string subject, string message)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                var host = _configuration.GetValue<string>("SMTP:Host");
                var name = _configuration.GetValue<string>("SMTP:Name");
                var username = _configuration.GetValue<string>("SMTP:Username");
                var pass = _configuration.GetValue<string>("SMTP:Password");
                var door = _configuration.GetValue<int>("SMTP:Door");

                var mail = new MailMessage
                {
                    From = new MailAddress(username, name)
                };

                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (var smtp = new SmtpClient())
                {
                    smtp.Credentials = new NetworkCredential(username, pass);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}