using MediatR;
using System.Linq;
using System.Reflection;
using MediatrGrapQL.Attributes;
using MediatrGrapQL.Resolvers;

namespace HotChocolate.Types
{
    public static class GraphqlExtensions
    {
        public static IObjectFieldDescriptor AddResolver<T, TResult>(this IObjectFieldDescriptor descriptor) where T : class, IBaseResolver<TResult>, new()
        {
            var request = new T();
            request.ConfigFieldDescriptor(descriptor);
            return descriptor.Resolver<TResult>(async context =>
            {
                request.ResolverContext(context);
                return await context.Service<IMediator>().Send(request);
            });
        }

        public static ISchemaBuilder ScanGraphTypes(this ISchemaBuilder builder, params Assembly[] assemblies)
        {
            var types = assemblies
                .SelectMany(x => x.GetTypes()
                    .Where(t => t != typeof(ObjectType) && typeof(ObjectType).IsAssignableFrom(t)
                        && t.GetCustomAttribute<GraphQueryTypeAttribute>() == null
                        && t.GetCustomAttribute<GraphMutationTypeAttribute>() == null
                        && t.GetCustomAttribute<GraphSubscriptionTypeAttribute>() == null)
                );

            foreach (var type in types)
            {
                builder.AddType(type);
            }

            return builder;
        }
    }
}
