using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Api.Controllers;
[ApiController]
[ApiVersion(1)]
[ApiVersion(2)]

[Route("api/v{v:apiVersion}/workouts")]
public class TestController : ControllerBase
{
    [MapToApiVersion(1)]
    [HttpGet("hello")]
    public IActionResult GetWorkoutV1()
    {
        return Ok("hello1");
    }

    [MapToApiVersion(2)]

    [HttpGet("hello")]
    public IActionResult GetWorkoutV2()
    {
        return Ok("hello2");
    }


}
