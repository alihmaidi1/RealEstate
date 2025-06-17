// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Paypal.Dto;

public class PayPalAccessTokenResponse
{
    
    
    public string access_token { get; set; }
    public string token_type { get; set; } 

    public int expires_in { get; set; }



}
