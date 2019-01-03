using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace pwdExposed.Services
{

    // Defines the interface that will be implemented..
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    // the AuthMessageSender class implements the IMailSender interface..
    public class AuthMessageSender : IEmailSender
    {
        private readonly IOptions<AuthMessageSenderOptions> _optionsMail; // appSettings
        private ILogger<AuthMessageSender> _logger { get; set; }

        public AuthMessageSender(
            IOptions<AuthMessageSenderOptions> optionsAccessor,
            ILogger<AuthMessageSender> logger)
        {
            _optionsMail = optionsAccessor; // Parameters in AuthMessageSenderOptions
            _logger = logger;
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new SendGridMessage()
            {
                From = new EmailAddress(_optionsMail.Value.SmtpFromAddress, _optionsMail.Value.SmtpFromName),
                Subject = subject,
                PlainTextContent = message
                //HtmlContent = "<strong>Html Hello, Email!</strong>"
            };
            emailMessage.AddTo(email); // AddTos for multiple recipients in a List<>

            _logger.LogInformation("### SendGridMessage: Creating e-mail object to be sent to {0} from {1}.", email, emailMessage.From.Email);
            //_logger.LogInformation("### SendGridUser is {0} and SendGridKey is {1}.", _optionsMail.Value.SendGridUser, _optionsMail.Value.SendGridKey);

            // to appsettings??
            var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");

            _logger.LogInformation("### SendGrid API Key: {0}", apiKey);

            var client = new SendGridClient(apiKey);

            if (client != null)
            {
                var result = await client.SendEmailAsync(emailMessage);
                _logger.LogInformation("### The result is {0} while sending an e-mail {1}.", result.StatusCode, email);
            }
            else
            {
                _logger.LogError("### Something went wrong while trying to create a client to send an e-mail to {0}.", email);
            }
        }

    }
}
