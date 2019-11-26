using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using MediatrGrapQL.Attributes;

namespace MediatrGrapQL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection service, ServiceLifetime lifetime, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(t => t.GetTypes().Where(x => x.GetCustomAttribute<InjectedAttribute>() != null));
            foreach (var type in types)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Transient:
                        service.AddTransient(type); break;
                    case ServiceLifetime.Scoped:
                        service.AddScoped(type); break;
                    case ServiceLifetime.Singleton:
                        service.AddSingleton(type); break;
                }
            }
            return service;
        }
    }
}
