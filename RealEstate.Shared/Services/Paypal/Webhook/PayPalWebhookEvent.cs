// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Paypal.Webhook;

public class PayPalWebhookEvent
{
    public string id { get; set; }
    
    public string event_type { get; set; }
    
    public object resource { get; set; }

}
