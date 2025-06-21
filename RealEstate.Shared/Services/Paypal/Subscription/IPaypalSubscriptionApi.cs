// Licensed to the .NET Foundation under one or more agreements.

using RealEstate.Shared.Services.Paypal.Subscription.Dto;
using Refit;

namespace RealEstate.Shared.Services.Paypal.Subscription;

public interface IPaypalSubscriptionApi
{
    
    [Post("/v1/catalogs/products")]
    Task<ProductResponse> CreateProduct(
        [Header("Authorization")] string bearerToken,
        [Body] ProductRequest request);
    
    [Post("/v1/billing/plans")]
    Task<PlanResponse> CreatePlan(
        [Header("Authorization")] string bearerToken,
        [Body] PlanRequest request);
    
    [Post("/v1/billing/subscriptions")]
    Task<SubscriptionResponse> CreateSubscription(
        [Header("Authorization")] string bearerToken,
        [Body] SubscriptionRequest request);
    
    
    [Post("/v1/billing/subscriptions/{id}/activate")]
    Task ActivateSubscription(
        [Header("Authorization")] string bearerToken,
        [AliasAs("id")] string subscriptionId);
    
    
}
