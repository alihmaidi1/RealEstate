// Licensed to the .NET Foundation under one or more agreements.

using System.ComponentModel.DataAnnotations;

namespace RealEstate.Shared.Services.Sms.Whatsapp;

public class WhatsappMessageSetting
{
    
    
    [Required]
    public string token { get; set; }
    
    [Required]
    public string phone { get; set; }
}
