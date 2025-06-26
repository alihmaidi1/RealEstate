// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.AspNetCore.Identity;
using RealEstate.Shared.Abstraction.Entities.Entity;

namespace RealEstate.Domain.Security;

public class User: IdentityUser<Guid>, IEntity
{

    public User()
    {

        Id = Guid.NewGuid();
    }
    
    public UserType UserType { get; set; }=UserType.Customer;
    
}
