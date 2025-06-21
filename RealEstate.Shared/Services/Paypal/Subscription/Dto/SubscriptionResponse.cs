// Licensed to the .NET Foundation under one or more agreements.

using RealEstate.Shared.Services.Paypal.Checkout.Dto;

namespace RealEstate.Shared.Services.Paypal.Subscription.Dto;

public class SubscriptionResponse
{
    
    
    public string id { get; set; }
    
    public List<Link>  links { get; set; }
}
