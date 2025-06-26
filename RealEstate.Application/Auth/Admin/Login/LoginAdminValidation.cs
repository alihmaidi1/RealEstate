// Licensed to the .NET Foundation under one or more agreements.

using FluentValidation;
using RealEstate.Contract.Auth.Admin.Login;

namespace RealEstate.Application.Auth.Admin.Login;

public class LoginAdminValidation: AbstractValidator<AdminLoginRequest>
{

    public LoginAdminValidation()
    {
        RuleFor(x => x.Phone)
            .NotNull();

        RuleFor(x => x.Password)
            .NotNull();
    }
    
}
