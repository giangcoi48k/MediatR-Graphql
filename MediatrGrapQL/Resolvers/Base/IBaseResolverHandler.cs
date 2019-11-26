using MediatR;

namespace MediatrGrapQL.Resolvers
{
    public interface IBaseResolverHandler<in TRequest, TRespone> : IRequestHandler<TRequest, TRespone>
      where TRequest : IBaseResolver<TRespone>
    {

    }
}
