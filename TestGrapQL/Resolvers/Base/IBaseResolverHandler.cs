using MediatR;

namespace TestGrapQL.Resolvers
{
    public interface IBaseResolverHandler<in TRequest> : IRequestHandler<TRequest, object>
        where TRequest : IBaseResolver
    {
    }
}
