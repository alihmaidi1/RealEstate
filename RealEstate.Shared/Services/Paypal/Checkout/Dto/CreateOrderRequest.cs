// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Paypal.Checkout.Dto;

public class CreateOrderRequest
{
    public string intent { get; set; } = "CAPTURE";
    
    public List<PurchaseUnit> purchase_units { get; set; } = new();
    
    public ApplicationContext application_context { get; set; } = new();
    
}

public class PurchaseUnit
{
    public Amount amount { get; set; } = new();
}

public class Amount
{
    public string currency_code { get; set; } = "USD";
    
    public string value { get; set; } = "0.00";
}

public class ApplicationContext
{
    public string return_url { get; set; } = string.Empty;
    
    public string cancel_url { get; set; } = string.Empty;
}


