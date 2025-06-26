// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Security;
using RealEstate.Infrastructure.Repositories.Base.Repository;

namespace RealEstate.Infrastructure.Repositories.Base.Security;

public class AccountRepository:BaseRepository<User>, IAccountRepository
{
    public AccountRepository(RealEstateDbContext context) : base(context)
    {
    }
}
