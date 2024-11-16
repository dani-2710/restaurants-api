using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;

namespace Restaurants.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            },
                        },
                         []
                    }
                });
            });
            services.AddControllers();
            services.AddAuthentication();

            services.AddScoped<ErrorHandlingMiddleware>();


            return services;
        }
    }
}