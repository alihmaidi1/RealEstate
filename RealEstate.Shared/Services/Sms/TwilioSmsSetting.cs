using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Shared.Services.Sms;

public class TwilioSmsSetting
{

    public string AccountSID{get;set;}
    
    public string AuthToken{get;set;}

    public string TwilioPhoneNumber{get;set;}

    
}
