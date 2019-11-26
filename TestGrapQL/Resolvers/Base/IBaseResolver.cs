using HotChocolate.Resolvers;
using HotChocolate.Types;
using MediatR;

namespace TestGrapQL.Resolvers
{
    public interface IBaseResolver<out TResult> : IRequest<TResult>
    {
        void AddArguments(IObjectFieldDescriptor descriptor);

        void ResolveArguments(IResolverContext context);
    }
}
