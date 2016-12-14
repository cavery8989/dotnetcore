using System.Diagnostics;

namespace cityInfo.Services
{
    public class LocalMailservice: IMailService
    {
        private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subject, string Message)
        {
            Debug.WriteLine($"Mail sent from {_mailFrom} to {_mailTo} with local mail service.");
            Debug.WriteLine($"Subject: {subject}.");
            Debug.WriteLine($"Message: {Message}");
        }
    }
}