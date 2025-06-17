// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Paypal.Checkout.Dto;

public class CaptureAuthorizationRequest
{
    public Amount amount { get; set; } = new();
    
    public bool final_capture { get; set; } = true;

}
