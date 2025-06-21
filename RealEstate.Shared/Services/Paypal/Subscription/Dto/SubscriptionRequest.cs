// Licensed to the .NET Foundation under one or more agreements.

using RealEstate.Shared.Services.Paypal.Checkout.Dto;

namespace RealEstate.Shared.Services.Paypal.Subscription.Dto;

public class SubscriptionRequest
{
    public string plan_id { get; set; }
    
    public ApplicationContext applicationContext { get; set; }
}
