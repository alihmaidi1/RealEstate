using Twilio.Rest.Api.V2010.Account;

namespace RealEstate.Shared.Services.Sms.Twilio;

public interface ISmsTwilioService
{

    public Task<MessageResource> Send(string mobileNumber,string Body);
    
    
    
}
