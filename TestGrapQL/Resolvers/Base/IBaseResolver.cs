using HotChocolate.Resolvers;
using HotChocolate.Types;
using MediatR;

namespace TestGrapQL.Resolvers
{
    public interface IBaseResolver : IRequest<object>
    {
        void CreateArguments(IObjectFieldDescriptor descriptor);

        void ResolveArguments(IResolverContext context);
    }
}
