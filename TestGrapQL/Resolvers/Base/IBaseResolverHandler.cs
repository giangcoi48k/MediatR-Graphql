using MediatR;

namespace TestGrapQL.Resolvers
{
    public interface IBaseResolverHandler<in TRequest, TRespone> : IRequestHandler<TRequest, TRespone>
      where TRequest : IBaseResolver<TRespone>
    {

    }
}
