// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Paypal.Subscription.Dto;

public class ProductRequest
{
    
    public string name { get; set; }

    public string type { get; set; } = "SERVICE";

    public string category { get; set; } = "SOFTWARE";
    
    public string description { get; set; }
    

}
