using Microsoft.Extensions.DependencyInjection;
using RestApiBoilerplate.Application.Abstractions;
using RestApiBoilerplate.Application.Services;

namespace RestApiBoilerplate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();
        return services;
    }
}