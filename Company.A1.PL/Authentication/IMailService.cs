using Company.A1.PL.Helpers;

namespace Company.A1.PL.Authentication
{
    public interface IMailService
    {
        public void SendEmail(Email email);
    }
}
