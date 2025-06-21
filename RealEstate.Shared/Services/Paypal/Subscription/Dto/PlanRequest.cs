// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Paypal.Subscription.Dto;

public class PlanRequest
{
    public string ProductId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    
    public List<BillingCycle>  billingCycles { get; set; }
    public PaymentPreferences  paymentPreferences { get; set; }
    
}


public class BillingCycle
{
    
    public Frequency frequency { get; set; }
    
    public string tenure_type { get; set; }
    
    public int sequence { get; set; }
    
    public int total_cycles { get; set; }
    
    public PricingScheme pricingScheme { get; set; }
    
}

public class Frequency
{
    public string interval_unit { get; set; }
    
    public int interval_count { get; set; }
    
}

public class PricingScheme
{
    public Money fixed_price { get; set; }
    
}

public class Money
{
    
    public string currency_code { get; set; }
    
    public string value { get; set; }
}

public class PaymentPreferences
{
    
    public bool auto_bill_outstanding { get; set; }
    
    public Money setup_fee { get; set; }

    public string setup_fee_failure_action { get; set; } = "CONTINUE";
}