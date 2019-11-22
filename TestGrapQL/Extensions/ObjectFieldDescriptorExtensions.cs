using HotChocolate.Types;
using MediatR;
using TestGrapQL.Resolvers;

namespace TestGrapQL.Extensions
{
    public static class ObjectFieldDescriptorExtensions
    {
        public static IObjectFieldDescriptor AddResolver<T>(this IObjectFieldDescriptor descriptor)
            where T : IBaseResolver, new()
        {
            var request = new T();
            request.AddArguments(descriptor);

            return descriptor.Resolver(async context =>
            {
                request.ResolveArguments(context);
                return await context.Service<IMediator>().Send(request);
            });
        }
    }
}
