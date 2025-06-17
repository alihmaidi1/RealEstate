// Licensed to the .NET Foundation under one or more agreements.

using System.ComponentModel.DataAnnotations;

namespace RealEstate.Shared.Services.Paypal.Dto;

public class PaypalSetting
{
    [Required]
    public string ClientId { get; set; }
    
    [Required]
    public string Secret { get; set; }
    
    [Required]
    public string Environment { get; set; }
    

    
    [Required,Url]
    public string ReturnUrl { get; set; }

    
    [Required,Url]
    public string CancelUrl { get; set; }
    
    
    [Required]
    public string WebhookId { get; set; }
    
}
