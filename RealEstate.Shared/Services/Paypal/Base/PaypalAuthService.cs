// Licensed to the .NET Foundation under one or more agreements.

using System.Text;
using Microsoft.Extensions.Options;
using RealEstate.Shared.Services.Paypal.Dto;
using RealEstate.Shared.Services.Paypal.HttpClientCall;

namespace RealEstate.Shared.Services.Paypal.Base;

public class PaypalAuthService: IPaypalAuthService
{
    
    private string? _accessToken;
    private DateTime _tokenExpiration;
    
    private readonly PaypalSetting _config;
    
    private readonly IPayPalAuthApi _authApi;

    public PaypalAuthService(IOptions<PaypalSetting> paypalSetting,IPayPalAuthApi authApi)
    {
        _config = paypalSetting.Value;
        _authApi = authApi;
    }
    public async Task<string> GetAccessTokenAsync()
    {
        if (IsValidToken())
            return _accessToken;

        var authHeader = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_config.ClientId}:{_config.Secret}"))}";
        
        var data = new Dictionary<string, object>
        {
            {"grant_type", "client_credentials"}
        };

        var response = await _authApi.GetAccessTokenAsync(authHeader, data);
        _accessToken = response.access_token;
        _tokenExpiration = DateTime.UtcNow.AddSeconds(response.expires_in - 30); // 30 ثانية هامش أمان
        return _accessToken!;
    }

    private bool IsValidToken()
    {
        return !string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpiration;
    }
}
