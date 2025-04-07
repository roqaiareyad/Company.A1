using System.Net;
using System.Net.Mail;

namespace Company.A1.PL.Helpers
{
    public class EmailSetting
    {
        public static bool SendEmail(Email email)
        {

            // Mail Server : Gmail
            // SMTP
            try
            {
                var Client = new SmtpClient("smtp.gmail.com", 587);
                Client.EnableSsl = true;
                Client.Credentials = new NetworkCredential("roqaiareyad509@gmail.com", "zukftzjfcxvyskqb");
                Client.Send("roqaiareyad509@gmail.com", email.To, email.Subject, email.Body);

                return true;
            }
            catch (Exception e) 
            { 
                return false;   
            }
           
           
        }
        
    }
}
