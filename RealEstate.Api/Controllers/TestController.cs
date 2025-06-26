
using System.Net;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Contract.Auth.Admin.Login;
using RealEstate.Shared.Abstraction.CQRS;
using RealEstate.Shared.Filters;
using RealEstate.Shared.OperationResult;
using RealEstate.Shared.Security.SecretManager;
using RealEstate.Shared.Services.Paypal.Base;
using RealEstate.Shared.Services.Paypal.Checkout;
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


    [HttpGet]
    public async Task<IActionResult> GetPaypalToken([FromServices] IPaypalAuthService paypalService)
    {
        var token=await paypalService.GetAccessTokenAsync();
        return Ok(token);
    }

    
    [HttpGet]
    public async Task<IActionResult> RequestId()
    {
        return Ok("");
    }


    [HttpGet]
    public async Task<IActionResult> CreatePaypalOrder([FromServices] IPaypalCheckoutService paypalService)
    {
        var paymenturl=await paypalService.CreateOrderAsync(100);
        return Ok(paymenturl);
    }

    [HttpPost]
    public async Task<IActionResult> CaptureAuthorization([FromBody]string id,[FromServices] IPaypalCheckoutService paypalService)
    {
        var paymenturl=await paypalService.CaptureAuthorizationAsync(id,20);
        return Ok(paymenturl);
    }
    
    
    [HttpGet]
    
    public async Task<IActionResult> Test12( [FromQuery]AdminLoginRequest uploadChunkRequest, [FromServices] ICommandHandler<AdminLoginRequest> commandHandler, CancellationToken cancellationToken)
    {
        return await commandHandler.Handle(uploadChunkRequest, cancellationToken);
        
    }


    [HttpPost]
    public async Task<IActionResult> WebHook()
    {
        
        using var reader = new StreamReader(Request.Body);
        var json = await reader.ReadToEndAsync();
        var headers = Request.Headers;

        
        // if (!_webhookService.VerifyWebhookSignature(json, headers))
        // {
        //     _logger.LogWarning("Invalid webhook signature");
        //     return BadRequest("Invalid signature");
        // }

        return null;
    }


}
