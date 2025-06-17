// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Paypal.Checkout.Dto;

public class CreateOrderResponse
{
    public string id { get; set; } = string.Empty;
    
    public string status { get; set; } = string.Empty;
    
    public List<Link> links { get; set; } = new();
}

public class Link
{
    public string href { get; set; } = string.Empty;
    
    public string rel { get; set; } = string.Empty;
    
    public string method { get; set; } = "GET";
}
