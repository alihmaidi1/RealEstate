// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.Abstraction.Entities.EntityPagination;

public static class PaginationExtension
{
    
    public static PageList<T> ToPagedList<T>(this IQueryable<T> source, int? pageNumber, int? pageSize)
    {
        if (source == null) throw new ArgumentNullException($"Iqueryable of {typeof(T)} is null");
        if (pageNumber is null || pageSize is null)
        {
            var sourceList = source.ToList();
            return new PageList<T>(sourceList, sourceList.Count);

        }

        var PaginatePageNumber = (int)(pageNumber <= 0 ? 1 : pageNumber);
        var PaginatePageSize = (int)(pageSize <= 0 ? 10 : pageSize);
        var items = source.Skip((PaginatePageNumber - 1) * PaginatePageSize).Take(PaginatePageSize).ToList();
        var count = source.Count();
        return new PageList<T>(items.ToList(), count, PaginatePageNumber, pageSize);
    }
    
    
    public static async Task<PageList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int? pageNumber, int? pageSize)
    {
        return ToPagedList<T>(source, pageNumber, pageSize);
    }
}
