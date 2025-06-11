using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace RealEstate.Shared.Swagger;

public static class DependencyInjection
{

    public static WebApplication UseSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "");

        });

        return app;
    }

}
