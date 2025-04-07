using Company.A1.DAL.Models.Sms;
using Twilio.Rest.Api.V2010.Account;

namespace Company.A1.PL.Helpers
{
    public interface ITwilioService
    {
        public MessageResource SendSms(Sms sms);
    }
}
