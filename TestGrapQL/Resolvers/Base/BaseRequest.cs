using HotChocolate.Resolvers;
using MediatR;

namespace TestGrapQL.Resolvers
{
    public class BaseRequest : IRequest<object>
    {
        public BaseRequest(IResolverContext context)
        {
           
        }
    }
}
