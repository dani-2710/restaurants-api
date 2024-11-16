using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;

namespace Restaurants.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;


            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
            });
            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly)
            .AddFluentValidationAutoValidation();

            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}