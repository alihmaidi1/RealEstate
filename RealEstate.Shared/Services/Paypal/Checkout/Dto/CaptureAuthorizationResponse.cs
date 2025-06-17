// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Paypal.Checkout.Dto;

public class CaptureAuthorizationResponse
{
    
    public string id { get; set; } = string.Empty;
    
    public string status { get; set; } = string.Empty;
    
    public Amount amount { get; set; } = new();
}
