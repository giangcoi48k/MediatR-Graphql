using HotChocolate.Resolvers;
using HotChocolate.Types;
using MediatR;

namespace TestGrapQL.Resolvers
{
    public interface IBaseResolver : IRequest<object>
    {
        void AddArguments(IObjectFieldDescriptor descriptor);

        void ResolveArguments(IResolverContext context);
    }
}
