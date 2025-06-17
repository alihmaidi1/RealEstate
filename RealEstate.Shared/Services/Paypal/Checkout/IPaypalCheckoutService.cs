// Licensed to the .NET Foundation under one or more agreements.

using RealEstate.Shared.Services.Paypal.Checkout.Dto;

namespace RealEstate.Shared.Services.Paypal.Checkout;

public interface IPaypalCheckoutService
{
    public  Task<string> CreateOrderAsync(decimal amount, string currency = "USD");

    public Task<CaptureAuthorizationResponse> CaptureAuthorizationAsync(
        string authorizationId,
        decimal amount,
        string currency = "USD");

    public Task VoidAuthorizationAsync(string authorizationId);


}
