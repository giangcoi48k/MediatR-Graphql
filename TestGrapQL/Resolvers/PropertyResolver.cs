using HotChocolate.Resolvers;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;
using TestGrapQL.Services;

namespace TestGrapQL.Resolvers
{
    public sealed class PropertyResolver : IBaseResolver
    {
        public int? Last { get; set; }

        public void CreateArguments(IObjectFieldDescriptor descriptor)
        {
            descriptor.Argument("last", a => a.Type<IntType>());
        }

        public void ResolveArguments(IResolverContext context)
        {
            Last = context.Argument<int?>("last");
        }

        private class PropertyResolverHandle : IBaseResolveHandler<PropertyResolver>
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
