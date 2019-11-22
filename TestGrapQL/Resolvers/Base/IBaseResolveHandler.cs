using MediatR;

namespace TestGrapQL.Resolvers
{
    public interface IBaseResolveHandler<in TRequest> : IRequestHandler<TRequest, object>
        where TRequest : IBaseResolver
    {
    }
}
