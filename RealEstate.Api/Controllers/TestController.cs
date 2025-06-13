using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Shared.Filters;
using RealEstate.Shared.Security.SecretManager;
using RealEstate.Shared.Services.Sms;

namespace RealEstate.Api.Controllers;
[ApiController]
[Route("[controller]/[action]")]

public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetKey(string key,[FromServices] ISecretManagerService secretManagerService)
    {
        return Ok(await secretManagerService.GetSecret(key));
    }

    
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    [HttpGet]
    public async Task<IActionResult> RemoveKey(string key,[FromServices] ISecretManagerService secretManagerService)
    {
        await secretManagerService.InvalidateSecret(key);
        return Ok("done!");
    }



}
