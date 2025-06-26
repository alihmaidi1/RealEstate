// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.AspNetCore.Http;

namespace RealEstate.Shared.Middleware;

public class RequestIdmiddleware: IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!context.Request.Headers.ContainsKey("RequestId"))
        {
            context.Request.Headers.Append("RequestId", Guid.NewGuid().ToString());
            context.Response.OnStarting(() =>
            {

                context.Response.Headers.Append("RequestId", context.Request.Headers["RequestId"]);
                return Task.CompletedTask;

            });
            await next(context);
        }
    }
}
