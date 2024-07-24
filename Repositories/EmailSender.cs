using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toAddress, string subject, string body)
        {
            using (var client = new SmtpClient())
            {
                _logger.LogInformation("Email configuration:");
                _logger.LogInformation($"Host: {_configuration["Smtp:Host"]}");
                _logger.LogInformation($"Port: {_configuration["Smtp:Port"]}");
                _logger.LogInformation($"UseSSL: {_configuration["Smtp:UseSSL"]}");
                _logger.LogInformation($"Username: {_configuration["Smtp:Username"]}");

                client.Host = _configuration["Smtp:Host"];
                client.Port = int.Parse(_configuration["Smtp:Port"]);
                client.EnableSsl = bool.Parse(_configuration["Smtp:UseSSL"]);
                client.Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);

                var fromAddress = _configuration["Smtp:Username"];
                var message = new MailMessage
                {
                    From = new MailAddress(fromAddress),
                    Subject = subject,
                    Body = body
                };
                message.To.Add(toAddress);

                try
                {
                    await client.SendMailAsync(message);
                    _logger.LogInformation("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error sending email:", ex);
                }
            }
        }
    }
}
