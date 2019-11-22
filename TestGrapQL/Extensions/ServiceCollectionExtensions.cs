using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using TestGrapQL.Attributes;

namespace TestGrapQL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScopedInjecteds(this IServiceCollection service, params Assembly[] assemblies)
        {
            var types = assemblies
                .SelectMany(t => t.GetTypes().Where(x => x.GetCustomAttribute<InjectedAttribute>() != null));
            foreach (var type in types)
                service.AddScoped(type);

            return service;
        }
    }
}
