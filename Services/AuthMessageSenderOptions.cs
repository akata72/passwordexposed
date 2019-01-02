
namespace pwdExposed.Services
{
    // http://docs.asp.net/en/latest/security/authentication/accconfirm.html

    public class AuthMessageSenderOptions
    {
        /* 
         * Set the SendGridUser and SendGridKey with the secret-manager tool. 
         * For example: dotnet user-secrets set SendGridUser ping2017
         *              dotnet user-secrets set SendGridKey ping2017!
         * The userSecretsId directory can be found in the project.json file */

        /* SendGrid Service */
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }

        /* Standard SMTP service */
        public string SmtpRelayserver { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPwd { get; set; }
        public string SmtpLocalDomain { get; set; }
        public string SmtpFromName { get; set; }
        public string SmtpFromAddress { get; set; }
    }
}
