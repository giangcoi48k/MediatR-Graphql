using HotChocolate.Resolvers;
using HotChocolate.Types;
using MediatR;
using System;
using TestGrapQL.Resolvers;

namespace TestGrapQL.Extensions
{
    public static class ObjectFieldDescriptorExtensions
    {
        public static IObjectFieldDescriptor AddResolver<T>(this IObjectFieldDescriptor descriptor)
            where T : BaseRequest
        {
            return descriptor.Resolver(async ctx =>
            {
                var request = (T)Activator.CreateInstance(typeof(T), ctx);
                return await ctx.Service<IMediator>().Send(request);
            });
        }

        public static IObjectFieldDescriptor AddResolver<T>(this IObjectFieldDescriptor descriptor, Func<IResolverContext, T> request)
            where T : BaseRequest
        {
            return descriptor.Resolver(async ctx =>
            {
                return await ctx.Service<IMediator>().Send(request(ctx));
            });
        }
    }
}
