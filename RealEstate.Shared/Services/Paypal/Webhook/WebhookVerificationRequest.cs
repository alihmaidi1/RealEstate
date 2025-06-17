// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Paypal.Webhook;

public class WebhookVerificationRequest
{
    public string transmission_id { get; set; }
    
    public string transmission_time { get; set; }
    
    public string transmission_sig { get; set; }
    
    public string cert_url { get; set; }
    
    public string auth_algo { get; set; }
    
    public string webhook_id { get; set; }
    
    public object webhook_event { get; set; }
    
}

