// Licensed to the .NET Foundation under one or more agreements.

using System.Net.Http.Headers;
using System.Text;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;

namespace RealEstate.Infrastructure.BackgroundServer.Filter;
public class HangfireCustomBasicAuthenticationFilter : IDashboardAuthorizationFilter
{
  private readonly ILogger _logger;
  private const string _AuthenticationScheme = "Basic";

  public string User { get; set; }

  public string Pass { get; set; }

  public HangfireCustomBasicAuthenticationFilter()
    : this((ILogger) new NullLogger<HangfireCustomBasicAuthenticationFilter>())
  {
  }

  public HangfireCustomBasicAuthenticationFilter(ILogger logger) => this._logger = logger;

  public bool Authorize(DashboardContext context)
  {
    HttpContext httpContext = context.GetHttpContext();
    StringValues header = httpContext.Request.Headers["Authorization"];
    if (HangfireCustomBasicAuthenticationFilter.Missing_Authorization_Header(header))
    {
      this._logger.LogInformation("Request is missing Authorization Header");
      this.SetChallengeResponse(httpContext);
      return false;
    }
    AuthenticationHeaderValue authValues = AuthenticationHeaderValue.Parse((string) header);
    if (HangfireCustomBasicAuthenticationFilter.Not_Basic_Authentication(authValues))
    {
      this._logger.LogInformation("Request is NOT BASIC authentication");
      this.SetChallengeResponse(httpContext);
      return false;
    }
    BasicAuthenticationTokens authenticationTokens = HangfireCustomBasicAuthenticationFilter.Extract_Authentication_Tokens(authValues);
    if (authenticationTokens.Are_Invalid())
    {
      this._logger.LogInformation("Authentication tokens are invalid (empty, null, whitespace)");
      this.SetChallengeResponse(httpContext);
      return false;
    }
    if (authenticationTokens.Credentials_Match(this.User, this.Pass))
    {
      this._logger.LogInformation("Awesome, authentication tokens match configuration!");
      return true;
    }
    this._logger.LogInformation($"Boo! Authentication tokens [{authenticationTokens.Username}] [{authenticationTokens.Password}] do not match configuration");
    this.SetChallengeResponse(httpContext);
    return false;
  }

  private static bool Missing_Authorization_Header(StringValues header)
  {
    return string.IsNullOrWhiteSpace((string) header);
  }

  private static BasicAuthenticationTokens Extract_Authentication_Tokens(
    AuthenticationHeaderValue authValues)
  {
    return new BasicAuthenticationTokens(Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter)).Split(':'));
  }

  private static bool Not_Basic_Authentication(AuthenticationHeaderValue authValues)
  {
    return !"Basic".Equals(authValues.Scheme, StringComparison.InvariantCultureIgnoreCase);
  }

  private void SetChallengeResponse(HttpContext httpContext)
  {
    httpContext.Response.StatusCode = 401;
    httpContext.Response.Headers.Append("WWW-Authenticate", (StringValues) "Basic realm=\"Hangfire Dashboard\"");
  }
}
