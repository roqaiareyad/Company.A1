using System.Net;
using System.Net.Mail;
using Company.A1.PL.Helpers;

namespace Company.A1.PL.Authentication
{
    public class MailSettings
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
