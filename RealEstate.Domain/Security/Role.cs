// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.AspNetCore.Identity;

namespace RealEstate.Domain.Security;

public class Role: IdentityRole<Guid>
{
    public Role()
    {
        
        Id=Guid.NewGuid();
    }
    
}
