using Microsoft.Extensions.DependencyInjection;
using RestApiBoilerplate.Application.Abstractions;
using RestApiBoilerplate.Infrastructure.Repositories;

namespace RestApiBoilerplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Replace this with an EF Core repository later.
        services.AddSingleton<IPersonRepository, InMemoryPersonRepository>();
        return services;
    }
}