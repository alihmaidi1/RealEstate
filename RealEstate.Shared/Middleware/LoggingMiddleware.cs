using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RealEstate.Shared.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = Stopwatch.GetTimestamp();

        try
        {
            _logger.LogInformation("Incoming Request: {Method} {Path}", context.Request.Method, context.Request.Path);

            await _next(context);

            // Log response details
            var elapsedMs = GetElapsedMilliseconds(startTime, Stopwatch.GetTimestamp());
            _logger.LogInformation("Request Completed: {StatusCode} in {Elapsed}ms",context.Response.StatusCode,elapsedMs);
        }
        catch (Exception ex)
        {
            // Log exceptions
            var elapsedMs = GetElapsedMilliseconds(startTime, Stopwatch.GetTimestamp());
            _logger.LogError(ex, "Request Failed: {Method} {Path} ({Elapsed}ms)",context.Request.Method,context.Request.Path,elapsedMs);

            throw; 
        }
    }
    
    private static double GetElapsedMilliseconds(long start, long stop)
    {
        return (stop - start) * 1000 / (double)Stopwatch.Frequency;
    }



}
