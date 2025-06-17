// Licensed to the .NET Foundation under one or more agreements.

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using RealEstate.Shared.Services.Paypal.Base;
using RealEstate.Shared.Services.Paypal.Dto;
using Refit;

namespace RealEstate.Shared.Services.Paypal.Webhook;

public class PayPalWebhookService: IPayPalWebhookService
{
    private readonly string _webhookId;
    private readonly IPaypalAuthService  _authService;
    private readonly IPayPalWebhookApi _payPalWebhookApi;
    private readonly AsyncRetryPolicy _retryPolicy;

    public PayPalWebhookService(IPayPalWebhookApi payPalWebhookApi,IOptions<PaypalSetting> PaypalSetting,IPaypalAuthService  authService)
    {
        _webhookId= PaypalSetting.Value.WebhookId;
        _authService=authService;
        _payPalWebhookApi = payPalWebhookApi;
        _retryPolicy = Policy
            .Handle<ApiException>(ex => ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(3, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));


    }
    
    public async Task<bool> VerifyWebhookSignature(string json, IHeaderDictionary headers)
    {
        var request = new WebhookVerificationRequest
        {
            transmission_id = headers["paypal-transmission-id"],
            transmission_time = headers["paypal-transmission-time"],
            transmission_sig = headers["paypal-transmission-sig"],
            cert_url = headers["paypal-cert-url"],
            auth_algo = headers["paypal-auth-algo"],
            webhook_id = _webhookId,
            webhook_event = JsonSerializer.Deserialize<object>(json)
        };
        var accessToken = await _authService.GetAccessTokenAsync();
        var response = await _retryPolicy.ExecuteAsync(async () => 
            await _payPalWebhookApi.VerifyWebhookSignatureAsync(
                $"Bearer {accessToken}",
                request
            ));

        return response.verification_status == "SUCCESS";

    }

    public Task ProcessWebhookEvent(string json)
    {
        var webhookEvent = JsonSerializer.Deserialize<PayPalWebhookEvent>(json);
        switch (webhookEvent.event_type)
        {
            case "CHECKOUT.ORDER.APPROVED":
                break;
            case "CHECKOUT.ORDER.CANCELLED":
                break;
        }
        throw new NotImplementedException();
    }
}
