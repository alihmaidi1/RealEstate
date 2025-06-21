// Licensed to the .NET Foundation under one or more agreements.

using RealEstate.Shared.Services.Paypal.Subscription.Dto;

namespace RealEstate.Shared.Services.Paypal.Subscription;

public interface IPaypalSubscriptionService
{
    Task<string> GetOrCreateProductAsync(string productName,string description,string category,string type);

    Task<string> CreatePlanAsync(string productId, string planName,string description,string category,List<BillingCycle> billingCycles,PaymentPreferences paymentPreferences);

    
    Task<string> CreateSubscriptionAsync(string planId);
    Task ActivateSubscriptionAsync(string subscriptionId);

    
}
