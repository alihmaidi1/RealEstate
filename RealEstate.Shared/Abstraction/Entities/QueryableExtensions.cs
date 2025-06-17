// Licensed to the .NET Foundation under one or more agreements.

using System.Linq.Expressions;

namespace RealEstate.Shared.Abstraction.Entities;

public static class QueryableExtensions
{

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition,
        Expression<Func<T, bool>> predicate)
    {

        if (condition)
        {
            return query.Where(predicate);
        }

        return query;

    }
    
}
