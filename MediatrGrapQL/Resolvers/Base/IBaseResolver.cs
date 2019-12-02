using HotChocolate.Resolvers;
using HotChocolate.Types;
using MediatR;

namespace MediatrGrapQL.Resolvers
{
    public interface IBaseResolver<out TResult> : IRequest<TResult>
    {
        void ConfigFieldDescriptor(IObjectFieldDescriptor descriptor);

        void ResolverContext(IResolverContext context);
    }
}
