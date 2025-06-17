// Licensed to the .NET Foundation under one or more agreements.

using Refit;

namespace RealEstate.Shared.Services.Paypal.Webhook;

public interface IPayPalWebhookApi
{
    [Post("/v1/notifications/verify-webhook-signature")]
    Task<WebhookVerificationResult> VerifyWebhookSignatureAsync(
        [Header("Authorization")] string authorization,
        [Body] WebhookVerificationRequest request);
    
}
