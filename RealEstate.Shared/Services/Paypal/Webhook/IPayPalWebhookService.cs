// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.AspNetCore.Http;

namespace RealEstate.Shared.Services.Paypal.Webhook;

public interface IPayPalWebhookService
{
    public Task<bool> VerifyWebhookSignature(string json, IHeaderDictionary headers);
    public  Task ProcessWebhookEvent(string json);

}
