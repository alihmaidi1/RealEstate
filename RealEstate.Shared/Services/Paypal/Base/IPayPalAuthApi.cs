// Licensed to the .NET Foundation under one or more agreements.

using RealEstate.Shared.Services.Paypal.Dto;
using Refit;

namespace RealEstate.Shared.Services.Paypal.HttpClientCall;

public interface IPayPalAuthApi
{
    [Post("/v1/oauth2/token")]
    [Headers("Accept: application/json", "Accept-Language: en_US")]
    Task<PayPalAccessTokenResponse> GetAccessTokenAsync(
        [Header("Authorization")] string authHeader,
        [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);
    
}
