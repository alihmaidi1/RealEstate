using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Shared.Services.Sms;

namespace RealEstate.Api.Controllers;
[ApiController]
[Route("[controller]/[action]")]

public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetWorkout([FromServices] ISmsTwilioService smsTwilioService)
    {
        await smsTwilioService.Send("+18777804236", "hello from ali Test");
        return Ok("hello1");
    }



}
