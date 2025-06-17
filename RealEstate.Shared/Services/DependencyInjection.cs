using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using RealEstate.Shared.Security.SecretManager;
using RealEstate.Shared.Services.Paypal.Base;
using RealEstate.Shared.Services.Paypal.Checkout;
using RealEstate.Shared.Services.Paypal.Dto;
using RealEstate.Shared.Services.Paypal.HttpClientCall;
using RealEstate.Shared.Services.Paypal.Webhook;
using RealEstate.Shared.Services.Sms;
using Refit;

namespace RealEstate.Shared.Services;

public static class DependencyInjection
{

    
    public static IServiceCollection AddSharedServices(this IServiceCollection services,IConfiguration configuration)
    {
        
        
        services.AddRefitClient<IPayPalAuthApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.sandbox.paypal.com"))
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(100)));
        
        services.AddRefitClient<IPayPalOrdersApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.sandbox.paypal.com"))
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(100)));

        services.AddRefitClient<IPayPalWebhookApi>(provider => 
                    new RefitSettings
                    {
                        ContentSerializer = new SystemTextJsonContentSerializer(
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                            })
                    })
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.sandbox.paypal.com"))
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(100)));
        
        services.AddOptions<TwilioSmsSetting>()
            .BindConfiguration("Twilio")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddOptions<PaypalSetting>()
            .BindConfiguration("PayPal")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddTransient<ISecretManagerService, SecretManagerService>();
        services.AddMemoryCache();
        services.AddTransient<ISmsTwilioService,SmsTwilioService>();
        services.AddTransient<IPaypalCheckoutService, PaypalCheckoutService>();
        services.AddTransient<IPaypalAuthService, PaypalAuthService>();

        return services;
    }
    
}
