using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace RealEstate.Shared.Services.Sms;

public interface ISmsTwilioService
{

    public Task<MessageResource> Send(string mobileNumber,string Body);
    
    
    
}
