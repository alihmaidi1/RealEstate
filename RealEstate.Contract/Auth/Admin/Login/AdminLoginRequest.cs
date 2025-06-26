// Licensed to the .NET Foundation under one or more agreements.

using RealEstate.Shared.Abstraction.CQRS;

namespace RealEstate.Contract.Auth.Admin.Login;

public class AdminLoginRequest: ICommand
{
    public string Phone { get; set; }
    public string Password { get; set; }
    
}
