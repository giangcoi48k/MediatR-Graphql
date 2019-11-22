using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;
using TestGrapQL.Services;

namespace TestGrapQL.Resolvers
{
    public sealed class PropertiesResolver : IBaseResolver
    {
        public int? Last { get; set; }

        public void AddArguments(IObjectFieldDescriptor descriptor)
        {
            descriptor.Argument("Id", a => a.Type<IdType>());
        }

        public void ResolveArguments(IResolverContext context)
        {
            Last = context.Argument<int?>("last");
        }

        private class Handler : IBaseResolverHandler<PropertiesResolver>
        {
            private readonly PropertyService _propertyService;

            public Handler(PropertyService propertyService)
            {
                _propertyService = propertyService;
            }

            public async Task<object> Handle(PropertiesResolver request, CancellationToken cancellationToken)
            {
                return await _propertyService.GetAllAsync(request.Last);
            }
        }
    }
}
