using Company.A1.PL.Helpers;
using Twilio.Rest.Api.V2010.Account;

namespace Company.A1.PL.Authentication
{
    public interface ITwilioService
    {
        public MessageResource SendSms(Sms sms);
    }
}
