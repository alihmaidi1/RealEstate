// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.AspNetCore.Identity;

namespace RealEstate.Domain.Security;

public class User: IdentityUser<Guid>
{

    public User()
    {

        Id = Guid.NewGuid();
    }
    
    public UserType UserType { get; set; }=UserType.Customer;
    
}
