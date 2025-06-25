// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.AspNetCore.Authorization;

namespace RealEstate.Infrastructure.Authorization;

public class PermissionRequirement: IAuthorizationRequirement
{
    public string Permission { get; set; }    


    public PermissionRequirement(string Permission)
    {
        this.Permission = Permission;

    }
    
}
