// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Services.Paypal.Base;

public interface IPaypalAuthService
{
    public  Task<string> GetAccessTokenAsync();

    
}
