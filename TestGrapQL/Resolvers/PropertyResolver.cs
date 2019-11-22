using HotChocolate.Resolvers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestGrapQL.Services;

namespace TestGrapQL.Resolvers
{
    public sealed class PropertyResolver : BaseRequest
    {
        public int? Last { get; set; }

        public PropertyResolver(IResolverContext context) : base(context)
        {
            Last = context.Argument<int?>("last");
        }

        private class PropertyResolverHandle : IRequestHandler<PropertyResolver, object>
        {
            private readonly PropertyService _propertyService;

            public PropertyResolverHandle(PropertyService propertyService)
            {
                _propertyService = propertyService;
            }

            public async Task<object> Handle(PropertyResolver request, CancellationToken cancellationToken)
            {
                return await _propertyService.GetAllAsync(request.Last);
            }
        }
    }
}
