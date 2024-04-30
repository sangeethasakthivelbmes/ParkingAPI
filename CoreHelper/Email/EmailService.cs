using System.Net.Mail;
using System.Net;

namespace Parking.WebAPI.CoreHelper.Email
{
    public class EmailService 
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmail(string toAddress, string subject, string body)        
        {
            try
            {
                using (var smtpClient = new SmtpClient(_config["MailSettings:Host"], Convert.ToInt16(_config["MailSettings:Port"])))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_config["MailSettings:UserName"], _config["MailSettings:Password"]);
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_config["MailSettings:UserName"]);
                    mailMessage.To.Add(toAddress);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;

                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
