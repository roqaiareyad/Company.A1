using Company.A1.PL.Helpers;
using Company.A1.PL.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Voice;
using Sms = Company.A1.PL.Helpers.Sms;

namespace Company.A1.PL.Authentication;
public class TwilioService(IOptions<TwilioSettings> options) : ITwilioService
{
    private readonly TwilioSettings _options = options.Value;

    public MessageResource SendSms(Sms sms)
    {
        // Initialze Connection
        TwilioClient.Init(_options.AccountSID, _options.AuthToken);

        // Build and Return Message
        var message = MessageResource.Create(
            body: sms.Body,
            to: sms.To,
            from: new Twilio.Types.PhoneNumber(_options.PhoneNumber)
            );
        return message;
    }
}