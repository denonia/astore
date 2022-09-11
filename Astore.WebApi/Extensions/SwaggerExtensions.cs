using Microsoft.OpenApi.Models;

namespace Astore.WebApi.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Astore", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
        });
    }

    public static IApplicationBuilder UseSwaggerWithUI(this WebApplication app)
    {
        return app.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/swagger.json")
            .UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "Astore API"));
    }
}