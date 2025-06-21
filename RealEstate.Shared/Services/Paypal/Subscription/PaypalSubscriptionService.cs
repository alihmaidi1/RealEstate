// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.Extensions.Options;
using RealEstate.Shared.Services.Paypal.Base;
using RealEstate.Shared.Services.Paypal.Checkout.Dto;
using RealEstate.Shared.Services.Paypal.Dto;
using RealEstate.Shared.Services.Paypal.Subscription.Dto;

namespace RealEstate.Shared.Services.Paypal.Subscription;

public class PaypalSubscriptionService: IPaypalSubscriptionService
{
    private readonly IPaypalAuthService _paypalAuthService;
    private readonly PaypalSetting _config;
    
    private readonly IPaypalSubscriptionApi _paypalSubscriptionApi;
    public PaypalSubscriptionService(IOptions<PaypalSetting> config,IPaypalAuthService paypalAuthService,IPaypalSubscriptionApi paypalSubscriptionApi)
    {

        _paypalAuthService = paypalAuthService;
        _config=config.Value;
        _paypalSubscriptionApi= paypalSubscriptionApi;

    }
    
    public async Task<string> GetOrCreateProductAsync(string productName,string description,string category,string type)
    {
        var token = await _paypalAuthService.GetAccessTokenAsync();
        
            var request = new ProductRequest
            {
                
                name = productName,
                description = description,
                category = category,
                type = type
            };
            
            var response = await _paypalSubscriptionApi.CreateProduct($"Bearer {token}", request);
            return response.id;
        

    }

    public async Task<string> CreatePlanAsync(string productId, string planName,string description,string category,List<BillingCycle> billingCycles,PaymentPreferences paymentPreferences)
    {
        var token = await _paypalAuthService.GetAccessTokenAsync();
        var request = new PlanRequest
        {
            ProductId = productId,
            name = planName,
            description = description,
            billingCycles = billingCycles,
            paymentPreferences = paymentPreferences
            
        };

        var response = await _paypalSubscriptionApi.CreatePlan($"Bearer {token}", request);
        return response.id;
    }

    public async Task<string> CreateSubscriptionAsync(string planId)
    {   
        var token = await _paypalAuthService.GetAccessTokenAsync();
        var request = new SubscriptionRequest
        {
            plan_id = planId,
            applicationContext = new ApplicationContext
            {
                
                return_url = _config.ReturnUrl,
                cancel_url = _config.CancelUrl
            }
            
        };
        
        var response = await _paypalSubscriptionApi.CreateSubscription($"Bearer {token}", request);
        return response.links
            .First(link => link.rel == "approve")
            .href;
    }

    public async Task ActivateSubscriptionAsync(string subscriptionId)
    {
        var token = await _paypalAuthService.GetAccessTokenAsync();
        await _paypalSubscriptionApi.ActivateSubscription($"Bearer {token}", subscriptionId);
    }
}
