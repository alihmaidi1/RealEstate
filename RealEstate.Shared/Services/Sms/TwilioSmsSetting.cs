using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Shared.Services.Sms;

public class TwilioSmsSetting
{
    [Required]
    public string AccountSID{get;set;}

    [Required]
    
    public string AuthToken{get;set;}

    [Required]
    
    public string TwilioPhoneNumber{get;set;}

    
}
