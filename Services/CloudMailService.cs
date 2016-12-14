using System.Diagnostics;

namespace cityInfo.Services
{
    public class CloudMailService: IMailService
    {
        private string _mailTo = "admin@company.com";
        private string _mailFrom = "noreply@complany.com";

        public void Send(string subject, string Message)
        {
            Debug.WriteLine($"Mail sent from {_mailFrom} to {_mailTo} with Cloud mail service.");
            Debug.WriteLine($"Subject: {subject}.");
            Debug.WriteLine($"Message: {Message}");
        }
    }
}