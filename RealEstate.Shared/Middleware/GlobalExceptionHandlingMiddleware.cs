// Licensed to the .NET Foundation under one or more agreements.

using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Shared.Middleware;

public class GlobalExceptionHandlingMiddleware: IMiddleware
{
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);

        }
        catch (Exception e)
        {

            ProblemDetails problem = new ProblemDetails()
            {
                
                Status = StatusCodes.Status500InternalServerError,
                Type = "Server Error",
                Detail = e.Message,
                Title = "Server Error"
                
            };
            var json = JsonSerializer.Serialize(problem);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

    }
}
