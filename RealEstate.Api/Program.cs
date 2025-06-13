using RealEstate.Application;
using RealEstate.Infrastructure;
using RealEstate.Shared;
using RealEstate.Shared.Middleware;
using RealEstate.Shared.Security;
using RealEstate.Shared.Swagger;
using RealEstate.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddControllers();

builder.Services.AddApiVersioning();
builder.Services.AddLimitRate();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddSharedServices(builder.Configuration);
builder.Services.AddSharedLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.MapOpenApi();
app.UseSwaggerConfiguration();
app.UseInfrastructure();

// }

app.UseMiddleware<LoggingMiddleware>();
app.UseHttpsRedirection();
app.UseRateLimiter();
app.MapControllers();
app.Run();
