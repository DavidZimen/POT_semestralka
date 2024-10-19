using Microsoft.Extensions.DependencyInjection;

namespace Security.Extension;

internal static class ServiceCollectionExtensions
{
    internal static bool IsServiceRegistered<TService>(this IServiceCollection services)
    {
        return services.Any(s => s.ServiceType == typeof(TService));
    }
    
    internal static bool IsServiceRegistered<TService, TImplementation>(this IServiceCollection services)
    {
        return services.Any(s => s.ServiceType == typeof(TService) && s.ImplementationType == typeof(TImplementation));
    }
}