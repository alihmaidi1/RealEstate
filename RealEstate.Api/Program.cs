using RealEstate.Application;
using RealEstate.Domain.Security;
using RealEstate.Infrastructure;
using RealEstate.Infrastructure.Repositories.Base.Security;
using RealEstate.Shared;
using RealEstate.Shared.Abstraction.CQRS;
using RealEstate.Shared.Middleware;
using RealEstate.Shared.Security;
using RealEstate.Shared.Swagger;
using RealEstate.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddControllers();




builder.Services.Scan(scan =>
    scan.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
        .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
        .AsImplementedInterfaces()
        .WithScopedLifetime()
        .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<>)), publicOnly: false)
        .AsImplementedInterfaces()
        .WithScopedLifetime()
        .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
        .AsImplementedInterfaces()
        .WithScopedLifetime()

);


builder.Services.AddApiVersioning();
builder.Services.AddLimitRate();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddSharedServices(builder.Configuration);
builder.Services.AddSharedLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.MapOpenApi();
app.UseSwaggerConfiguration();
await app.UseInfrastructure();

// }
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseMiddleware<RequestIdmiddleware>();

app.UseMiddleware<LoggingMiddleware>();
app.UseHttpsRedirection();
app.UseRateLimiter();
app.MapControllers();
app.Run();
