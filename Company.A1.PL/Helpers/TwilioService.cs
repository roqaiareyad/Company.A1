using Company.A1.DAL.Models.Sms;
using Company.A1.PL.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Company.A1.PL.Helpers
{

    public class TwilioService(IOptions<TwilioSettings> options) : ITwilioService
    {

        public MessageResource SendSms(Sms sms)
        {
            // Intialize Connection

            TwilioClient.Init(options.Value.AccountSID, options.Value.AuthToken);
            //TwilioClient.Init("ACa9386dff8f65af55b9bd59c7f51c004e", "aef6fc148d53e13a516ce59d640073e8");

            // Build Message

            var message = MessageResource.Create(
                body : sms.Body,
                to: sms.To,
                from: options.Value.PhoneNumber
                //from:new Twilio.Types.PhoneNumber("+201143941265")
                );

            // return Message
            return message;
        }

    }
}