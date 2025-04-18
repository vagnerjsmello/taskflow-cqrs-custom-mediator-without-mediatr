using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskFlow.CQRS.Infra.CQRS.Messaging;

namespace TaskFlow.CQRS.Infra.Extensions;

/// <summary>
/// Extension methods for registering the CQRS infrastructure (Bus and Handlers).
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the Bus and all message handlers implementing <see cref="IHandler{TMessage, TResult}"/>
    /// found in the specified assemblies.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="assembliesToScan">Assemblies to scan for handlers.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddCqrs(this IServiceCollection services, params Assembly[] assembliesToScan)
    {
        services.AddScoped<IMediator, Mediator>();

        services.Scan(scan => scan
            .FromAssemblies(assembliesToScan)
            .AddClasses(classes => classes.AssignableTo(typeof(IHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        return services;
    }
}

