// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using RealEstate.Shared.Services.Paypal.Base;
using RealEstate.Shared.Services.Paypal.Checkout.Dto;
using RealEstate.Shared.Services.Paypal.Dto;
using Refit;

namespace RealEstate.Shared.Services.Paypal.Checkout;

public class PaypalCheckoutService: IPaypalCheckoutService
{
    
    private readonly PaypalSetting _config;
    private readonly AsyncRetryPolicy _retryPolicy;
    private IPaypalAuthService  _paypalAuthService;
    private IPayPalOrdersApi _payPalOrdersApi;
    public PaypalCheckoutService(IOptions<PaypalSetting> config,IPayPalOrdersApi payPalOrdersApi,IPaypalAuthService  paypalAuthService)
    {
        _payPalOrdersApi= payPalOrdersApi;
        _paypalAuthService = paypalAuthService;
        _config = config.Value;
        _retryPolicy = Policy
            .Handle<ApiException>(ex => ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(3, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));


    }

    public async Task<string> CreateOrderAsync(decimal amount, string currency = "USD")
    {

        var token = await _paypalAuthService.GetAccessTokenAsync();

        var request = new CreateOrderRequest
        {
            purchase_units = new List<PurchaseUnit>
            {
                new()
                {
                    amount = new Amount
                    {
                        currency_code = currency,
                        value = amount.ToString("0.00")
                    }
                }
            },
            application_context = new ApplicationContext
            {
                return_url = _config.ReturnUrl,
                cancel_url = _config.CancelUrl
            }
        };
        var response = await _retryPolicy.ExecuteAsync(async () => 
            await _payPalOrdersApi.CreateOrderAsync($"Bearer {token}", "application/json", request));
        
        var approvalLink = response.links.FirstOrDefault(l => l.rel == "approve")?.href;
        if (string.IsNullOrEmpty(approvalLink))
            throw new Exception("Approval link not found in PayPal response");


        return approvalLink;
    }

    public async Task<CaptureAuthorizationResponse> CaptureAuthorizationAsync(string authorizationId, decimal amount, string currency = "USD")
    {
        var token = await _paypalAuthService.GetAccessTokenAsync();
        
        var request = new CaptureAuthorizationRequest
        {
            amount = new Amount {
                currency_code = currency,
                value = amount.ToString("0.00")
            },
            final_capture = true
        };
        
        
        var response = await _retryPolicy.ExecuteAsync(async () => 
            await _payPalOrdersApi.CaptureAuthorizationAsync(
                $"Bearer {token}", authorizationId, request));

        return response;
    }
    
    public async Task VoidAuthorizationAsync(string authorizationId)
    {   
        var token = await _paypalAuthService.GetAccessTokenAsync();
        
        await _retryPolicy.ExecuteAsync(async () => await _payPalOrdersApi.VoidAuthorizationAsync(
            $"Bearer {token}", authorizationId));
        
        
    }

}
