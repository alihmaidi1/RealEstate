// Licensed to the .NET Foundation under one or more agreements.

using RealEstate.Shared.Services.Paypal.Checkout.Dto;
using Refit;

namespace RealEstate.Shared.Services.Paypal.Checkout;

public interface IPayPalOrdersApi
{
    [Post("/v2/checkout/orders")]
    Task<CreateOrderResponse> CreateOrderAsync(
        [Header("Authorization")] string authToken,
        [Header("Content-Type")] string contentType,
        [Body] CreateOrderRequest request);
    
    
    [Post("/v2/payments/authorizations/{authorizationId}/capture")]
    Task<CaptureAuthorizationResponse> CaptureAuthorizationAsync(
        [Header("Authorization")] string authToken,
        [AliasAs("authorizationId")] string authorizationId,
        [Body] CaptureAuthorizationRequest request);
    
    
    [Post("/v2/payments/authorizations/{authorizationId}/void")]
    Task VoidAuthorizationAsync(
        [Header("Authorization")] string authToken,
        [AliasAs("authorizationId")] string authorizationId);
    
}
