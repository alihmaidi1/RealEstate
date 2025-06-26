// Licensed to the .NET Foundation under one or more agreements.

using System.Net;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Contract.Auth.Admin.Login;
using RealEstate.Domain.Security;
using RealEstate.Shared.Abstraction.CQRS;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Application.Auth.Admin.Login;

public class LoginAdminHandler: ICommandHandler<AdminLoginRequest>
{
    
    public LoginAdminHandler(IAccountRepository  accountRepository)
    {
        
    }
    public async Task<IActionResult> Handle(AdminLoginRequest request, CancellationToken cancellationToken)
    {
        // 1- try to get admin
        // var admin = _accountRepository.GetQueryable().FirstOrDefault(x=>x.PhoneNumber == request.Phone);
        // if (admin == null)
        // {
        //     return Result.Failure<AdminLoginResponse>(Error.ValidationFailures(""));
        // }
        // 2- if not exists return error
        // 3- check if password correct
        // 4- generate token info and return data
        return Result.Success(new AdminLoginResponse()).ToJsonResult();

    }
}
