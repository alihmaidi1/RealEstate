using System.ComponentModel.DataAnnotations;

namespace RealEstate.Shared.Services.Sms.Twilio;

public class TwilioSmsSetting
{
    [Required]
    public string AccountSID{get;set;}

    [Required]
    
    public string AuthToken{get;set;}

    [Required]
    
    public string TwilioPhoneNumber{get;set;}

    
}
