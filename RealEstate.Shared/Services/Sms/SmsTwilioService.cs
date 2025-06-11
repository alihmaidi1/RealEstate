using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RealEstate.Shared.Services.Sms;

public class SmsTwilioService : ISmsTwilioService
{
    private readonly TwilioSmsSetting _twilioSmsSetting;
    public SmsTwilioService(IOptions<TwilioSmsSetting> twilioSettings)
    {
        _twilioSmsSetting = twilioSettings.Value;

    }
    public async Task<MessageResource> Send(string mobileNumber, string Body)
    {
        
        TwilioClient.Init(_twilioSmsSetting.AccountSID,_twilioSmsSetting.AuthToken);
        var messageOptions = new CreateMessageOptions(new PhoneNumber(mobileNumber));
        messageOptions.From = new PhoneNumber("+15676011499");
        messageOptions.Body = Body;
        var result = MessageResource.Create(messageOptions);
        return result;
    }
}

